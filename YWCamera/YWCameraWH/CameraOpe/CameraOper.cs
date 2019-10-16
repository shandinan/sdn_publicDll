using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YWCameraWH.CameraOpe
{
    public abstract class CameraOper
    {
        /// <summary>
        /// 加载SDK
        /// </summary>
        public abstract int InitSDK();
        /// <summary>
        /// 卸载SDK
        /// </summary>
        public abstract int FreeSDK();
        /// <summary>
        /// 摄像头登录
        /// </summary>
        /// <returns></returns>
        public abstract int Login();
        /// <summary>
        /// 摄像头登录注销
        /// </summary>
        /// <returns></returns>
        public abstract int LogOut();
        /// <summary>
        /// 开始预览
        /// </summary>
        public abstract int StartPreview();
        /// <summary>
        /// 停止预览
        /// </summary>
        public abstract int StopPreview();
        /// <summary>
        /// 开始录像
        /// </summary>
        public abstract int StartRecord();
        /// <summary>
        /// 停止录像
        /// </summary>
        public abstract int StopRecord();
        /// <summary>
        /// 摄像头截图
        /// </summary>
        public abstract int CapImgs();
    }
}
