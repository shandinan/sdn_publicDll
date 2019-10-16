using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace SdnCtrlCount
{
    public class sdnCtrlCount
    {
        #region 电子锁
        /// <summary>
        /// 获取控制器数量
        /// </summary>
        /// <returns></returns>
        [DllImport("HIDCtrl.dll", EntryPoint = "GetCtrlCount", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetCtrlCount();
        [DllImport("HIDCtrl.dll", EntryPoint = "PowerOn1", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int PowerOn1();
        [DllImport("HIDCtrl.dll", EntryPoint = "PowerOff1", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int PowerOff1();
        [DllImport("HIDCtrl.dll", EntryPoint = "PowerOnEx1", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int PowerOnEx1();
        [DllImport("HIDCtrl.dll", EntryPoint = "PowerOff1", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int PowerOffEx1();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [DllImport("HIDCtrl.dll", EntryPoint = "PowerStatus1", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int PowerStatus1();
        #endregion

        #region 门磁计数
         //状态相关
        [DllImport("FreqDLL.dll", EntryPoint = "GetPowerCount", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetPowerCount();
        [DllImport("FreqDLL.dll", EntryPoint = "GetPower", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetPower(int Index);
        [DllImport("FreqDLL.dll", EntryPoint = "GetPower1", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetPower1();
        [DllImport("FreqDLL.dll", EntryPoint = "GetPower2", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetPower2();

        //频率相关
        [DllImport("FreqDLL.dll", EntryPoint = "GetFreqCount", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetFreqCount();
        [DllImport("FreqDLL.dll", EntryPoint = "GetFreq", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetFreq(int Index);
        [DllImport("FreqDLL.dll", EntryPoint = "GetFreq1", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetFreq1();
        [DllImport("FreqDLL.dll", EntryPoint = "GetFreq2", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetFreq2();

        //计数相关
        [DllImport("FreqDLL.dll", EntryPoint = "InitCount", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern void InitCount();
        [DllImport("FreqDLL.dll", EntryPoint = "GetCountCount", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetCountCount();
        [DllImport("FreqDLL.dll", EntryPoint = "GetCount", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetCount(int Index);
        [DllImport("FreqDLL.dll", EntryPoint = "GetCount1", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetCount1();
        [DllImport("FreqDLL.dll", EntryPoint = "GetCount2", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetCount2();

        [DllImport("FreqDLL.dll", EntryPoint = "GetIDEx", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetIDEx(int Index, [MarshalAs(UnmanagedType.LPStr)]StringBuilder APChar,
            ref int APCharLen);

        #endregion
    }
}
