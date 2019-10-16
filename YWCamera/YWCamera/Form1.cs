using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using YWCamreaOper;

namespace YWCamera
{
    public partial class Form1 : Form
    {
        public Int32 handle = -1; //摄像头视频预览句柄
        public int WM_USER = 0x0400;
        public int WM_COMMAND;

        string strIp;//摄像头ip
        string strUid;//摄像头用户名
        string strPwd;//摄像头密码

        public Form1()
        {
            InitializeComponent();
        }

        public const int LAUMSG_LINKMSG = 1; //连接信息
        public const int LAUMSG_VIDEOMOTION = 2; //视频移动报警消息
        public const int LAUMSG_VIDEOLOST = 3; //视频丢失消息
        public const int LAUMSG_ALARM = 4; //探头报警开始消息
        public const int LAUMSG_OUTPUTSTATUS = 5; //报警输出状态消息
        public const int LAUMSG_CURSWITCHCHAN = 6; //通道切换消息
        public const int LAUMSG_HIDEALARM = 7; //视频遮挡报警消息
       // public const int 
        /// <summary>
        /// 注册信息回调 函数
        /// </summary>
        /// <param name="hHandle"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <param name="context"></param>
        public static void messagecallback(IntPtr hHandle, int wParam, int lParam, IntPtr context)
        {
            switch (wParam)
            {
                case LAUMSG_LINKMSG:
                    {
                        //link message
                        if (lParam == 0)
                        {
                            Trace.WriteLine("连接成功\n");
                        }
                        else if (lParam == 1)
                        {
                            Trace.WriteLine("用户停止连接\n");
                        }
                        else if (lParam == 2)
                        {
                            Trace.WriteLine("连接失败\n");
                        }
                        else if (lParam == 3)
                        {
                            Trace.WriteLine("连接断开\n");
                        }
                        else if (lParam == 4)
                        {
                            Trace.WriteLine("短裤维护\n");
                        }
                        else if (lParam == 5)
                        {
                            Trace.WriteLine("分配内存失败\n");
                        }
                        else if (lParam == 6)
                        {
                            Trace.WriteLine("连接DNS错误\n");
                        }
                        else if (lParam == -102)
                        {
                            Trace.WriteLine("用户名或密码错误\n");
                        }
                        else if (lParam == -103)
                        {
                            Trace.WriteLine("系统用户已满\n");
                        }
                        else if (lParam == -105)
                        {
                            Trace.WriteLine("通道用户已满\n");
                        }
                        else if (lParam == -106)
                        {
                            Trace.WriteLine("没有该通道\n");
                        }
                        else if (lParam == -112)
                        {
                            Trace.WriteLine("没有找到服务器\n");
                        }
                        else
                        {
                            Trace.WriteLine("未定义错误~! \n");
                        }
                        break;
                    }
                case LAUMSG_VIDEOMOTION:
                    Trace.WriteLine("视频请求警告\n");
                    break;
                case LAUMSG_VIDEOLOST:
                    Trace.WriteLine("视频丢失警告\n");
                    break;
                case LAUMSG_ALARM:
                    {
                        Trace.WriteLine("探头报警警告\n");
                        break;
                    }
                case LAUMSG_OUTPUTSTATUS:
                    Trace.WriteLine("报警输出状态消息\n");
                    break;
                case LAUMSG_CURSWITCHCHAN:
                    Trace.WriteLine("通道切换消息\n");
                    break;
                case LAUMSG_HIDEALARM:
                    Trace.WriteLine("视频遮挡报警消息\n");
                    break;
                default:
                    Trace.WriteLine("未定义错误~! \n");
                    break;
            }
        }


        static m_messagecallback msgCallbk = new m_messagecallback(messagecallback);


