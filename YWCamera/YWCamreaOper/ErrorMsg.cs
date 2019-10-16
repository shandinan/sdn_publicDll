using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace YWCamreaOper
{
    /**
     * 错误信息
     * */
    public class ErrorMsg
    {
        public string strMsg = "";//返回信息
        public const int LAUMSG_LINKMSG = 1; //连接信息
        public const int LAUMSG_VIDEOMOTION = 2; //视频移动报警消息
        public const int LAUMSG_VIDEOLOST = 3; //视频丢失消息
        public const int LAUMSG_ALARM = 4; //探头报警开始消息
        public const int LAUMSG_OUTPUTSTATUS = 5; //报警输出状态消息
        public const int LAUMSG_CURSWITCHCHAN = 6; //通道切换消息
        public const int LAUMSG_HIDEALARM = 7; //视频遮挡报警消息
        public const int LAUMSG_SERVERRECORD = 11;//摄像头录像状态
        // public const int 
        /// <summary>
        /// 注册信息回调 函数
        /// </summary>
        /// <param name="hHandle"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <param name="context"></param>
        public  void messagecallback(IntPtr hHandle, int wParam, int lParam, IntPtr context)
        {
            switch (wParam)
            {
                case LAUMSG_LINKMSG:
                    {
                        //link message
                        if (lParam == 0)
                        {
                            strMsg = "连接成功";
                        }
                        else if (lParam == 1)
                        {
                            strMsg = "用户停止连接";
                        }
                        else if (lParam == 2)
                        {
                             strMsg = "连接失败";
                        }
                        else if (lParam == 3)
                        {
                            strMsg = "连接断开";
                        }
                        else if (lParam == 4)
                        {
                           strMsg = "断开维护";
                        }
                        else if (lParam == 5)
                        {
                           strMsg = "分配内存失败";
                        }
                        else if (lParam == 6)
                        {
                            strMsg = "连接DNS错误";
                        }
                        else if (lParam == -102)
                        {
                            strMsg = "用户名或密码错误";
                        }
                        else if (lParam == -103)
                        {
                            strMsg = "系统用户已满";
                        }
                        else if (lParam == -105)
                        {
                            strMsg = "通道用户已满";
                        }
                        else if (lParam == -106)
                        {
                            strMsg = "没有该通道";
                        }
                        else if (lParam == -112)
                        {
                            strMsg = "没有找到服务器";
                        }
                        else
                        {
                            strMsg = "连接,未定义错误~! ";
                        }
                    //    if (lParam != 0 || lParam !=1)//不是连接成功和停止连接
                   //     Common.SysLog.WriteServiceDisk(strMsg, "1", AppDomain.CurrentDomain.BaseDirectory);
                        break;
                    }
                case LAUMSG_VIDEOMOTION:
                    strMsg = "视频请求警告";
                //    Common.SysLog.WriteServiceDisk(strMsg, "1", AppDomain.CurrentDomain.BaseDirectory);
                    break;
                case LAUMSG_VIDEOLOST:
                    strMsg = "视频丢失警告";
                 //   Common.SysLog.WriteServiceDisk(strMsg, "1", AppDomain.CurrentDomain.BaseDirectory);
                    break;
                case LAUMSG_ALARM:
                    {
                        strMsg = "探头报警警告";
                  //      Common.SysLog.WriteServiceDisk(strMsg, "1", AppDomain.CurrentDomain.BaseDirectory);
                        break;
                    }
                case LAUMSG_OUTPUTSTATUS:
                    strMsg = "报警输出状态消息";
                  //  Common.SysLog.WriteServiceDisk(strMsg, "1", AppDomain.CurrentDomain.BaseDirectory);
                    break;
                case LAUMSG_CURSWITCHCHAN:
                    strMsg = "通道切换消息";
                  //  Common.SysLog.WriteServiceDisk(strMsg, "1", AppDomain.CurrentDomain.BaseDirectory);
                    break;
                case LAUMSG_HIDEALARM:
                     strMsg = "视频遮挡报警消息";
                  //   Common.SysLog.WriteServiceDisk(strMsg, "1", AppDomain.CurrentDomain.BaseDirectory);
                    break;
                case LAUMSG_SERVERRECORD: //设备录像信息
                    if (lParam==0)
                    {
                        strMsg = "未录像";
                    }
                    else if(lParam==1)
                    {
                        strMsg = "正在录像";
                    }
                 //   Common.SysLog.WriteServiceDisk(strMsg, "1", AppDomain.CurrentDomain.BaseDirectory);
                    break;
                default:
                   strMsg = wParam+"-未定义错误~!";
                 //  Common.SysLog.WriteServiceDisk(strMsg, "1", AppDomain.CurrentDomain.BaseDirectory);
                    break;
            }
        }

    }
}
