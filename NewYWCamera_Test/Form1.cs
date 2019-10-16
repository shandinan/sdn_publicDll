using NewYWCameraOper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace NewYWCamera_Test
{
    public partial class Form1 : Form
    {
        NewYWCameraOper.NewYWOper newYw = null;
        int iLogin = 0;
        public Form1()
        {
            InitializeComponent();
            
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            IntPtr showHandle = picBoxShow.Handle;
            //newYw.sdnLogonServer();
            NewYWCameraOper.NewYWcameraParam param = new NewYWCameraOper.NewYWcameraParam();
            param.dwClientID = 101;
            param.hNotifyWindow = IntPtr.Zero;
            param.pDeviceName = "DVS";
            param.pServerIP = "192.168.59.128";
            param.pServerPort = 5000;
            param.pUserName = "admin";
            param.pUserPassword="12345qwert";
            string strMsg="";
         //   NewYWCameraOper.HHNetInterface.ChannelStreamCallback funStreanCallback = new NewYWCameraOper.HHNetInterface.ChannelStreamCallback(sdnChannelStramCallBack);
          //  NewYWCameraOper.HHNetInterface.HHOPEN_CHANNEL_INFO open_channel_info = new NewYWCameraOper.HHNetInterface.HHOPEN_CHANNEL_INFO()
          //  {101,0,NewYWCameraOper.HHNetInterface.NET_PROTOCOL_TYPE.NET_PROTOCOL_TCP,funStreanCallback,showHandle};
            NewYWCameraOper.HHNetInterface.HHOPEN_CHANNEL_INFO open_channel_info = new NewYWCameraOper.HHNetInterface.HHOPEN_CHANNEL_INFO();
            open_channel_info.dwClientID = 101;
            open_channel_info.nOpenChannel = 0;
            open_channel_info.protocolType = NewYWCameraOper.HHNetInterface.NET_PROTOCOL_TYPE.NET_PROTOCOL_TCP;
            open_channel_info.funcStreamCallback = null;
            open_channel_info.pCallbackContext = showHandle;
            

             newYw = new NewYWCameraOper.NewYWOper(param);

         //  int b= newYw.ClientInit(this.Handle, 0, "192.1.6.177");

             string strFileName = string.Format(@"D:\sdnVideo\{0}.mp6", DateTime.Now.ToString("yyyyMMddHHmmss"));
             // iRecHandle = newYw.sdnStartRecVideo(5 * 1024, strFileName, out strMsg);
             newYw.strVideoPath = strFileName;



             iLogin = newYw.sdnLogonServer(out strMsg);
            int  op = newYw.sdnOpenChannel(out strMsg);

             int iRead = newYw.sdnReadChannelInfo(out strMsg);
 
          

        }


        private int sdnChannelStramCallBack(IntPtr hOpenChannel, IntPtr pStreamData, uint dwClientID, IntPtr pConetxt, NewYWCameraOper.HHNetInterface.ENCODE_VIDEO_TYPE encodeVideoType,IntPtr pAVInfo)
        {
            return 0;
        }

        int iRecHandle = 0;
        /// <summary>
        /// 录像
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRec_Click(object sender, EventArgs e)
        {
            string strMsg = "";
            string strFileName = string.Format(@"D:\sdnVideo\{0}.mp6", DateTime.Now.ToString("yyyyMMddHHmmss"));
           // iRecHandle = newYw.sdnStartRecVideo(5 * 1024, strFileName, out strMsg);
            newYw.strVideoPath = strFileName;
        }

        private void btnStopRec_Click(object sender, EventArgs e)
        {
           // newYw.sdnStopRecVideo(iRecHandle);
            newYw.sdnCloseChannel();
        }

        string strFile = "";
       public static  HHNetInterface.PictureCallback delPic = null;
        private void btnCapture_Click(object sender, EventArgs e)
        {
            string imgName = DateTime.Now.Millisecond.ToString();
            strFile = string.Format("D:\\abc{0}.jpg", imgName);
            //delPic = new HHNetInterface.PictureCallback(sdnCapturePictureCallback);

            newYw.sdnCapturePic(strFile);
           // newYw.sdnCaptureCallback(strFile, delPic);
        }

        /// <summary>
        /// 前段抓图回掉函数
        /// </summary>
        /// <param name="hPictureChn">拍照句柄</param>
        /// <param name="pPicData">图片信息</param>
        /// <param name="nPicLen">图片长度</param>
        /// <param name="dwClientID"></param>
        /// <param name="pContext"></param>
        /// <returns></returns>
        private int sdnCapturePictureCallback(IntPtr hPictureChn, IntPtr pPicData, int nPicLen, uint dwClientID, IntPtr pContext)
        {
            HHNetInterface.HH_PICTURE_INFO hhPicInfo = new HHNetInterface.HH_PICTURE_INFO();//图片信息结构体

            IntPtr iPicInfo = IntPtr.Zero;
          
            byte[] sdnByteheader = new byte[((int)nPicLen) + 1];
            Marshal.Copy(pPicData, sdnByteheader, 0, (int)nPicLen); //复制句柄指定内存到字节数组

            //将缓冲区里的JPEG图片数据写入文件
            FileStream fs = new FileStream(strFile, FileMode.Create);
            int iLen = (int)nPicLen;
            fs.Write(sdnByteheader, 0, iLen);
            fs.Close();
            return 0;
            // }
            return -1;
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnCap2_Click(object sender, EventArgs e)
        {
            newYw.sdnCaptureImg();
        }
    }
}
