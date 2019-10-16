using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YWCameraWH.CameraOpe
{
    public class HK_CameraOper : CameraOper
    {
        public HK_CameraOper(Camera_Data video_camera)
        {

        }
        /// <summary>
        /// 初始化SDK
        /// </summary>
        /// <returns></returns>
        public override int InitSDK()
        {
            return -1;
            throw new NotImplementedException();
        }
        /// <summary>
        /// 卸载SDK
        /// </summary>
        /// <returns></returns>
        public override int FreeSDK()
        {
            return -1;
            throw new NotImplementedException();
        }
        /// <summary>
        /// 登录(亿维摄像头不启用）
        /// </summary>
        /// <returns></returns>
        public override int Login()
        {
            return -1;
            throw new NotImplementedException();
        }
        /// <summary>
        /// 退出（亿维摄像头不启用）
        /// </summary>
        /// <returns></returns>
        public override int LogOut()
        {
            return -1;
            throw new NotImplementedException();
        }
        /// <summary>
        /// 开始预览（为了统一，这里当作开始录像使用）
        /// </summary>
        /// <returns></returns>
        public override int StartPreview()
        {
            return -1;
            throw new NotImplementedException();
        }
        /// <summary>
        /// 停止预览(为了统一，这里当作停止录像使用）
        /// </summary>
        /// <returns></returns>
        public override int StopPreview()
        {
            return -1;
            throw new NotImplementedException();
        }
        /// <summary>
        /// 开始录像
        /// </summary>
        /// <returns></returns>
        public override int StartRecord()
        {
            return -1;
            throw new NotImplementedException();
        }
        /// <summary>
        /// 停止录像
        /// </summary>
        /// <returns></returns>
        public override int StopRecord()
        {
            return -1;
            throw new NotImplementedException();
        }
        /// <summary>
        /// 摄像头截图
        /// </summary>
        /// <returns></returns>
        public override int CapImgs()
        {
            return -1;
            throw new NotImplementedException();
        }
    }
}
