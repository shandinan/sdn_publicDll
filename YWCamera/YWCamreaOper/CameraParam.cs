using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YWCamreaOper
{
    /*
     * 摄像头参数
     * */
    public class CameraParam
    {
        //播放用的缓冲大小
        private int _m_buffnum;
        /// <summary>
        /// 播放用的缓冲大小
        /// </summary>
        public int m_buffnum
        {
            get { return this._m_buffnum; }
            set { this._m_buffnum = value; }
        }
        //连接服务器的通道
        private byte _m_ch;
        /// <summary>
        /// 连接服务器的通道
        /// </summary>
        public byte m_ch
        {
            get { return this._m_ch; }
            set { this._m_ch = value; }
        }
        //播放窗口句柄
        private IntPtr _m_hVideohWnd;
        /// <summary>
        /// 播放窗口句柄
        /// </summary>
        public IntPtr m_hVideohWnd
        {
            get { return this._m_hVideohWnd; }
            set { this._m_hVideohWnd = value; }
        }
        //通道消息通知窗口句柄，可以为NULL
        private IntPtr _m_hChMsgWnd;
        /// <summary>
        /// 通道消息通知窗口句柄，可以为NULL
        /// </summary>
        public IntPtr m_hChMsgWnd
        {
            get { return this._m_hChMsgWnd; }
            set { this._m_hChMsgWnd = value; }
        }
        //通道消息号
        private uint _m_nChmsgid;
        /// <summary>
        /// 通道消息号
        /// </summary>
        public uint m_nChmsgid
        {
            get { return this._m_nChmsgid; }
            set { this._m_nChmsgid = value; }
        }
        //服务器名称
        private string _m_sername;
        /// <summary>
        /// 服务器名称
        /// </summary>
        public string m_sername
        {
            get { return this._m_sername; }
            set { this._m_sername = value; }
        }
        //用户名
        private string _m_username;
        /// <summary>
        /// 用户名
        /// </summary>
        public string m_username
        {
            get { return this._m_username; }
            set { this._m_username = value; }
        }
        //密码
        private string _m_password;
        /// <summary>
        /// 密码
        /// </summary>
        public string m_password
        {
            get { return this._m_password; }
            set { this._m_password = value; }
        }
        //是否启动播放窗口
        private ushort _m_playstart;
        /// <summary>
        /// 是否启动播放窗口
        /// </summary>
        public ushort m_playstart
        {
            get { return this._m_playstart; }
            set { this._m_playstart = value; }
        }
        //连接模式，1：UDP方式，2：多播方式，3：TCP方式
        private ushort _m_tranType;
        /// <summary>
        /// 连接模式，1：UDP方式，2：多播方式，3：TCP方式
        /// </summary>
        public ushort m_tranType
        {
            get { return this._m_tranType; }
            set { this._m_tranType = value; }
        }
        //OVERLAY使用标志 默认0
        private int _m_useoverlay;
        /// <summary>
        /// OVERLAY使用标志
        /// </summary>
        public int m_useoverlay
        {
            get { return this._m_useoverlay; }
            set { this._m_useoverlay = value; }
        }
        //URL
        private string _url;
        /// <summary>
        /// URL
        /// </summary>
        public string url
        {
            get { return this._url; }
            set { this._url = value; }
        }
        /// <summary>
        /// 摄像头端口
        /// </summary>
        private string _port;
        /// <summary>
        /// 摄像头端口
        /// </summary>
        public string port
        {
            get { return this._port; }
            set { this._port = value; }
        }
        //回调函数
        private m_messagecallback _callback;
        /// <summary>
        /// 回调函数
        /// </summary>
        public m_messagecallback callback
        {
            get { return this._callback; }
            set { this._callback = value; }
        }
    }
}
