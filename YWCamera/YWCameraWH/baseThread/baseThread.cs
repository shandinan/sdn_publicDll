using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YWCameraWH.CameraOpe;

namespace YWCameraWH.baseThread
{
    public delegate void ThreadHandle(Camera_Data dd, IntPtr ihandle);
    public delegate void ThreadHandle_null();
    public delegate void ThreadHandle_camera(Camera_Data video_camerqa);
    public delegate void ThreadHandle_record(string strIp, string strCardNoa);
}
