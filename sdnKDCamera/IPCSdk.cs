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
        [DllImport("ipcsdk.dll")]
        public static extern IntPtr IPC_CreateHandle(long dwIP, int wPort, string pName, string pPassword);
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
        public static extern bool IPC_Login(IntPtr pHandle, string pName, string pPassword, ref long pErrorCode);
        /// <summary>
        /// 用户注销注册
        /// </summary>
        /// <param name="pHandle">前端句柄</param>
        /// <param name="pErrorCode"></param>
        /// <returns></returns>
        [DllImport("ipcsdk.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool IPC_Logout(IntPtr pHandle, ref long pErrorCode);
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
        public static extern bool IPC_GetCap(IntPtr pHandle, int nCapLen, ref string apCapName, ref int adwCapOut, ref long pErrorCode);

        #endregion

        #region uniplay.dll 函数封装
        /// <summary>
        /// 初始化整个SDK环境 
        /// </summary>
        /// <returns></returns>
        [DllImport("uniplay.dll")]
        public static extern bool PLAYKD_Startup();
        /// <summary>
        /// 初始化整个SDK环境 
        /// </summary>
        /// <returns></returns>
        [DllImport("uniplay.dll")]
        public static extern bool PLAYKD_Cleanup();
        /// <summary>
        /// 获取通道号
        /// </summary>
        /// <param name="szCompany">厂商名</param>
        /// <param name="bHw">是否启动硬件加速</param>
        /// <param name="ppPort">通道号</param>
        /// <returns>成功返回TRUE；失败返回FALSE</returns>
        [DllImport("uniplay.dll")]
        public static extern bool PLAYKD_GetPort(string szCompany, bool bHw, ref int ppPort);
        /// <summary>
        /// 释放通道号
        /// </summary>
        /// <param name="nPort">通道号</param>
        [DllImport("uniplay.dll")]
        public static extern void PLAYKD_FreePort(int nPort);
        /// <summary>
        /// 打开流,开始准备媒体流播放，支持媒体流
        /// </summary>
        /// <param name="nPort">通道号</param>
        /// <param name="pHead">文件头缓冲地址</param>
        /// <param name="nHeadLen">文件头长度</param>
        /// <param name="nbufferlen"> 视频未解码缓冲区大小，以字节为单位，取值范围50K-100M，即[512*1024/10,512*1024*200]</param>
        /// <returns>成功返回TRUE；失败返回FALSE</returns>
        [DllImport("uniplay.dll")]
        public static extern bool PLAYKD_OpenStream(int nPort, string pHead, int nHeadLen, int nbufferlen);
        /// <summary>
        /// 播放声音,默认关闭
        /// </summary>
        /// <param name="nPort">通道号</param>
        /// <returns>成功返回TRUE；失败返回FALSE</returns>
        [DllImport("uniplay.dll")]
        public static extern bool PLAYKD_PlaySound(int nPort);
        /// <summary>
        /// 关闭指定通道号的声音
        /// </summary>
        /// <param name="nPort">通道号</param>
        /// <returns></returns>
        [DllImport("uniplay.dll")]
        public static extern bool PLAYKD_StopSound(int nPort);
        /// <summary>
        /// 开启本地录像，支持文件，媒体流  该接口必须在PLAYKD_PLAY之后调用
        /// </summary>
        /// <param name="nPort">通道号</param>
        /// <param name="szRecFileName">录像的本地文件名</param>
        /// <param name="nRecodeType"> 录像媒体类型，默认为0，自动根据文件后缀名来探测</param>
        /// <returns>成功返回TRUE；失败返回FALSE</returns>
        [DllImport("uniplay.dll")]
        public static extern bool PLAYKD_StartLocalRecord(int nPort, string szRecFileName, int nRecodeType);
        /// <summary>
        /// 停止本地录像，支持文件，媒体流
        /// </summary>
        /// <param name="nPort">通道号</param>
        /// <returns></returns>
        [DllImport("uniplay.dll")]
        public static extern bool PLAYKD_StopLocalRecord(int nPort);

        #endregion

    }
}
