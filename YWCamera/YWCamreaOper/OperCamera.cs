/*
 * 亿维摄像头操作
 * */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace YWCamreaOper
{
    /**
     * 摄像头操作类
     * */
    public class OperCamera
    {
        private CameraParam cameraParam;//摄像头参数类
        private int handle = -1; //摄像头预览句柄

        public OperCamera(CameraParam cp)
        {
            cameraParam = cp;
        }

        #region 一、SDK初始化与关闭

        //public delegate string 
        /// <summary>
        /// 客户端初始化、加载
        /// </summary>
        public bool ClientInit(int WM_COMMAND)
        {
            return YWCamreaOper.YWCamera.VSNET_ClientStartup((uint)WM_COMMAND, IntPtr.Zero, null, IntPtr.Zero, IntPtr.Zero); //初始化加载SDK
         //  YWCamera.VSNET_ClientStartup()
        }
        /// <summary>
        /// 释放客户端 SDK
        /// </summary>
        /// <returns></returns>
        public bool ClientFree()
        {
            return YWCamera.VSNET_ClientCleanup();//释放SDK
        }
        /// <summary>
        /// 设置超时时间和尝试连接次数
        /// </summary>
        /// <param name="m_waitnum"></param>
        /// <param name="m_trynum"></param>
        /// <returns></returns>
        public bool ClientWaitTime(int m_waitnum, int m_trynum)
        {
            return YWCamera.VSNET_ClientWaitTime(m_waitnum, m_trynum);
        }

        #endregion

        #region 二、摄像头预览
        /// <summary>
        /// 预览摄像头
        /// </summary>
        public int CameraPreview()
        {
            if (cameraParam != null)
            {
                CHANNEL_CLIENTINFO info = new CHANNEL_CLIENTINFO();//客户端登录信息
                info.m_buffnum = cameraParam.m_buffnum;
                info.m_ch = cameraParam.m_ch;
                info.m_hChMsgWnd = cameraParam.m_hChMsgWnd;
                info.m_hVideohWnd = cameraParam.m_hVideohWnd;
                info.m_nChmsgid = cameraParam.m_nChmsgid;
                info.m_password = cameraParam.m_password;
                info.m_playstart = cameraParam.m_playstart;
                info.m_sername = cameraParam.m_sername;
                info.m_tranType = cameraParam.m_tranType;
                info.m_useoverlay = cameraParam.m_useoverlay;
                info.m_username = cameraParam.m_username;
                info.url = cameraParam.url;
                info.callback = cameraParam.callback;

                int iSizeOfStruct = Marshal.SizeOf(typeof(CHANNEL_CLIENTINFO));
                IntPtr pStructChannel = Marshal.AllocHGlobal(iSizeOfStruct);
                Marshal.StructureToPtr(info, pStructChannel, false);

                handle = YWCamreaOper.YWCamera.VSNET_ClientStart(cameraParam.url, pStructChannel, Convert.ToInt16(cameraParam.port), 0);//预览摄像头
                return handle;
            }
            else
            {
                return -1;
            }
        }
        /// <summary>
        /// 根据预览句柄，停止预览
        /// </summary>
        /// <param name="iHandle"></param>
        public bool PreviewStop(int iHandle)
        {
            if (iHandle < 0)
            {
                iHandle = handle;
            }
            return YWCamera.VSNET_ClientStop(iHandle);
        }
        
        #endregion

        #region 三、录像

        /// <summary>
        /// 开始录像
        /// </summary>
        /// <param name="iHandle"></param>
        /// <param name="strFileName"></param>
        /// <returns></returns>
        public bool StartRec(int iHandle,string strFileName)
        {
            if (iHandle <0)
            {
                iHandle = handle;
            }
            return YWCamera.VSNET_ClientStartASFFileCap(iHandle, strFileName, false);
        }
        /// <summary>
        /// 停止录像
        /// </summary>
        /// <param name="iHandle"></param>
        /// <returns></returns>
        public bool StopRec(int iHandle)
        {
            if (iHandle < 0)
            {
                iHandle = handle;
            }
            return YWCamera.VSNET_ClientStopCapture(iHandle);
        }
        /// <summary>
        /// 暂停录像
        /// </summary>
        /// <param name="iHandle"></param>
        /// <returns></returns>
        public bool PauseRec(int iHandle)
        {
            if (iHandle < 0)
            {
                iHandle = handle;
            }
            return YWCamera.VSNET_ClientPauseCapture(iHandle);
        }
        /// <summary>
        /// 恢复录像
        /// </summary>
        /// <param name="iHandle"></param>
        /// <returns></returns>
        public bool ReStartRec(int iHandle)
        {
            if (iHandle < 0)
            {
                iHandle = handle;
            }
            return YWCamera.VSNET_ClientCaptureRestart(iHandle);
        }
        /// <summary>
        /// 设置预录像
        /// </summary>
        /// <param name="iHandle"></param>
        /// <param name="m_benable"></param>
        /// <param name="m_buffsize"></param>
        /// <param name="m_framecount"></param>
        /// <returns></returns>
        public bool YLRecVideo(int iHandle, bool m_benable, int m_buffsize, int m_framecount)
        {
            if (iHandle < 0)
            {
                iHandle = handle;
            }
            return YWCamera.VSNET_ClientPrerecord(iHandle, m_benable, m_buffsize, m_framecount);
        }
        
        #endregion

        #region 四、拍照

        /// <summary>
        /// 启动截图回传
        /// </summary>
        /// <param name="jpegCall"></param>
        /// <param name="userdata"></param>
        /// <returns></returns>
        public int JpegCapStart(YWCamera.jpegdatacallback jpegCall, string userdata)
        {
            if(cameraParam != null)
            {
                return YWCamera.VSNET_ClientJpegCapStart(cameraParam.m_sername, cameraParam.url, cameraParam.m_username, cameraParam.m_password, Convert.ToInt16(cameraParam.port),
                    jpegCall, userdata);
            }
            else
            {
                return -1;
            }
           
        }
        /// <summary>
        /// 启动一次截图程序
        /// </summary>
        /// <param name="iHandle"></param>
        /// <param name="m_ch"></param>
        /// <param name="m_quality"></param>
        /// <returns></returns>
        public bool CapJpegSingle(int iHandle, int m_ch, int m_quality)
        {
            return YWCamera.VSNET_ClientJpegCapSingle(iHandle,m_ch,m_quality);
        }
        /// <summary>
        /// 关闭截图回传事件
        /// </summary>
        /// <param name="iHandle"></param>
        /// <returns></returns>
        public bool CapJpegStop(int iHandle)
        {
            return YWCamera.VSNET_ClientJpegCapStop(iHandle);
        }
      
        #endregion

    }
}
