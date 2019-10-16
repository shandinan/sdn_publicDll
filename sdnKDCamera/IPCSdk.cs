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
    }
}
