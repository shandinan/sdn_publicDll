using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using YWCameraWH.CameraOpe;

namespace YWCameraWH.baseThread
{
    public class BaseThread_Came
    {
        protected bool m_IsRun;
        protected int m_Num;
        protected Thread m_th;

        protected Camera_Data cameData;


        public event ThreadHandle_camera ThreadEvent;


        public BaseThread_Came(int num, Camera_Data rtcd)
        {
            this.m_Num = num;
            this.cameData = rtcd;
        }

        protected virtual void BeginWork()
        {
            //while (this.m_IsRun)
            //{
            if (this.ThreadEvent != null)
            {
                this.ThreadEvent(cameData);
            }
            Thread.Sleep(this.m_Num);
            //}
        }

        public virtual void StartThread()
        {
            this.m_IsRun = true;
            this.m_th = new Thread(new ThreadStart(this.BeginWork));//定义一个线程并绑定方法beginwork
            this.m_th.Start();//启动线程
        }

        public virtual void StopThread()
        {
            this.m_IsRun = false;
            this.m_th.Abort();
        }
    }
}
