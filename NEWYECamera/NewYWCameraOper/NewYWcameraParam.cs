using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewYWCameraOper
{
    public class NewYWcameraParam
    {
        private string _pServerIP;
        /// <summary>
        /// 服务地址 IP
        /// </summary>
        public string pServerIP
        {
            get { return this._pServerIP; }
            set { this._pServerIP = value; }
        }
        private ushort _pServerPort;
        /// <summary>
        /// 通讯端口号
        /// </summary>
        public ushort pServerPort
        {
            get { return this._pServerPort; }
            set { this._pServerPort = value; }
        }

        private string _pDeviceName;
        /// <summary>
        /// DVS设备名称
        /// </summary>
        public string pDeviceName
        {
            get { return this._pDeviceName; }
            set { this._pDeviceName = value; }
        }
        /// <summary>
        /// 登录DVS用户名
        /// </summary>
        private string _pUserName;
        public string pUserName
        {
            get { return this._pUserName; }
            set { this._pUserName = value; }
        }
        private string _pUserPassword;
        /// <summary>
        /// 登录DVS密码
        /// </summary>
        public string pUserPassword
        {
            get { return this._pUserPassword; }
            set { this._pUserPassword = value; }
        }

        private uint _dwClientID;
        /// <summary>
        /// 回调参数（连接号）
        /// </summary>
        public uint dwClientID
        {
            get { return this._dwClientID; }
            set { this._dwClientID = value; }
        }
        private IntPtr _hLogonServer;
        /// <summary>
        /// 登录DVS 返回的句柄
        /// </summary>
        public IntPtr hLogonServer
        {
            get { return this._hLogonServer; }
            set { this._hLogonServer = value; }
        }
        private IntPtr _hNotifyWindow;
        /// <summary>
        /// 消息通知的句柄
        /// </summary>
        public IntPtr hNotifyWindow
        {
            get { return this._hNotifyWindow; }
            set { this._hNotifyWindow = value; }
        }

        private IntPtr _open_channel_info;
        /// <summary>
        /// 打开通道参数
        /// </summary>
        public IntPtr Open_Channel_Info
        {
            get { return this._open_channel_info; }
            set { this._open_channel_info = value; }
        }

        private string _VideoFileName;
        /// <summary>
        /// 录像文件路径
        /// </summary>
        public string VideoFileName
        {
            get { return this._VideoFileName; }
            set { this._VideoFileName = value; }
        }

        private string _ImgFileName;
        /// <summary>
        /// 截图文件名
        /// </summary>
        public string ImgFileName
        {
            get { return this._ImgFileName; }
            set { this._ImgFileName = value; }
        }


    }
}
