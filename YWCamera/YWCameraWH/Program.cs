using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace YWCameraWH
{
    class Program
    {
       static DateTime dtNow = DateTime.Now.AddDays(-1);
       static Thread thrReadData = null; //添加数据进程
       static Thread startRec = null; //开始录像进程
       static int iHour = 7;//维护时间 小时
       static int iMin = 30;//维护时间 分钟
       static bool isFirstRun = true;//是否第一次启动程序
        static void Main(string[] args)
        {
            Common.ReadIniFile readIni = new Common.ReadIniFile(AppDomain.CurrentDomain.BaseDirectory + "\\config.ini");
            iHour = string.IsNullOrEmpty(readIni.ReadValue("datetime", "hour")) ? 7 : Convert.ToInt32(readIni.ReadValue("datetime", "hour"));
            iMin = string.IsNullOrEmpty(readIni.ReadValue("datetime", "min")) ? 30 : Convert.ToInt32(readIni.ReadValue("datetime", "min"));
            new Thread(startLook).Start();
           
        }
        private static void startLook()
        {
            while (true)
            {
                Common.SysLog.WriteOptDisk("线程检测一次", AppDomain.CurrentDomain.BaseDirectory);
                if (dtNow.Day != DateTime.Now.Day) //如果不是当天
                {
                    dtNow = DateTime.Now.AddDays(-1);
                    Console.WriteLine("更新时间（天数为前一天)");
                    if ((dtNow.Hour == iHour && dtNow.Minute > iMin) || isFirstRun) //dtNow.Hour == 7 && dtNow.Minute>30
                    {
                        if(isFirstRun)
                        {
                            Console.WriteLine("程序第一次开始,进行摄像头维护工作！！！---请勿关闭");
                            Common.SysLog.WriteOptDisk("程序第一次开始,进行摄像头维护工作！！！", AppDomain.CurrentDomain.BaseDirectory);
                        }
                        isFirstRun = false;//把第一次启动标记掉
                        Common.SysLog.WriteOptDisk("执行亿维摄像头维护进程-----------开始", AppDomain.CurrentDomain.BaseDirectory);
                        Console.WriteLine("执行亿维摄像头维护进程-----------开始---请勿关闭");
                        dtNow = DateTime.Now;
                        Console.WriteLine("更新时间(当前时间）"+dtNow.ToString());
                        //录像线程
                        RecordByDb rec = new RecordByDb();
                        thrReadData = new Thread(rec.sdnStartRecByDb);
                        startRec = new Thread(rec.sdnLoopStartRec);
                        thrReadData.Start();
                        Thread.Sleep(1000);
                        startRec.Start();
                    }
                    Common.SysLog.WriteOptDisk("未到有效时间", AppDomain.CurrentDomain.BaseDirectory);
                    Console.WriteLine("未到有效时间---请勿关闭");
                }
                else
                {
                    try
                    {
                        Common.SysLog.WriteOptDisk("执行亿维摄像头维护进程-----------本次结束", AppDomain.CurrentDomain.BaseDirectory);
                        Console.WriteLine("执行亿维摄像头维护进程-----------本次结束---请勿关闭");
                        Console.WriteLine("等待下次维护任务…………---请勿关闭");
                        Thread.Sleep(20000);//线程停止20秒
                        if (thrReadData != null && thrReadData.IsAlive)
                        {
                            thrReadData.Abort();
                            thrReadData = null;
                        }
                        if (startRec != null && startRec.IsAlive)
                        {
                            startRec.Abort();
                            startRec = null;
                        }
                        Thread.Sleep(2000);//线程停止2秒

                    }
                    catch (Exception ex)
                    {
                        Common.SysLog.WriteLog(ex, AppDomain.CurrentDomain.BaseDirectory);
                    }
                }
                Thread.Sleep(1200000);
            }
        }
        ~Program()
        {
            
        }
    }
}
