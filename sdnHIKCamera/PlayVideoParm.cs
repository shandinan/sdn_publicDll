/*
 * 此类是摄像头预览方法参数
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
    public class PlayVideoParm
    {
        private int _lUserID;
        /// <summary>
        /// 用户登录SDK id
        /// </summary>
        public int lUserId
        {
            set { _lUserID = value; }
            get { return _lUserID; }
        }
        private IntPtr _intControls;
        /// <summary>
        /// 预览窗口 
        /// </summary>
        public IntPtr intControls
        {
            set { _intControls = value; }
            get { return _intControls; }
        }
        private int _lChannel;
        /// <summary>
        /// 预览的设备通道 
        /// </summary>
        public int lChannel
        {
            set { _lChannel = value; }
            get { return _lChannel; }
        }
        private uint _dwStreamType;
        /// <summary>
        /// 码流类型：0-主码流，1-子码流，2-码流3，3-码流4，以此类推
        /// </summary>
        public uint dwStreamType
        {
            set { _dwStreamType = value; }
            get { return _dwStreamType; }
        }
        private uint _dwLinkMode;
        /// <summary>
        /// 连接方式：0- TCP方式，1- UDP方式，2- 多播方式，3- RTP方式，4-RTP/RTSP，5-RSTP/HTTP 
        /// </summary>
        public uint dwLinkMode
        {
            set { _dwLinkMode = value; }
            get { return _dwLinkMode; }
        }
        private bool _bBlocked;
        /// <summary>
        /// 0- 非阻塞取流，1- 阻塞取流
        /// </summary>
        public bool bBlocked
        {
            set { _bBlocked = value; }
            get { return _bBlocked; }
        }

        private int _previewType;
        /// <summary>
        /// 视频预览方法
        /// 0：显示视频，不取流
        /// 1：显示视频，取流
        /// 2：不显示视频，取流
        /// </summary>
        public int previewType
        {
            set { _previewType = value; }
            get { return _previewType; }
        }

        private string _videoName;
        /// <summary>
        /// 录取文件存放位置，全路径，建议后缀为.mp4
        /// </summary>
        public string videoName
        {
            set { this._videoName = value; }
            get { return _videoName; }
        }
    }
}
