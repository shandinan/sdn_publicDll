using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace sdnKDCamera
{
    /// <summary>
    /// 科达摄像头 IPCSdk
    /// </summary>
    public class IPCSdk
    {

        #region 结构体

        public struct tagRawData
        {

        }

        #endregion

        #region  ipcsdk.dll 函数封装

        /// <summary>
        /// 初始化 dll
        /// </summary>
        /// <param name="pzDLLName">初始化的动态库名称(如ipcsdk.dll,ipcmedia.dll)</param>
        /// <param name="wTelnetPort">Telnet端口(默认3000)</param>
        /// <param name="bOpenTelnet">是否开启Telnet(默认打开)</param>
        /// <param name="pErrorCode">错误码</param>
        /// <returns>成功返回true, 失败返回false，原因解析pErrorCode</returns>
        [DllImport("ipcsdk.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool IPC_InitDll(string pzDLLName, int wTelnetPort, int bOpenTelnet, ref long pErrorCode);
        /// <summary>
        /// 得到系统版本号 【不可用】
        /// </summary>
        /// <param name="szVersion"></param>
        /// <param name="nMaxLen"></param>
        /// <param name="pErrorCode"></param>
        /// <returns>成功返回true, 失败返回false，原因解析pErrorCode</returns>
        [DllImport("ipcsdk.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool IPC_GetVersion(out string szVersion, int nMaxLen, ref long pErrorCode);
        /// <summary>
        /// 释放SDK的相关资源
        /// </summary>
        /// <param name="pErrorCode">错误码</param>
        /// <returns>成功返回true, 失败返回false，原因解析pErrorCode</returns>
        [DllImport("ipcsdk.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool IPC_ReleaseDll(ref long pErrorCode);
        /// <summary>
        /// 设置打印端口
        /// </summary>
        /// <param name="wTelnetPort">Telnet端口(默认3300)</param>
        /// <param name="bOpenTelnet">是否开启Telnet(默认打开)</param>
        /// <param name="pErrorCode">错误码</param>
        /// <returns>成功返回true, 失败返回false，原因解析pErrorCode</returns>
        [DllImport("ipcsdk.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool IPC_SetTelnet(int wTelnetPort, int bOpenTelnet, ref long pErrorCode);
        /// <summary>
        /// 在访问监控前端前，需先行建立连接句柄对象
        /// </summary>
        /// <param name="dwIP">注册IP</param>
        /// <param name="wPort">注册端口(80端口)</param>
        /// <param name="pName">用户名</param>
        /// <param name="pPassword">用户密码</param>
        /// <returns>成功返回设备句柄, 失败返回错误码</returns>
        [DllImport("ipcsdk.dll",CallingConvention=CallingConvention.Cdecl)]
        public static extern long IPC_CreateHandle(uint dwIP, uint wPort, string pName, string pPassword);
        /// <summary>
        /// 销毁句柄
        /// </summary>
        /// <returns>成功返回TRUE，失败返回FALSE</returns>
        [DllImport("ipcsdk.dll")]
        public static extern bool IPC_DestroyHandle();
        /// <summary>
        /// 用户注册，注册信息与ip绑定，修改了ip后需要重新登录
        /// </summary>
        /// <param name="pHandle">前端句柄</param>
        /// <param name="pName">用户名</param>
        /// <param name="pPassword">用户密码</param>
        /// <param name="pErrorCode">错误码</param>
        /// <returns>成功返回true, 失败返回false，原因解析pErrorCode</returns>
        [DllImport("ipcsdk.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool IPC_Login(ref long pHandle, string pName, string pPassword, ref long pErrorCode);
        /// <summary>
        /// 用户注销注册
        /// </summary>
        /// <param name="pHandle">前端句柄</param>
        /// <param name="pErrorCode"></param>
        /// <returns></returns>
        [DllImport("ipcsdk.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool IPC_Logout(ref long pHandle, ref long pErrorCode);
        /// <summary>
        /// 获取设备指定的能力集
        /// </summary>
        /// <param name="pHandle">前端句柄</param>
        /// <param name="nCapLen">能力集数量</param>
        /// <param name="apCapName">能力集名称</param>
        /// <param name="adwCapOut">能力集值</param>
        /// <param name="pErrorCode">错误码</param>
        /// <returns>成功返回true, 失败返回false，原因解析pErrorCode</returns>
        [DllImport("ipcsdk.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool IPC_GetCap(ref long pHandle, int nCapLen, ref string apCapName, ref int adwCapOut, ref long pErrorCode);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pHandle"></param>
        /// <param name="pErrorCode"></param>
        /// <returns></returns>
        [DllImport("ipcsdk.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int IPC_DelConnectDetect(ref long pHandle, ref long pErrorCode);
        /*=================================================================
         函数名称: cbfConnectDetect
         功    能: 连接探测回调回调    ，IPC_AddConnectDetect
         参数说明: 
		  dwIP		-- 设备IP 
		  wPort		-- 设备http端口 
		  dwCBconnectType	-- 连接状态 emConnectState
		  dwData	-- 回传数据
		  dwDataLen -- 回传数据长度
		  pContext	-- 上下文
          返 回 值: 成功返回IPC_ERR_SUCCESS, 失败返回错误码
         =================================================================*/
        public delegate void cbfConnectDetect(long dwIP, int wPort, long dwHandle, long dwCBconnectType, long dwDataLen, long dwData, IntPtr pContext);

        [DllImport("ipcsdk.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int IPC_AddConnectDetect(ref long pHandle, int dwConnectTimeOut, int dwReConnectTimes, cbfConnectDetect pcbfFun, IntPtr pContext, ref long pErrorCode);

        #endregion

        #region uniplay.dll 函数封装
        /// <summary>
        /// 初始化整个SDK环境 
        /// </summary>
        /// <returns></returns>
        [DllImport("uniplay.dll",CallingConvention=CallingConvention.Cdecl)]
        public static extern bool PLAYKD_Startup();
        /// <summary>
        /// 初始化整个SDK环境 
        /// </summary>
        /// <returns></returns>
        [DllImport("uniplay.dll",CallingConvention=CallingConvention.Cdecl)]
        public static extern bool PLAYKD_Cleanup();
        /// <summary>
        /// 获取通道号
        /// </summary>
        /// <param name="szCompany">厂商名</param>
        /// <param name="bHw">是否启动硬件加速</param>
        /// <param name="ppPort">通道号</param>
        /// <returns>成功返回TRUE；失败返回FALSE</returns>
        [DllImport("uniplay.dll",CallingConvention=CallingConvention.Cdecl)]
        public static extern bool PLAYKD_GetPort(string szCompany, bool bHw, ref int ppPort);
        /// <summary>
        /// 释放通道号
        /// </summary>
        /// <param name="nPort">通道号</param>
        [DllImport("uniplay.dll",CallingConvention=CallingConvention.Cdecl)]
        public static extern void PLAYKD_FreePort(int nPort);
        /// <summary>
        /// 打开流,开始准备媒体流播放，支持媒体流
        /// </summary>
        /// <param name="nPort">通道号</param>
        /// <param name="pHead">文件头缓冲地址</param>
        /// <param name="nHeadLen">文件头长度</param>
        /// <param name="nbufferlen"> 视频未解码缓冲区大小，以字节为单位，取值范围50K-100M，即[512*1024/10,512*1024*200]</param>
        /// <returns>成功返回TRUE；失败返回FALSE</returns>
        [DllImport("uniplay.dll",CallingConvention=CallingConvention.Cdecl)]
        public static extern bool PLAYKD_OpenStream(int nPort, string pHead, int nHeadLen, int nbufferlen);
        /// <summary>
        /// 播放声音,默认关闭
        /// </summary>
        /// <param name="nPort">通道号</param>
        /// <returns>成功返回TRUE；失败返回FALSE</returns>
        [DllImport("uniplay.dll",CallingConvention=CallingConvention.Cdecl)]
        public static extern bool PLAYKD_PlaySound(int nPort);
        /// <summary>
        /// 关闭指定通道号的声音
        /// </summary>
        /// <param name="nPort">通道号</param>
        /// <returns></returns>
        [DllImport("uniplay.dll",CallingConvention=CallingConvention.Cdecl)]
        public static extern bool PLAYKD_StopSound(int nPort);
        /// <summary>
        /// 开启本地录像，支持文件，媒体流  该接口必须在PLAYKD_PLAY之后调用
        /// </summary>
        /// <param name="nPort">通道号</param>
        /// <param name="szRecFileName">录像的本地文件名</param>
        /// <param name="nRecodeType"> 录像媒体类型，默认为0，自动根据文件后缀名来探测</param>
        /// <returns>成功返回TRUE；失败返回FALSE</returns>
        [DllImport("uniplay.dll",CallingConvention=CallingConvention.Cdecl)]
        public static extern bool PLAYKD_StartLocalRecord(int nPort, string szRecFileName, int nRecodeType);
        /// <summary>
        /// 开启本地录像，支持文件，媒体流
        /// </summary>
        /// <param name="nPort">通道号</param>
        /// <param name="szRecFileName"> 录像的本地文件全路径,文件名以.asf或者.mp4结尾</param>
        /// <param name="nRecodeType">录像媒体类型，  0：将未解码buffer数据保存为ASF/MP4文件，由传入的文件名后缀决定；1：将当前CacheBuffer数据保存为ASF/MP4文件，由传入的文件名后缀决定；必须先调用PLAYKD_CachedDataTime接口，并且当前cache中有数据</param>
        /// <param name="iStreamType"> 要录制的码流类型，1 表示音频,2表示视频,0表示所有码流都录制,目前只支持0</param>
        /// <param name="iFileLen">文件大小超过该值时按命名规则重录一个新的文件，单位为1K字节</param>
        /// <param name="bFirstVideoFrame">录像文件第一帧是否要求为视频帧，1：第一帧必须是视频帧（若一直没有视频帧，则不会产生录像文件），0：不强制第一帧必须为视频帧，默认不强制。 </param>
        /// <returns>成功返回TRUE；失败返回FALSE</returns>
        [DllImport("uniplay.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool PLAYKD_StartLocalRecordExt(int nPort, string szRecFileName, int nRecodeType, int iStreamType, int iFileLen, bool bFirstVideoFrame);
        /// <summary>
        /// 停止本地录像，支持文件，媒体流
        /// </summary>
        /// <param name="nPort">通道号</param>
        /// <returns></returns>
        [DllImport("uniplay.dll",CallingConvention=CallingConvention.Cdecl)]
        public static extern bool PLAYKD_StopLocalRecord(int nPort);
        /// <summary>
        /// 配置LOG输出 
        /// </summary>
        /// <param name="iTarget">LOG输出目标。可以为TARGET_NULL、TARGET_PRINT、TARGET_TELNET、TARGET_FILE或者TARGET_LOGCAT，采用或的方式控制</param>
        /// <param name="iLevel">LOG输出级别。可以为UNILOG_ERR，UNILOG_WARNING,UNILOG_INFO或者UNILOG_DEBUG，采用或的方式控制。需要最详细信息时，需要以如下方式作为参数输入UNILOG_ERR|UNILOG_WARNING|UNILOG_INFO|UNILOG_DEBUG</param>
        /// <param name="pLogPath">LOG输出路径。只有在LOG输出目标有TARGET_FILE时，才检测该参数</param>
        /// <param name="iFileNum">输出LOG文件数。只有在LOG输出目标有TARGET_FILE时，才检测该参数。目前该参数无效。可填任意值</param>
        /// <param name="iModule">输出哪些模块的LOG。目前该参数无效，可填任意值</param>
        /// <returns></returns>
        [DllImport("uniplay.dll",CallingConvention=CallingConvention.Cdecl)]
        public static extern bool PLAYKD_SetLogConfig(int iTarget, int iLevel, string pLogPath, int iFileNum, int iModule);
        /// <summary>
        /// 开始解码，播放，支持媒体流，文件，再次调用的话，应该重置到初始状态.
        /// </summary>
        /// <param name="nPort">通道号</param>
        /// <param name="hwnd">播放窗口句柄，view(安卓)，UIView(ios)，hwnd(windows),linux(XID) </param>
        /// <returns></returns>
        [DllImport("uniplay.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool PLAYKD_Play(int nPort, IntPtr hwnd);

        //[DllImport("uniplay.dll",CallingConvention=CallingConvention.Cdecl)]
        //public static extern bool PLAYKD_InputVideoData(int nPort,)

        #endregion

    }
}
