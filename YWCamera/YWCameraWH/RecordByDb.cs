using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using YWCameraWH.baseThread;
using YWCameraWH.CameraOpe;
using YWCameraWH.storeBase;

namespace YWCameraWH
{
    public class RecordByDb
    {
        BaseThread_Came baseThread = null;

        private QueueList m_StartQueue = new QueueList(); //开始录像队列
        private QueueList m_RecQueue = new QueueList();  //录像中队列

        public delegate void ServiceLog(string msg);
        public event ServiceLog EventServiceLog;
        CameraOper camera_oper = null;//摄像头操作类

        int iNum = 0;//批次号
        public void sdnStartRecByDb()
        {
            try
            {
                List<Camera_Data> cameras = new List<Camera_Data>();
                try
                {
                    //从数据库中获取需要开始录像的摄像头
                    DataTable dt = new OperXml().GetCameraDataByXml("camera.xml", "1=1").Table; //获取数据源 ==========================================================
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            int i = 1;
                            foreach (DataRow row in dt.Rows)
                            {
                                Camera_Data came_temp = new Camera_Data();
                                came_temp.LoginChannel = "0";//row["CHANNEL"].ToString();
                                came_temp.LoginIp = row["ip"].ToString();
                                came_temp.LoginName = row["user"].ToString();
                                came_temp.LoginPort = "3000";//row["PORT"].ToString();
                                came_temp.LoginPwd = row["pwd"].ToString();
                                came_temp.Camera_Type = row["cametype"].ToString();
                                came_temp.Camera_Name = "video server";//摄像头名称
                                came_temp.VideoTemp_Id = i++;// Convert.ToInt32(row["ID"].ToString());
                                string strFilePath = "D:\\YWVIDEO_test\\" + row["name"].ToString() + ".mp4";
                                try
                                {
                                    if (File.Exists(strFilePath)) //如果该文件存在
                                    {
                                        File.Delete(strFilePath);
                                    }
                                }
                                catch { }
                                came_temp.File_Path = "D:\\YWVIDEO_test\\" + row["name"].ToString() + ".mp4";
                                cameras.Add(came_temp);
                            }
                        }
                    }

                    if (cameras != null && cameras.Count > 0)
                    {
                        iNum++;
                        foreach (Camera_Data video_camera in cameras)
                        {
                            //添加对应的类到字典中，以便与之后查询
                            //  cameraParm sdnCP = new cameraParm();
                            QueueItem sdnItem = new QueueItem();
                            try
                            {
                                sdnItem.iFlag = iNum;
                                sdnItem.cameraId = Convert.ToInt32(video_camera.Camera_Id);
                                sdnItem.camera_Data = video_camera;
                                sdnItem.lRealHandle = -1;
                                sdnItem.luserId = -1;
                                sdnItem.usedState = 0;
                                sdnItem.videoId = video_camera.VideoTemp_Id;

                                if (m_StartQueue.Find(video_camera.VideoTemp_Id) != null)
                                {
                                    m_StartQueue.Find(video_camera.VideoTemp_Id).iFlag += 1;

                                    if (m_StartQueue.Find(video_camera.VideoTemp_Id).lRealHandle < 0 && m_StartQueue.Find(video_camera.VideoTemp_Id).iFlag < 3)
                                    {
                                        m_StartQueue.Find(video_camera.VideoTemp_Id).usedState = 0; //把状态重新置为0，未使用
                                    }
                                }
                                else
                                {
                                    lock (this)
                                    {
                                        m_StartQueue.Add(video_camera.VideoTemp_Id, sdnItem);//把此数据加入开始队列
                                    }
                                }
                            }
                            catch
                            { }
                        }
                    }
                    Thread.Sleep(200);//线程停止2秒
                }
                catch (Exception ex)
                {
                    // EventServiceLog("获取开始录像摄像头数据时：" + ex.Message);
                    Thread.Sleep(2000);//如果连接数据库失败则停止两秒
                    // continue;
                }
            }
            // }
            catch (Exception ex)
            {
                //EventServiceLog(ex.Message);
            }
        }
        /// <summary>
        /// 循环开始录像
        /// </summary>
        public void sdnLoopStartRec()
        {

            try
            {
                QueueItem item = null;
                while (true)
                {
                    if ((item = this.m_StartQueue.GetTopOutQueue()) != null)
                    {
                        if (item.lRealHandle < 0) //如果播放句柄大于0 则证明已经播放了，可以移除并添加到录像中队列
                        {
                            baseThread = new BaseThread_Came(0, item.camera_Data);
                            //  baseThread.ThreadEvent += sdnRecByDb;
                            baseThread.ThreadEvent += sdnCameraRecord; //绑定全摄像头操作类
                            baseThread.StartThread();
                            //this.m_StartQueue.Remove(item.videoId);
                            // this.m_RecQueue.Add(item.videoId, item);
                        }
                        else
                        {
                            this.m_StartQueue.Remove(item.videoId);
                        }
                        Thread.Sleep(200); //一秒的延迟
                    }
                    else
                    {
                        Thread.Sleep(1000);
                    }
                    Thread.Sleep(200);
                }
            }
            catch (Exception ex)
            {
                //   EventServiceLog(ex.Message);
                Common.SysLog.WriteLog(ex, AppDomain.CurrentDomain.BaseDirectory);
            }
        }

        #region 摄像头操作 ---- 策略模式
        /// <summary>
        /// 开始录像
        /// </summary>
        /// <param name="video_camera"></param>
        private void sdnCameraRecord(Camera_Data video_camera)
        {
            string strMsg = string.Empty;
            int iUid = -1;
            int iReal = -1;
            CameraOper camera_oper = null;//定义一个摄像头操作变量
            switch (video_camera.Camera_Type)
            {
                case "1"://海康摄像头
                    camera_oper = new CameraOpe.HK_CameraOper(video_camera);//实例化海康摄像头操作类
                    break;
                case "2": //亿维摄像头
                    camera_oper = new CameraOpe.YW_CameraOper(video_camera);
                    break;
                default:
                    break;
            }
            if (camera_oper.InitSDK() < 0)//初始化摄像头
            {//初始化SDK失败，返回，等待下次加载
                Common.SysLog.WriteOptDisk("初始化SDK失败" + video_camera.LoginIp, AppDomain.CurrentDomain.BaseDirectory);
                return;
            }
            if ((iUid = camera_oper.Login()) < 0)
            {
                camera_oper.FreeSDK(); //如果登录失败，则释放SDK，并等待下次启动录像
                Common.SysLog.WriteOptDisk("登录摄像头失败" + video_camera.LoginIp, AppDomain.CurrentDomain.BaseDirectory);
                return;
            }
            try
            {
                this.m_StartQueue.Find(video_camera.VideoTemp_Id).luserId = iUid;
            }
            catch (Exception ex)
            {
                Common.SysLog.WriteLog(ex, AppDomain.CurrentDomain.BaseDirectory);
            }
            if ((iReal = camera_oper.StartPreview()) < 0)
            {
                if ((iReal = camera_oper.StartPreview())<0)
                {
                    Common.SysLog.WriteOptDisk("再次预览录像失败" + video_camera.LoginIp, AppDomain.CurrentDomain.BaseDirectory);
                    camera_oper.LogOut();//如果预览失败，则注销登录，释放SDK
                    camera_oper.FreeSDK();//释放SDK
                    return;
                }
               
                
            }
            try
            {
                this.m_StartQueue.Find(video_camera.VideoTemp_Id).lRealHandle = iReal;
                this.m_StartQueue.Find(video_camera.VideoTemp_Id).camera_oper = camera_oper;//加入队列
                Common.SysLog.WriteOptDisk("录像成功" + video_camera.LoginIp, AppDomain.CurrentDomain.BaseDirectory);
            }
            catch (Exception ex)
            {
                Common.SysLog.WriteLog(ex, AppDomain.CurrentDomain.BaseDirectory);
            }
            DateTime dtStopTime = DateTime.Now.AddSeconds(Convert.ToInt32(5));//=======================================最长录像时间
            while (DateTime.Now <= dtStopTime && this.m_StartQueue.Find(video_camera.VideoTemp_Id) != null)
            {
                Thread.Sleep(300);
                continue;
            }
            if (this.m_StartQueue.Find(video_camera.VideoTemp_Id) != null)
            {
                StopCameraRec(camera_oper);
                camera_oper = null;//把摄像头操作类置为null
                this.m_StartQueue.Remove(video_camera.VideoTemp_Id);//移除队列中的数据
            }

        }

        /// <summary>
        /// 停止录像
        /// </summary>
        /// <param name="CamOper"></param>
        private void StopCameraRec(CameraOper CamOper)
        {
            try
            {
                if (CamOper == null)
                    return;
                //   CamOper.StopRecord();//停止录像
                if (CamOper.StopPreview() < 0)//停止预览
                {
                    Common.SysLog.WriteOptDisk("停止录像失败", AppDomain.CurrentDomain.BaseDirectory);
                }
                int i1 = CamOper.LogOut();//注销登录
                int i2 = CamOper.FreeSDK();//释放SDK
            }
            catch (Exception ex)
            {
                Common.SysLog.WriteLog(ex, AppDomain.CurrentDomain.BaseDirectory);
            }
        }

        #endregion

    }
}
