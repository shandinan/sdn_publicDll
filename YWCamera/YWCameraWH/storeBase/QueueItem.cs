using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YWCameraWH.CameraOpe;

namespace YWCameraWH.storeBase
{
    public class QueueItem
    {
        private int _cameraId;
        /// <summary>
        /// 摄像头ID
        /// </summary>
        public int cameraId
        {
            get { return _cameraId; }
            set { this._cameraId = value; }
        }
        private int _luserId;
        /// <summary>
        /// 登录Id
        /// </summary>
        public int luserId
        {
            get { return _luserId; }
            set { this._luserId = value; }
        }
        private int _lchannel;
        /// <summary>
        /// 摄像头通道号
        /// </summary>
        public int lchannel
        {
            get { return _lchannel; }
            set { this._lchannel = value; }
        }
        private int _lRealHandle;
        /// <summary>
        /// 实时预览句柄
        /// </summary>
        public int lRealHandle
        {
            get { return _lRealHandle; }
            set { this._lRealHandle = value; }
        }

        private IntPtr _controlHandle;
        /// <summary>
        /// 预览句柄
        /// </summary>
        public IntPtr controlHandle
        {
            get { return _controlHandle; }
            set { this._controlHandle = value; }
        }
        private int _iFlag;
        /// <summary>
        /// 批次号标志
        /// </summary>
        public int iFlag
        {
            get { return this._iFlag; }
            set { this._iFlag = value; }
        }

        private int _videoId;
        /// <summary>
        /// 录像记录id
        /// </summary>
        public int videoId
        {
            get { return this._videoId; }
            set { this._videoId = value; }
        }

        private int _usedState;
        /// <summary>
        /// 使用状态0:未使用；1:已经使用
        /// </summary>
        public int usedState
        {
            get { return _usedState; }
            set { this._usedState = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        private Camera_Data _camera_Data;
        /// <summary>
        /// 摄像头信息类
        /// </summary>
        public Camera_Data camera_Data
        {
            get { return this._camera_Data; }
            set { this._camera_Data = value; }
        }
        /// <summary>
        /// 摄像头操作类
        /// </summary>
        private CameraOper _camera_oper;
        /// <summary>
        /// 摄像头操作类
        /// </summary>
        public CameraOper camera_oper
        {
            get { return this._camera_oper; }
            set { this._camera_oper = value; }
        }

    }
}
