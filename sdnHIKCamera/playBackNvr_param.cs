using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sdnHIKCamera
{
    /// <summary>
    /// NVR录像回访下载
    /// </summary>
    public class playBackNvr_param
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
        private int _lChannel;
        /// <summary>
        /// 预览的设备通道 
        /// </summary>
        public int lChannel
        {
            set { _lChannel = value; }
            get { return _lChannel; }
        }
        private string _sSavedFileName;
        /// <summary>
        /// 保存本地的文件名称（全名称）
        /// </summary>
        public string sSavedFileName
        {
            set { _sSavedFileName = value; }
            get { return _sSavedFileName; }
        }

        private DateTime _startTime;
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime startTime
        {
            set { _startTime = value; }
            get { return _startTime; }
        }
        private DateTime _endTime;
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime endTime
        {
            set { _endTime = value; }
            get { return _endTime; }
        }
    }
}
