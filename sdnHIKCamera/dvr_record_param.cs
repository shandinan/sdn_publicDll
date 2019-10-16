using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sdnHIKCamera
{
    /// <summary>
    /// 控制客户端录像参数
    /// </summary>
    public class dvr_record_param
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

        //录像类型：0- 手动
        private int _lRecordType;
        /// <summary>
        /// 录像类型：0- 手动，1- 报警，2- 回传，3- 信号，4- 移动，5- 遮挡 
        /// </summary>
        public int lRecordType
        {
            set { _lRecordType = value; }
            get { return _lRecordType; }
        }
    }
}
