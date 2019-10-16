/*
 * 此类是摄像头登录和退出方法的封装
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
{
    public class CameraLogin
    {
        public CHCNetSDK.NET_DVR_IPPARACFG_V40 m_struIpParaCfgV40; //IP设备资源及IP通道资源配置结构体
        public CHCNetSDK.NET_DVR_DEVICEINFO_V30 DeviceInfo; //设备参数结构体
        public CHCNetSDK.NET_DVR_STREAM_MODE m_struStreamMode; //取流方式配置结构体
        public CHCNetSDK.NET_DVR_IPCHANINFO m_struChanInfo;
        private LoginParm _loginParm;
        private uint dwAChanTotalNum = 0;
        private Int32 i = 0;
        private Int32 m_lTree = 0;
        private string str1 = string.Empty;
        private string str2 = string.Empty;
        private int[] _iChannelNum = new int[100];
        int m_lUserID = -1;
        uint iLastErr = 0;
        public CameraLogin(LoginParm loginParm)
        {
            this._loginParm = loginParm;
        }
       
        /// <summary>
        /// 登录注册dll
        /// </summary>
        /// <param name="str1"></param>
        /// <returns></returns>
        public int LoginDll(out int[] chanNums)
        {
            string str1 = "";
            //登录设备 Login the device
            m_lUserID = CHCNetSDK.NET_DVR_Login_V30(_loginParm.Login_Ip, Int32.Parse(_loginParm.Login_Port), _loginParm.Login_Name, _loginParm.Login_Password, ref DeviceInfo);
            if (m_lUserID < 0)
            {
                iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                str1 = "NET_DVR_Login_V30 失败, 错误代码= " + iLastErr; //登录失败，输出错误号 
                chanNums = null;
                int iTemp = -1;
                try
                {
                    iTemp = -Convert.ToInt32(iLastErr);
                }
                catch
                {
                }
                return iTemp;
               // return -1;
            }
            else
            {
                str1 = "登录成功";
                dwAChanTotalNum = (uint)DeviceInfo.byChanNum;

                if (DeviceInfo.byIPChanNum > 0)
                {
                    chanNums = InfoIPChannel();
                }
                else
                {
                    for (i = 0; i < dwAChanTotalNum; i++)
                    {
                        ListAnalogChannel(i + 1, 1);
                        _iChannelNum[i] = i + (int)DeviceInfo.byStartChan;
                    }
                    chanNums = _iChannelNum;
                    // MessageBox.Show("This device has no IP channel!");
                }

                return m_lUserID;
            }



        }
        /// <summary>
        /// 退出登录
        /// </summary>
        /// <param name="m_lRealHandle"></param>
        /// <param name="str1"></param>
        /// <returns></returns>
        public int LogoutDll(int m_lRealHandle,int lUserID, out string str1)
        {

            //注销登录
            if (m_lRealHandle >= 0)
            {
                str1 = "退出前先停止预览"; //登出前先停止预览 
                return -1;
            }

            if (!CHCNetSDK.NET_DVR_Logout(lUserID))
            {
                iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                str1 = "NET_DVR_Logout 失败, 错误代码" + iLastErr;
               
                return -1;
            }
            m_lUserID = -1;
            str1 = "退出成功";
            return 1;
        }

        public int[] InfoIPChannel()
        {
            uint dwSize = (uint)Marshal.SizeOf(m_struIpParaCfgV40);

            IntPtr ptrIpParaCfgV40 = Marshal.AllocHGlobal((Int32)dwSize);
            Marshal.StructureToPtr(m_struIpParaCfgV40, ptrIpParaCfgV40, false);

            uint dwReturn = 0;
            if (!CHCNetSDK.NET_DVR_GetDVRConfig(m_lUserID, CHCNetSDK.NET_DVR_GET_IPPARACFG_V40, -1, ptrIpParaCfgV40, dwSize, ref dwReturn))
            {
                iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                //  string str1 = "NET_DVR_GET_IPPARACFG_V40 失败, 错误代码： " + iLastErr;
                //获取IP资源配置信息失败，输出错误号 
            }
            else
            {
                // succ
                m_struIpParaCfgV40 = (CHCNetSDK.NET_DVR_IPPARACFG_V40)Marshal.PtrToStructure(ptrIpParaCfgV40, typeof(CHCNetSDK.NET_DVR_IPPARACFG_V40));

                for (i = 0; i < dwAChanTotalNum; i++)
                {
                    _iChannelNum[i] = i + (int)DeviceInfo.byStartChan;
                }

                byte byStreamType;
                for (i = 0; i < m_struIpParaCfgV40.dwDChanNum; i++)
                {
                    _iChannelNum[i + dwAChanTotalNum] = i + (int)m_struIpParaCfgV40.dwStartDChan;
                    byStreamType = m_struIpParaCfgV40.struStreamMode[i].byGetStreamType;
                    switch (byStreamType)
                    {
                        //目前NVR仅支持0- 直接从设备取流一种方式
                        case 0:
                            dwSize = (uint)Marshal.SizeOf(m_struStreamMode);
                            IntPtr ptrChanInfo = Marshal.AllocHGlobal((Int32)dwSize);
                            Marshal.StructureToPtr(m_struIpParaCfgV40.struStreamMode[i].uGetStream, ptrChanInfo, false);
                            m_struChanInfo = (CHCNetSDK.NET_DVR_IPCHANINFO)Marshal.PtrToStructure(ptrChanInfo, typeof(CHCNetSDK.NET_DVR_IPCHANINFO));

                            //列出IP通道 List the IP channel
                            ListIPChannel(i + 1, m_struChanInfo.byEnable, m_struChanInfo.byIPID);
                            Marshal.FreeHGlobal(ptrChanInfo);
                            break;

                        default:
                            break;
                    }
                }
            }
            Marshal.FreeHGlobal(ptrIpParaCfgV40);
            return _iChannelNum;
        }
        /// <summary>
        /// 动态给IPchannel添加相应的通道号(IP通道号)
        /// </summary>
        /// <param name="iChanNo"></param>
        /// <param name="byOnline"></param>
        /// <param name="byIPID"></param>
        public void ListIPChannel(Int32 iChanNo, byte byOnline, byte byIPID)
        {
            str1 = String.Format("IPCamera {0}", iChanNo);
            m_lTree++;

            if (byIPID == 0)
            {
                str2 = "X"; //通道空闲，没有添加前端设备                 
            }
            else
            {
                if (byOnline == 0)
                {
                    str2 = "offline"; //通道不在线
                }
                else
                    str2 = "online"; //通道在线
            }

            // listViewIPChannel.Items.Add(new ListViewItem(new string[] { str1, str2 }));//将通道添加到列表中
        }
        /// <summary>
        /// 模拟通道列表
        /// </summary>
        /// <param name="iChanNo"></param>
        /// <param name="byEnable"></param>
        public void ListAnalogChannel(Int32 iChanNo, byte byEnable)
        {
            str1 = String.Format("Camera {0}", iChanNo);
            m_lTree++;

            if (byEnable == 0)
            {
                str2 = "Disabled"; //通道已被禁用 This channel has been disabled               
            }
            else
            {
                str2 = "Enabled"; //通道处于启用状态
            }

            // listViewIPChannel.Items.Add(new ListViewItem(new string[] { str1, str2 }));//将通道添加到列表中
        }
    }
}
