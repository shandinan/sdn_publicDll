using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sdnHIKCamera
{
    /// <summary>
    /// DVR 录像 控制，录制到前端（前端设备存储上）
    /// </summary>
    public class DvrRecord
    {
        //dvr录像参数
        private dvr_record_param _dvr_record_param;
        public DvrRecord(dvr_record_param dvr_record_param)
        {
            this._dvr_record_param = dvr_record_param;
        }
        /// <summary>
        /// dvr 录像开始，前端设备录像（前端存储上）
        /// </summary>
        /// <param name="strMsg"></param>
        /// <returns></returns>
        public bool start_dvrRecord(out string strMsg)
        {
            if (_dvr_record_param.lUserId < 0)
            {
                strMsg = "请先登录";
                return false;
            }
            bool blRes = CHCNetSDK.NET_DVR_StartDVRRecord(_dvr_record_param.lUserId, _dvr_record_param.lChannel, _dvr_record_param.lRecordType);
            if (blRes)
            {
                strMsg = "成功";
            }
            else
            {
                strMsg = "失败";
            }
            return blRes;
        }
        /// <summary>
        /// dvr 录像结束，前端设备录像结束（前端存储上）
        /// </summary>
        /// <param name="strMsg"></param>
        /// <returns></returns>
        public bool stop_dvrRecord(out string strMsg)
        {
            if (_dvr_record_param.lUserId < 0)
            {
                strMsg = "请先登录";
                return false;
            }
            bool blRes = CHCNetSDK.NET_DVR_StopDVRRecord(_dvr_record_param.lUserId, _dvr_record_param.lChannel);
            if (blRes)
            {
                strMsg = "成功";
            }
            else
            {
                strMsg = "失败";
            }
            return blRes;
        }
    }
}
