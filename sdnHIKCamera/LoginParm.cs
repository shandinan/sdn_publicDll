
/*
 * 此类是摄像头登录和退出方法的封装
 * 作者：单氐楠
 * 日期：2014-12-05
 * 注意：未经本人同意切换修改
 * */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sdnHIKCamera
{
    public class LoginParm
    {
        private string _login_Name;
        /// <summary>
        /// 登录名
        /// </summary>
        public string Login_Name
        {
            set { _login_Name = value; }
            get { return _login_Name; }
        }
        private string _login_password;
        /// <summary>
        /// 登录密码
        /// </summary>
        public string Login_Password
        {
            set { _login_password = value; }
            get { return _login_password; }
        }
        private string _login_ip;
        /// <summary>
        /// 登录ip
        /// </summary>
        public string Login_Ip
        {
            set { _login_ip = value; }
            get { return _login_ip; }
        }
        private string _login_port;
        /// <summary>
        /// 登录端口
        /// </summary>
        public string Login_Port
        {
            set { _login_port = value; }
            get { return _login_port; }
        }
        private int _login_channel;
        /// <summary>
        /// 登录通道号
        /// </summary>
        public int Login_Channel
        {
            set { _login_channel = value; }
            get { return _login_channel; }
        }
        private int _cameraID;
        /// <summary>
        /// 摄像头id
        /// </summary>
        public int CameraID
        {
            set { _cameraID = value; }
            get { return _cameraID; }
        }
        private int _luserId;
        /// <summary>
        /// 用户id
        /// </summary>
        public int lUserId
        {
            set { this._luserId = value; }
            get { return this._luserId; }

        }
        private int _screenNum;
        /// <summary>
        /// 屏幕号
        /// </summary>
        public int screenNum
        {
            set { this._screenNum = value; }
            get { return this._screenNum; }
        }
    }
}
