/**
 * 亿维摄像头基础类
 * 作者：单氐楠  日期：2015/08/11
 * 注意：未经本人同意切勿修改
 * */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace YWCamreaOper
{
    #region 枚举 
    public delegate void m_messagecallback(IntPtr hHandle, int wParam, int lParam, IntPtr context);
  //  public delegate void jpegdatacallback(Int32 hHandle, int m_ch, byte[] pBuffer, int size, string userdata);
    enum PTZ_COMMAND
    {
        PTZ_LEFT = 0,
        PTZ_RIGHT = 1,
        PTZ_UP = 2,
        PTZ_DOWN = 3,
        PTZ_IRISADD = 4,
        PTZ_IRISDEC = 5,
        PTZ_FOCUSADD = 6,
        PTZ_FOCUSDEC = 7,
        PTZ_ZOOMADD = 8,
        PTZ_ZOOMDEC = 9,
        PTZ_GOTOPOINT = 10,
        PTZ_SETPOINT = 11,
        PTZ_AUTO = 12,
        PTZ_STOP = 13,
        PTZ_LEFTSTOP = 14,
        PTZ_RIGHTSTOP = 15,
        PTZ_UPSTOP = 16,
        PTZ_DOWNSTOP = 17,
        PTZ_IRISADDSTOP = 18,
        PTZ_IRISDECSTOP = 19,
        PTZ_FOCUSADDSTOP = 20,
        PTZ_FOCUSDECSTOP = 21,
        PTZ_ZOOMADDSTOP = 22,
        PTZ_ZOOMDECSTOP = 23,
        PTZ_LIGHT = 24,
        PTZ_LIGHTSTOP = 25,
        PTZ_RAIN = 26,
        PTZ_RAINSTOP = 27,
        PTZ_TRACK = 28,
        PTZ_TRACKSTOP = 29,
        PTZ_DEVOPEN = 30,
        PTZ_DECCLOSE = 31,
        PTZ_AUTOSTOP = 32,
        PTZ_CLEARPOINT = 33,
        PTZ_LEFTUP = 200,
        PTZ_LEFTUPSTOP = 201,
        PTZ_RIGHTUP = 202,
        PTZ_RIGHTUPSTOP = 203,
        PTZ_LEFTDOWN = 204,
        PTZ_LEFTDOWNSTOP = 205,
        PTZ_RIGHTDOWN = 206,
        PTZ_RIGHTDOWNSTOP = 207,
    };
    #endregion

    #region 结构体

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct CHANNEL_CLIENTINFO
    {

        [MarshalAsAttribute(UnmanagedType.LPStr)]
        public string m_sername;                    //服务名称

        [MarshalAsAttribute(UnmanagedType.LPStr)]
        public string m_username;                   //用户名

        [MarshalAsAttribute(UnmanagedType.LPStr)]
        public string m_password;                   //密码

        public ushort m_tranType;                    //传输 协议 3:TCP  
        public ushort m_playstart;                   //是否预览
        public byte m_ch;                            //通道号
        public IntPtr m_hVideohWnd;                  //视频句柄
        public IntPtr m_hChMsgWnd;                   //消息句柄
        public uint m_nChmsgid;                      //消息ID  
        public int m_buffnum;                        //缓冲区数字
        public int m_useoverlay;                     //是否用遮罩
        public int nColorKey;                       //颜色

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
        public string url; 
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public m_messagecallback callback;
        IntPtr context;

    };

    #endregion

    #region c++驱动封装类
    public class YWCamera
    {
        public delegate void m_messagecallback(IntPtr hHandle, int wParam, int lParam, IntPtr context);
        /// <summary>
        /// 初始化客户端SDK
        /// </summary>
        [DllImport("NetClient.dll")]
        public static extern bool VSNET_ClientStartup(uint m_nMessage, IntPtr m_hWnd, m_messagecallback collback, IntPtr conntex, IntPtr key);

        //卸载客户端SDK 
        [DllImport("NetClient.dll")]
        public static extern bool VSNET_ClientCleanup();
        /// <summary>
        /// 设置客户端超时时间和尝试次数 
        /// </summary>
        /// <param name="m_waitnum"></param>
        /// <param name="m_trynum"></param>
        /// <returns></returns>
        [DllImport("NetClient.dll")]
        public static extern bool VSNET_ClientWaitTime(int m_waitnum = 6, int m_trynum = 3);

        //读取消息 
        [DllImport("NetClient.dll")]
        public static extern bool VSNET_ClientReadMessage(string m_sername, string m_url, uint m_port, int m_ch, long wParam, long lParam);

        // 启动预览       
        [DllImport("NetClient.dll")]
        public static extern Int32 VSNET_ClientStart([MarshalAs(UnmanagedType.LPStr)]string url,IntPtr info, short port, short streamtype);
        //停止预览
        [DllImport("NetClient.dll")]
        public static extern bool VSNET_ClientStop(Int32 handle);

        //获得该通道的状态
        [DllImport("NetClient.dll")]
        public static extern Int32 VSNET_ClientGetState(Int32 handle);

        //云台控制;
        [DllImport("NetClient.dll")]
        public static extern int VSNET_ClientPTZCtrl(Int32 handle, int type, int value, int priority, IntPtr extrabuff, int extrasize);

        //MP4录像开始
        [DllImport("NetClient.dll")]
        public static extern bool VSNET_ClientStartMp4Capture(Int32 hHandle, string m_FileName);

        //停止本地MP4录像 
        [DllImport("NetClient.dll")]
        public static extern bool VSNET_ClientStopMp4Capture(Int32 hHandle);

        //暂停本地MP4录像
        [DllImport("NetClient.dll")]
        public static extern bool VSNET_ClientPauseMp4Capture(Int32 hHandle);

        //从暂停中恢复本地MP4录像
        [DllImport("NetClient.dll")]
        public static extern bool VSNET_ClientMp4CaptureRestart(Int32 hHandle);

        //设置预录像 预览句柄  使用标志 缓冲区大小  缓存帧数
        [DllImport("NetClient.dll")]
        public static extern bool VSNET_ClientPrerecord(Int32 hHandle, bool m_benable, int m_buffsize, int m_framecount);

        //开始录像ASF文件
        [DllImport("NetClient.dll")]
        public static extern bool VSNET_ClientStartASFFileCap(Int32 hHandle, string m_FileName, bool m_bbroad);

        /// <summary>
        /// 停止本地ASF录像 
        /// </summary>
        /// <param name="hHandle"></param>
        /// <returns></returns>
        [DllImport("NetClient.dll")]
        public static extern bool VSNET_ClientStopCapture(Int32 hHandle);

        //暂停本地ASF录像 
        [DllImport("NetClient.dll")]
        public static extern bool VSNET_ClientPauseCapture(Int32 hHandle);

        //从暂停中恢复本地ASF录像 
        [DllImport("NetClient.dll")]
        public static extern bool VSNET_ClientCaptureRestart(Int32 hHandle);

        /// <summary>
        /// 抓图JPG图片并存放在服务器硬盘 
        /// </summary>
       
        [DllImport("NetClient.dll")]
        public static extern bool VSNET_ServerCapJPEG(string m_sername, string m_url, int m_ch, string m_username, string m_password, int m_quality, short wserport);

        /// <summary>
        /// 截图回调函数
        /// </summary>
        /// <param name="hHandle">连接句柄</param>
        /// <param name="m_ch">连接通道</param>
        /// <param name="pBuffer">jpeg缓冲区</param>
        /// <param name="size">大于0表示图片大小</param>
        /// <param name="userdata">用户自定义数据</param>
        public delegate void jpegdatacallback(Int32 hHandle, int m_ch, IntPtr pBuffer, int size, IntPtr userdata);

        [DllImport("NetClient.dll")]
        public static extern Int32 VSNET_ClientJpegCapStart(string m_sername, string m_url, string m_username, string m_password, short wserport, jpegdatacallback jpegCall, string userdata);
        //启动一次抓图回传
        [DllImport("NetClient.dll")]
        public static extern bool VSNET_ClientJpegCapSingle(Int32 hHandle, int m_ch, int m_quality);

        //停止JPEG抓图回传 
        [DllImport("NetClient.dll")]
        public static extern bool VSNET_ClientJpegCapStop(Int32 hHandle);

        //抓拍为BMP文件 
        [DllImport("NetClient.dll")]
        public static extern bool VSNET_ClientCapturePicture(Int32 hHandle, string m_filename);

    }
    
    #endregion


}
