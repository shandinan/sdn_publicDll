/*
 * 此类是摄像头预览及取流方法
 * 作者：单氐楠
 * 日期：2014-12-05
 * 注意：未经本人同意切换修改
 * */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace sdnHIKCamera
{//PreviewView
    public class PreviewView
    {
        private PlayVideoParm _playVideoParm;
        private int m_lRealHandle = -1;
        private uint iLastErr = 0;
        public PreviewView(PlayVideoParm playVideoParm)
        {
            this._playVideoParm = playVideoParm;
        }
        public int PlayVideoDll(out string strMsg)
        {
            int m_lUserID = _playVideoParm.lUserId;
            if (m_lUserID < 0)
            {
                strMsg = "请登录设备";
                return m_lRealHandle;
            }

            if (m_lRealHandle < 0)
            {
                //System.Windows.Forms.PictureBox picBox = new System.Windows.Forms.PictureBox();
                //sdnTest.Child = picBox;

                CHCNetSDK.NET_DVR_PREVIEWINFO lpPreviewInfo = new CHCNetSDK.NET_DVR_PREVIEWINFO();
                lpPreviewInfo.hPlayWnd = _playVideoParm.intControls;//预览窗口 
                //  lpPreviewInfo.hPlayWnd = new WindowInteropHelper(this).Handle;
                lpPreviewInfo.lChannel = _playVideoParm.lChannel;//预览的设备通道 
                lpPreviewInfo.dwStreamType = _playVideoParm.dwStreamType;//码流类型：0-主码流，1-子码流，2-码流3，3-码流4，以此类推
                lpPreviewInfo.dwLinkMode = _playVideoParm.dwLinkMode;//连接方式：0- TCP方式，1- UDP方式，2- 多播方式，3- RTP方式，4-RTP/RTSP，5-RSTP/HTTP 
                lpPreviewInfo.bBlocked = _playVideoParm.bBlocked; //0- 非阻塞取流，1- 阻塞取流

                CHCNetSDK.REALDATACALLBACK RealData = new CHCNetSDK.REALDATACALLBACK(RealDataCallBack);//预览实时流回调函数
                IntPtr pUser = new IntPtr();//用户数据 user data 

                //打开预览 Start live view 
                switch (_playVideoParm.previewType)
                {
                    case 0://显示视频，不取流
                        m_lRealHandle = CHCNetSDK.NET_DVR_RealPlay_V40(m_lUserID, ref lpPreviewInfo, null/*RealData*/, pUser);
                        break;
                    case 1://显示视频,取流
                        GCHandle.Alloc(RealData);  //防止被回收
                        m_lRealHandle = CHCNetSDK.NET_DVR_RealPlay_V40(m_lUserID, ref lpPreviewInfo, RealData, pUser);
                        break;
                    case 2://不显示视频，只取流
                         GCHandle.Alloc(RealData);  //防止被回收
                        m_lRealHandle = CHCNetSDK.NET_DVR_RealPlay_V40(m_lUserID, ref lpPreviewInfo, RealData, pUser);
                        break;
                    default: //其他情况，默认为普通预览，不取视频流
                         m_lRealHandle = CHCNetSDK.NET_DVR_RealPlay_V40(m_lUserID, ref lpPreviewInfo, null/*RealData*/, pUser);
                        break;
                }
                //m_lRealHandle = CHCNetSDK.NET_DVR_RealPlay_V40(m_lUserID, ref lpPreviewInfo, null/*RealData*/, pUser);
                if (m_lRealHandle < 0)
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    strMsg = "NET_DVR_RealPlay_V40 失败,错误代码为" + iLastErr; //预览失败，输出错误号
                }
                else
                {
                    strMsg = "预览成功";
                }
            }
            else
            {
                strMsg = "已经登录";
            }
            return m_lRealHandle;
        }
        public int PlayVideoDll_CallBack(CHCNetSDK.REALDATACALLBACK RealDataFun,out string strMsg)
        {
            int m_lUserID = _playVideoParm.lUserId;
            if (m_lUserID < 0)
            {
                strMsg = "请登录设备";
                return m_lRealHandle;
            }

            if (m_lRealHandle < 0)
            {
                //System.Windows.Forms.PictureBox picBox = new System.Windows.Forms.PictureBox();
                //sdnTest.Child = picBox;

                CHCNetSDK.NET_DVR_PREVIEWINFO lpPreviewInfo = new CHCNetSDK.NET_DVR_PREVIEWINFO();
                lpPreviewInfo.hPlayWnd = _playVideoParm.intControls;//预览窗口 
                //  lpPreviewInfo.hPlayWnd = new WindowInteropHelper(this).Handle;
                lpPreviewInfo.lChannel = _playVideoParm.lChannel;//预览的设备通道 
                lpPreviewInfo.dwStreamType = _playVideoParm.dwStreamType;//码流类型：0-主码流，1-子码流，2-码流3，3-码流4，以此类推
                lpPreviewInfo.dwLinkMode = _playVideoParm.dwLinkMode;//连接方式：0- TCP方式，1- UDP方式，2- 多播方式，3- RTP方式，4-RTP/RTSP，5-RSTP/HTTP 
                lpPreviewInfo.bBlocked = _playVideoParm.bBlocked; //0- 非阻塞取流，1- 阻塞取流

                CHCNetSDK.REALDATACALLBACK RealData = RealDataFun;//预览实时流回调函数
                IntPtr pUser = new IntPtr();//用户数据 user data 

                //打开预览 Start live view 
                switch (_playVideoParm.previewType)
                {
                    case 0://显示视频，不取流
                        m_lRealHandle = CHCNetSDK.NET_DVR_RealPlay_V40(m_lUserID, ref lpPreviewInfo, null/*RealData*/, pUser);
                        break;
                    case 1://显示视频,取流
                        GCHandle.Alloc(RealData);  //防止被回收
                        m_lRealHandle = CHCNetSDK.NET_DVR_RealPlay_V40(m_lUserID, ref lpPreviewInfo, RealData, pUser);
                        break;
                    case 2://不显示视频，只取流
                        GCHandle.Alloc(RealData);  //防止被回收
                        m_lRealHandle = CHCNetSDK.NET_DVR_RealPlay_V40(m_lUserID, ref lpPreviewInfo, RealData, pUser);
                        break;
                    default: //其他情况，默认为普通预览，不取视频流
                        m_lRealHandle = CHCNetSDK.NET_DVR_RealPlay_V40(m_lUserID, ref lpPreviewInfo, null/*RealData*/, pUser);
                        break;
                }
                //m_lRealHandle = CHCNetSDK.NET_DVR_RealPlay_V40(m_lUserID, ref lpPreviewInfo, null/*RealData*/, pUser);
                if (m_lRealHandle < 0)
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    strMsg = "NET_DVR_RealPlay_V40 失败,错误代码为" + iLastErr; //预览失败，输出错误号
                }
                else
                {
                    strMsg = "预览成功";
                }
            }
            else
            {
                strMsg = "已经登录";
            }
            return m_lRealHandle;
        }
        public int StopPlayVideo(int sdnRealHandle, out string strMsg)
        {
            //停止预览 Stop live view 
            if (!CHCNetSDK.NET_DVR_StopRealPlay(sdnRealHandle))
            {
                iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                strMsg = "NET_DVR_StopRealPlay 失败, 错误代码为 " + iLastErr;
                return -1;
            }
            strMsg = "停止成功";
            m_lRealHandle = -1;
            return 1;
        }
        /// <summary>
        /// 回调函数
        /// </summary>
        /// <param name="lRealHandle">预览句柄</param>
        /// <param name="dwDataType">数据流类型</param>
        /// <param name="pBuffer">数据缓冲区指针</param>
        /// <param name="dwBufSize">数据缓存区大小</param>
        /// <param name="pUser">用户数据</param>
        public void RealDataCallBack(Int32 lRealHandle, UInt32 dwDataType, IntPtr pBuffer, UInt32 dwBufSize, IntPtr pUser)
        {

               try
            {

                switch (dwDataType)
                {
                    case CHCNetSDK.NET_DVR_SYSHEAD: //系统头
                        IntPtr pntheader = (IntPtr)pBuffer;
                        byte[] sdnByteheader = new byte[((int)dwBufSize) + 1];
                        Marshal.Copy(pntheader, sdnByteheader, 0, (int)dwBufSize);
                       // SaveVideo.WriteOptDisk(sdnByteheader, "D"); //录制头文件
                        break;
                    case CHCNetSDK.NET_DVR_STREAMDATA:   //码流数据 /视频流数据（包括复合流和音视频分开的视频流数据）
                        byte[] sdnByte = new byte[(int)dwBufSize];
                        Marshal.Copy(pBuffer, sdnByte, 0, (int)dwBufSize);
                        SaveVideo.WriteOptDisk(sdnByte, _playVideoParm.videoName); //mp4文件

                        ///FFMPEG

                        break;
                    case CHCNetSDK.NET_DVR_AUDIOSTREAMDATA:   //音频流数据
                        string ss = "sdsddsfsdf";
                        break;

                    case CHCNetSDK.NET_DVR_STD_VIDEODATA:   //标准视频流数据

                        break;

                    case CHCNetSDK.NET_DVR_STD_AUDIODATA:  //标准音频流数据

                        break;
                    default :

                        break;
                }
            }
            catch
            { }
        }
    }
}
