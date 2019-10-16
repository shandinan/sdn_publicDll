using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YWCameraWH.CameraOpe
{
    /// <summary>
    /// 摄像头基础数据
    /// </summary>
    public class Camera_Data
    {
        /// <summary>
        /// 摄像头种类
        /// </summary>
        public string Camera_Type { get; set; }
        /// <summary>
        /// 摄像头名称
        /// </summary>
        public string Camera_Name { get; set; }
        /// <summary>
        /// 摄像头ID
        /// </summary>
        public string Camera_Id { get; set; }
        /// <summary>
        /// 摄像头登录IP
        /// </summary>
        public string LoginIp { get; set; }
        /// <summary>
        /// 摄像头登录用户名
        /// </summary>
        public string LoginName { get; set; }
        /// <summary>
        /// 摄像头登录密码
        /// </summary>
        public string LoginPwd { get; set; }
        /// <summary>
        /// 摄像头登录端口号
        /// </summary>
        public string LoginPort { get; set; }
        /// <summary>
        /// 摄像头登录通道号
        /// </summary>
        public string LoginChannel { get; set; }
        /// <summary>
        /// 录像ID
        /// </summary>
        public int VideoTemp_Id { get; set; }
        /// <summary>
        /// 录像路径
        /// </summary>
        public string File_Path { get; set; }
        /// <summary>
        /// 截图存放路径
        /// </summary>
        public string Img_Paht { get; set; }
    }
}
