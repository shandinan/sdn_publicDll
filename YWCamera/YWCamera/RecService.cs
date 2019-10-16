using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using YWCamreaOper;

namespace YWCamera
{
    public partial class RecService : Form
    {
        Dictionary<string, YWCamreaOper.OperCamera> dicIpAndOper = new Dictionary<string, YWCamreaOper.OperCamera>(); //定义存放IP和摄像头操作类的字典
        public RecService()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 开始录像
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStart_Click(object sender, EventArgs e)
        {
            ErrorMsg errorMsg = new ErrorMsg();
            m_messagecallback messageCallBack = new m_messagecallback(errorMsg.messagecallback);
           // Thread td = new Thread();
            YWCamreaOper.CameraParam cp = new YWCamreaOper.CameraParam();
            cp.callback = messageCallBack;
            cp.m_buffnum = 20; //播放缓存大小
            cp.m_ch = 0;
            cp.m_hChMsgWnd = IntPtr.Zero; //通知信息句柄
            cp.m_hVideohWnd = IntPtr.Zero;//视频播放窗口
            cp.m_nChmsgid = 0;//消息通道号
            cp.m_playstart = 0;//是否启动视频播放显示
            cp.m_tranType = 3;//通信连接方式 3TCP
            cp.m_useoverlay = 0;//
            cp.m_password = "888888";//密码
            cp.m_sername = "video serveer";
            cp.m_username = "888888";
            cp.port = "3000";
            cp.url = "192.1.6.96";
            new Thread(StartRecVideo).Start((object)cp);

            Thread.Sleep(100);

          //  m_messagecallback messageCallBack = new m_messagecallback(errorMsg.messagecallback);
            // Thread td = new Thread();
            YWCamreaOper.CameraParam cp1 = new YWCamreaOper.CameraParam();
            cp1.callback = messageCallBack;
            cp1.m_buffnum = 20; //播放缓存大小
            cp1.m_ch = 0;
            cp1.m_hChMsgWnd = IntPtr.Zero; //通知信息句柄
            cp1.m_hVideohWnd = IntPtr.Zero;//视频播放窗口
            cp1.m_nChmsgid = 0;//消息通道号
            cp1.m_playstart = 0;//是否启动视频播放显示
            cp1.m_tranType = 3;//通信连接方式 3TCP
            cp1.m_useoverlay = 0;//
            cp1.m_password = "888888";//密码
            cp1.m_sername = "video serveer";
            cp1.m_username = "888888";
            cp1.port = "3000";
            cp1.url = "192.1.6.97";
            new Thread(StartRecVideo).Start((object)cp1);
        }
        int iNum = 1024;
        private void StartRecVideo(object obj)
        {
            YWCamreaOper.CameraParam camParm = (YWCamreaOper.CameraParam)obj; 
            OperCamera op = new OperCamera(camParm);
            dicIpAndOper.Add(camParm.url, op);
            bool bl1 = op.ClientInit(iNum);
            iNum++;
            int iHandle = op.CameraPreview();
            Thread.Sleep(100);
            bool bll1 = op.YLRecVideo(iHandle, true, 200, 250);
            Thread.Sleep(100);
            bool bl2 = op.StartRec(iHandle, @"D:\Video\fengshun\22_" + camParm.url + ".mp4");

        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            dicIpAndOper["192.1.6.96"].StopRec(-1);
            dicIpAndOper["192.1.6.96"].PreviewStop(-1);
            dicIpAndOper["192.1.6.96"].ClientFree();
        }
    }
}
