using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sdnHIKCamera
{
    /// <summary>
    /// 回访硬盘录像机信息  NET_DVR_FindFile_V40
    /// </summary>
    public class playBackNvr
    {
        private int m_lDownHandle = -1;//当前下载 
        private playBackNvr_param _playBackNvr_param;

        public playBackNvr(playBackNvr_param playBackNvr_param)
        {
            this._playBackNvr_param = playBackNvr_param;
        }
        /// <summary>
        /// 查找
        /// </summary>
        /// <returns></returns>
        public int FindFile_Nvr()
        {
            return 0;
        }

        /// <summary>
        /// 根据时间下载文件
        /// </summary>
        public bool downFileByTime(out string strMsg)
        {
            if (m_lDownHandle >= 0)
            {
                strMsg = "正在下载中";
                return false;
            }
            CHCNetSDK.NET_DVR_PLAYCOND struDownPara = new CHCNetSDK.NET_DVR_PLAYCOND();
            struDownPara.dwChannel = (uint)_playBackNvr_param.lChannel; //通道号 Channel number  

            //设置下载的开始时间 Set the starting time
            struDownPara.struStartTime.dwYear = (uint)_playBackNvr_param.startTime.Year;
            struDownPara.struStartTime.dwMonth = (uint)_playBackNvr_param.startTime.Month;
            struDownPara.struStartTime.dwDay = (uint)_playBackNvr_param.startTime.Day;
            struDownPara.struStartTime.dwHour = (uint)_playBackNvr_param.startTime.Hour;
            struDownPara.struStartTime.dwMinute = (uint)_playBackNvr_param.startTime.Minute;
            struDownPara.struStartTime.dwSecond = (uint)_playBackNvr_param.startTime.Second;

            //设置下载的结束时间 Set the stopping time
            struDownPara.struStopTime.dwYear = (uint)_playBackNvr_param.endTime.Year;
            struDownPara.struStopTime.dwMonth = (uint)_playBackNvr_param.endTime.Month;
            struDownPara.struStopTime.dwDay = (uint)_playBackNvr_param.endTime.Day;
            struDownPara.struStopTime.dwHour = (uint)_playBackNvr_param.endTime.Hour;
            struDownPara.struStopTime.dwMinute = (uint)_playBackNvr_param.endTime.Minute;
            struDownPara.struStopTime.dwSecond = (uint)_playBackNvr_param.endTime.Second;

            //按时间下载 Download by time
            m_lDownHandle = CHCNetSDK.NET_DVR_GetFileByTime_V40(_playBackNvr_param.lUserId, _playBackNvr_param.sSavedFileName, ref struDownPara);
            if (m_lDownHandle < 0)
            {
                uint iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                strMsg = "NET_DVR_GetFileByTime_V40 失败, 错误码= " + iLastErr;
                //MessageBox.Show(str);
                return false;
            }
            strMsg = "成功";
            return true;
        }
    }
}