        /// <summary>
        /// 初始化SDK
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInit_Click(object sender, EventArgs e)
        {
            strIp = txtIp.Text;//摄像头ip
            strUid = txtUid.Text;//用户名
            strPwd = txtPwd.Text;//密码

            WM_COMMAND = WM_USER + 2;
            bool flag = YWCamreaOper.YWCamera.VSNET_ClientStartup((uint)WM_COMMAND, IntPtr.Zero, null, IntPtr.Zero, IntPtr.Zero); //初始化加载SDK


            YWCamreaOper.CHANNEL_CLIENTINFO info = new YWCamreaOper.CHANNEL_CLIENTINFO();
            info.m_buffnum = 50;
            info.m_ch = 0;

            info.m_hVideohWnd = IntPtr.Zero;//this.plPlayer.Handle;

            info.m_hChMsgWnd = IntPtr.Zero;    //
            info.m_nChmsgid = 0;              //

            info.m_sername = "video server";
            info.m_username = strUid;
            info.m_password = strPwd;
            info.m_playstart = 1;            //启动视频预览
            info.m_tranType = 3;             //TCP
            info.m_useoverlay = 0;
            info.url = strIp;
            info.m_ch = Convert.ToByte(0);
            info.callback = msgCallbk;

            int iSizeOfStruct = Marshal.SizeOf(typeof(CHANNEL_CLIENTINFO));
            IntPtr pStructChannel = Marshal.AllocHGlobal(iSizeOfStruct);
            Marshal.StructureToPtr(info, pStructChannel, false);

            handle = YWCamreaOper.YWCamera.VSNET_ClientStart(strIp, pStructChannel, Convert.ToInt16("3000"), 0);

          //  int iHandle = YWCamreaOper.YWCamera.VSNET_ClientStart("",IntPtr.Zero,)

        }

        /// <summary>
        /// 录像开始
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRec_Click(object sender, EventArgs e)
        {
            //  bool blRecflag = YWCamreaOper.YWCamera.VSNET_ClientStartMp4Capture(handle,@"D:\123.mp4"); //VSNET_ClientStartASFFileCap
            strIp = txtIp.Text;//摄像头ip
            bool blRecflag = YWCamreaOper.YWCamera.VSNET_ClientStartASFFileCap(handle, string.Format(@"D:\{0}_rec.mp4", strIp), false);
        }
        /// <summary>
        /// 录像结束
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStopRec_Click(object sender, EventArgs e)
        {
            bool blStopRec = YWCamreaOper.YWCamera.VSNET_ClientStopMp4Capture(handle);
        }
        
        /// <summary>
        /// 图片回调函数
        /// </summary>
        /// <param name="hHandle"></param>
        /// <param name="m_ch"></param>
        /// <param name="pBuffer"></param>
        /// <param name="size"></param>
        /// <param name="userdata"></param>
        public void jepgCallBack(Int32 hHandle, int m_ch, IntPtr pBuffer, int size, IntPtr userdata)
        {
            byte[] byImgs = new byte[size];
            Marshal.Copy(pBuffer, byImgs, 0, size);
            MemoryStream stream = new MemoryStream(byImgs);
            System.Drawing.Image img = System.Drawing.Image.FromStream(stream);
            img.Save("D:\\xxx.jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);//xxx.jpeg为文件名

        }
       // YWCamreaOper.jpegdatacallback jepgCall = new YWCamreaOper.jpegdatacallback(jepgCallBack);
        /// <summary>
        /// 图片抓拍
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCapter_Click(object sender, EventArgs e)
        {
            YWCamreaOper.YWCamera.jpegdatacallback jpegBack = new YWCamreaOper.YWCamera.jpegdatacallback(jepgCallBack);
            Int32 iHandle = YWCamreaOper.YWCamera.VSNET_ClientJpegCapStart("Video server", "192.1.6.97", "888888", "888888", Convert.ToInt16("3000"), jpegBack, null);
            bool iFlag = YWCamreaOper.YWCamera.VSNET_ClientJpegCapSingle(iHandle, 0, 10);

            Thread.Sleep(500);
            bool iFlagstop = YWCamreaOper.YWCamera.VSNET_ClientJpegCapStop(iHandle);

        }
    }
}
