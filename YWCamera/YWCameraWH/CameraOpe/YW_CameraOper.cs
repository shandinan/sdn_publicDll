using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using YWCamreaOper;

namespace YWCameraWH.CameraOpe
{
    public class YW_CameraOper : CameraOper
    {
        #region 定义的变量
        public int iNum = 1024; //亿维摄像头加载SDK所需函数
        OperCamera op = null;// 亿维摄像头操作类
        Camera_Data video_camera = null;//摄像头数据
        int iHandle = -1;//亿维摄像头预览句柄
        ErrorMsg errorMsg;

        #endregion
        public YW_CameraOper(Camera_Data video_camera)
        {
            this.video_camera = video_camera;
        }
        /// <summary>
        /// 初始化SDK
        /// </summary>
        /// <returns></returns>
        public override int InitSDK()
        {
            if (video_camera != null)
            {
             //   ErrorMsg errorMsg = new ErrorMsg();
                errorMsg = new ErrorMsg();
                m_messagecallback messageCallBack = new m_messagecallback(errorMsg.messagecallback);
                CameraParam cp1 = new CameraParam();
                cp1.callback = messageCallBack;
                cp1.m_buffnum = 20; //播放缓存大小
                cp1.m_ch = 0;
                cp1.m_hChMsgWnd = IntPtr.Zero; //通知信息句柄
                cp1.m_hVideohWnd = IntPtr.Zero;//视频播放窗口
                cp1.m_nChmsgid = 0;//消息通道号
                cp1.m_playstart = 0;//是否启动视频播放显示
                cp1.m_tranType = 3;//通信连接方式 3TCP
                cp1.m_useoverlay = 0;//
                cp1.m_password = video_camera.LoginPwd;//密码
                cp1.m_sername = video_camera.Camera_Name;
                cp1.m_username = video_camera.LoginName;
                cp1.port = video_camera.LoginPort;
                cp1.url = video_camera.LoginIp;
                op = new OperCamera(cp1); //实例化摄像头操作类
                return op.ClientInit(iNum) ? 1 : -1; //加载SDK
            }
            return -1;


            throw new NotImplementedException();
        }
        /// <summary>
        /// 卸载SDK
        /// </summary>
        /// <returns></returns>
        public override int FreeSDK()
        {
            if (op.ClientFree())
            {
                return 1;
            }
            else
            {
                return -1;
            }
            throw new NotImplementedException();
        }
        /// <summary>
        /// 登录(亿维摄像头不启用）
        /// </summary>
        /// <returns></returns>
        public override int Login()
        {
            iHandle = op.CameraPreview(); //摄像头预览
            Common.SysLog.WriteOptDisk(errorMsg.strMsg, AppDomain.CurrentDomain.BaseDirectory);
            Thread.Sleep(100);
            //   bool bll1 = op.YLRecVideo(iHandle, true, 200, 250); //打开预录像功能 单氐楠 2015年8月26日08:13:02 第一笔数据不录像
            Thread.Sleep(100);
            return iHandle;
            throw new NotImplementedException();
        }
        /// <summary>
        /// 退出（亿维摄像头不启用）
        /// </summary>
        /// <returns></returns>
        public override int LogOut()
        {
            if (op.PreviewStop(-1))
            {
                return 1;
            }
            else
            {
                return -1;
            }
            throw new NotImplementedException();
        }
        /// <summary>
        /// 开始预览（为了统一，这里当作开始录像使用）
        /// </summary>
        /// <returns></returns>
        public override int StartPreview()
        {
            if (!string.IsNullOrEmpty(video_camera.File_Path))
            {
                string strPath = video_camera.File_Path.Substring(0, video_camera.File_Path.LastIndexOf("\\"));
                if (!Directory.Exists(strPath))
                {
                    Directory.CreateDirectory(strPath);
                }
                bool bl2 = op.StartRec(-1, video_camera.File_Path);
                Common.SysLog.WriteOptDisk(errorMsg.strMsg, AppDomain.CurrentDomain.BaseDirectory);
                return bl2 ? 1 : -1;
            }
            return -1;
            throw new NotImplementedException();
        }
        /// <summary>
        /// 停止预览(为了统一，这里当作停止录像使用）
        /// </summary>
        /// <returns></returns>
        public override int StopPreview()
        {
            if (op.StopRec(-1))
            {
                return 1;
            }
            else
            {
                return -1;
            }
            throw new NotImplementedException();
        }
        /// <summary>
        /// 开始录像
        /// </summary>
        /// <returns></returns>
        public override int StartRecord()
        {
            return -1;
            throw new NotImplementedException();
        }
        /// <summary>
        /// 停止录像
        /// </summary>
        /// <returns></returns>
        public override int StopRecord()
        {
            return -1;
            throw new NotImplementedException();
        }
        /// <summary>
        /// 摄像头截图
        /// </summary>
        /// <returns></returns>
        public override int CapImgs()
        {
            return -1;
            throw new NotImplementedException();
        }
    }
}
