using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GetUsbMsg;
using sdnHIKCamera;
using System.Threading;
using sdnHttpOper;

namespace publicDll
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
         //   sdnGetUsbMsg sdnGetUsb = new sdnGetUsbMsg();
         //   sdnGetUsb.UsBMethod(1);
        }

        /// <summary>
        /// 摄像头测试按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCamera_Click(object sender, EventArgs e)
        {
            string strMsg = string.Empty;
            int[] iChannel;
            int iRes = 0;
            //1.初始化sdk
            iRes = sdnHIKMethod.sdnInitSDK(out strMsg);
            if (iRes < 0)
            {
                MessageBox.Show(strMsg);
            }
            else
            {
                iChannel = new int[99];
            }
            //2.登录
            LoginParm loginParm = new LoginParm();
            loginParm.Login_Ip = "172.168.1.117";//"10.35.132.156";
            loginParm.Login_Port = "8000";
            loginParm.Login_Name = "admin";
            loginParm.Login_Password = "admin123";
            loginParm.Login_Channel = 0;
          //  string[] arr1 = {"X","Z","C","A","S","D","W","E","Q"};
            string[] arrNum1 = {"1", "2", "3", "4", "5", "6", "7", "8", "9", "0"};
            string mayPass = "XXWgsb";
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        for (int m = 0; m < 10; m++)
                        {
                          //  loginParm.Login_Password = mayPass + arrNum1[i] + arrNum1[j] + arrNum1[m];
                            loginParm.Login_Password = "admin123";

                          //  MessageBox.Show(loginParm.Login_Password);
                            iRes = sdnHIKMethod.sdnLogin(loginParm, out iChannel);
                            if (iRes < 0)
                            {
                                MessageBox.Show("登录失败" + loginParm.Login_Password);
                                Thread.Sleep(300);

                            }
                            else
                            {
                               // isBl = false;
                                MessageBox.Show("登录成功=========" + loginParm.Login_Password);
                                break;
                            }
                        }
                    }
                }
              
           // //3.预览
           // PlayVideoParm playParm = new PlayVideoParm();
           // playParm.bBlocked = true;
           // playParm.dwLinkMode = 0;
           // playParm.dwStreamType = 0;
           // playParm.intControls = IntPtr.Zero;
           // playParm.lChannel =1;
           // playParm.lUserId = iRes;
           // playParm.previewType = 2;//不显示只取流
           // playParm.videoName = @"D:\sdn123.mp4";
           //iRes = sdnHIKMethod.sdnStartPreview(playParm,out strMsg);
           //Thread.Sleep(100000);
           // sdnHIKMethod.sdnStopPreview(playParm,iRes,out strMsg);
          //  playParm.bBlocked
            //4.停止预览
            //5.退出
        }
        /// <summary>
        /// 测试http
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTestHttp_Click(object sender, EventArgs e)
        {
            //sdnHttpOper.sdnHttpWebRequest sdnhttp = new sdnHttpWebRequest();
            //string strContent = string.Format("{{\"NAME\":\"测试\",\"CARDNOIDENTITYNUM\":\"1234567890\",\"SEX\":\"男\",\"NATION\":\"汉\",\"YEAR\":2010,\"MONTH\":10,\"DAY\":27,\"ADDRESS\":\"沟沟壑壑\",\"SIGN\":\"显示公安局\",\"STARTTIME\":\"20141021\",\"ENDTIME\":\"20201021\",\"PHOTOPATH\":\"\",\"QHXXXLH\":\"0987654321\"}}");
            //string strurl = "http://localhost:64683/api/PdjhInterface/AddRecCardMsg";
            //string reslut = sdnhttp.sdnDoPost(strurl, strContent);
         //   string UPLOADUrl = "http://localhost:64683/tools/
          //  new sdnHttpUploadFIle().Upload_Request(UPLOADUrl, dr["PHOTOPATH"].ToString(), "3_" + dr["CARD_NUM"].ToString().Trim() + ".bmp");
        }
        /// <summary>
        /// 控制执法记录仪 客户端录像
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        int[] iChanNums; int iUserId;
        private void btnRecord_Click(object sender, EventArgs e)
        {
             // 1. 先登录设备
            LoginParm loginParm = new LoginParm();
            loginParm.Login_Ip = "192.168.2.145";
            loginParm.Login_Name = "admin";
            loginParm.Login_Password = "123456";
            loginParm.Login_Port = "8000";
            InitSDK initSdk = new InitSDK();
            string strMsg = "";
            initSdk.InitDll(out strMsg); //初始化DLL
            CameraLogin cameraLogin = new CameraLogin(loginParm);
            //int[] iChanNums;
            iUserId = cameraLogin.LoginDll(out iChanNums);

            //2.远程开启或关闭录像
            sdnHIKCamera.dvr_record_param dvr_record_param = new dvr_record_param();
            dvr_record_param.lChannel = iChanNums[0];
            dvr_record_param.lRecordType = 0;
            dvr_record_param.lUserId = iUserId;
            DvrRecord dvr_record = new DvrRecord(dvr_record_param);
            bool blRes = dvr_record.start_dvrRecord(out strMsg);
           
        }
        /// <summary>
        /// 按照时间下载视频
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDownVideo_Click(object sender, EventArgs e)
        {
            string strMsg;
            //1. 登录摄像头
            LoginParm loginParm = new LoginParm();
            loginParm.Login_Ip = "192.168.2.145";
            loginParm.Login_Name = "admin";
            loginParm.Login_Password = "123456";
            loginParm.Login_Port = "8000";
            InitSDK initSdk = new InitSDK();
            initSdk.InitDll(out strMsg); //初始化DLL
            CameraLogin cameraLogin = new CameraLogin(loginParm);
            //int[] iChanNums;
            iUserId = cameraLogin.LoginDll(out iChanNums);

            //2 根据时间远程下载
            if (iUserId >= 0)
            {
                playBackNvr_param downFileParam = new playBackNvr_param();
                downFileParam.endTime = DateTime.Now;
                downFileParam.lChannel = iChanNums[0];
                downFileParam.lUserId = iUserId;
                downFileParam.sSavedFileName = AppDomain.CurrentDomain.BaseDirectory + "videos\\test.mp4";
                downFileParam.startTime = DateTime.Now.AddMinutes(-25);
                playBackNvr playBack = new playBackNvr(downFileParam);
                bool blRes = playBack.downFileByTime(out strMsg);
            }
            cameraLogin.LogoutDll(iChanNums[0], iUserId, out strMsg);
        }
        /// <summary>
        /// 结束录像
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStopRec_Click(object sender, EventArgs e)
        {
            sdnHIKCamera.dvr_record_param dvr_record_param = new dvr_record_param();
            dvr_record_param.lChannel = iChanNums[0];
            dvr_record_param.lRecordType = 0;
            dvr_record_param.lUserId = iUserId;
            DvrRecord dvr_record = new DvrRecord(dvr_record_param);
            string strMsg;
            bool blRes = dvr_record.stop_dvrRecord(out strMsg);
        }
    }
}
