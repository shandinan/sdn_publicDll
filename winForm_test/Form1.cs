using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sdnKDCamera;
using System.Net;
using static sdnKDCamera.IPCSdk;
using System.Runtime.InteropServices;

namespace winForm_test
{
    public partial class Form1 : Form
    {

        int iPort_play = 0; //播放句柄
        long pdwMediaId = 0;//MediaId
       // private IPCSdk ipcsdk = null;//IPCSdk
        public Form1()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 初始化dll
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInit_Click(object sender, EventArgs e)
        {
            try
            {
                long iErro = 0;
                bool bl = IPCSdk.IPC_InitDll("ipcsdk.dll", 3300, 0, ref iErro);
                string strIp = "192.168.50.20";
                uint iPort = 80;
                string strName = "admin";
                string strPasswd = "admin123";
                char[] separator = new char[] { '.' };
                string[] items = strIp.Split(separator);
                long dreamduip = long.Parse(items[0]) << 24
                        | long.Parse(items[1]) << 16
                        | long.Parse(items[2]) << 8
                        | long.Parse(items[3]);
                // long lIp = System.Net.IPAddress.HostToNetworkOrder(dreamduip);
                uint lIp = Convert.ToUInt32(dreamduip);

                //创建句柄
                long inHandle = IPCSdk.IPC_CreateHandle(lIp, iPort, strName, strPasswd);
                //登录摄像头
                bool bl_login = IPCSdk.IPC_Login(ref inHandle, strName, strPasswd, ref iErro);

                IPCSdk.PMGR_InitPortMgr(); //初始化 mediaportmgr.dll
                IPCSdk.MEDIA_Init(3500, 0); //初始化 mediarevsdk.dll

               int iGetMediaId = IPCSdk.MEDIA_GetMediaId(ref pdwMediaId);

                //初始化 uniplay.dll 
                bool lb_init_uniplay = IPCSdk.PLAYKD_Startup();
               
                //获取播放端口
                bool bl_getPort = IPCSdk.PLAYKD_GetPort(null, false, ref iPort_play);
                //打开视频流
                bool bl_OpenStream = IPCSdk.PLAYKD_OpenStream(iPort_play, null, 0, 3);

                bool bl_play = IPCSdk.PLAYKD_Play(iPort_play, picShow.Handle);
                // 下面处理 rtsp流
                uint wVideoPort = 0;
                uint wAudioPort = 0;
                uint dwLocalIp = 0;
                //WORD wStreamChn = 1; //The first way: mainstream second way: auxiliary flow
                uint wVideoBackRtcp = 0;
                uint wAudioBackRtcp = 0;
                uint wAudioBackRtcp2 = 0;
                bool bDoubleAudio = false;

               int i=  IPCSdk.PMGR_GetLocalIp(ref dwLocalIp, lIp, 80);
               int ii= IPCSdk.PMGR_GetMediaPort(ref wVideoPort, ref wAudioPort, '0', 60000);

                tagPlayVideoInfo tPlayVideoInfo = new tagPlayVideoInfo();

                TRTSPPARAM tRtspParam = new TRTSPPARAM();
                tRtspParam.byVideoSource = '1';
                tRtspParam.wVideoChanID = 1;

                TRTSPINFO tRtspInfo = new TRTSPINFO();
                int nLen = 0;
                int bNoStream = 0;
                bool bRet = IPCSdk.IPC_GetRtspUrl(ref inHandle, emPlayVideoType.type_tcp, ref tRtspParam, Marshal.SizeOf(typeof(TRTSPPARAM)), ref tRtspInfo, nLen, ref iErro, bNoStream);

                tagRtspSwitchParam tRtspSwitch = new tagRtspSwitchParam();

                tRtspSwitch.tSwitchParam.tPlayPortInfo.tPlayVideoPort.wRtpPort = wVideoPort;
                tRtspSwitch.tSwitchParam.tPlayPortInfo.tPlayVideoPort.wRtcpPort = wVideoPort + 1;
                tRtspSwitch.tSwitchParam.tPlayPortInfo.tPlayVideoPort.wRtcpBackPort = tPlayVideoInfo.wRtcpVideoPort;
                tRtspSwitch.tSwitchParam.tPlayPortInfo.tPlayAudioPort.wRtpPort = wAudioPort;
                tRtspSwitch.tSwitchParam.tPlayPortInfo.tPlayAudioPort.wRtcpPort = wAudioPort + 1;
                tRtspSwitch.tSwitchParam.tPlayPortInfo.tPlayAudioPort.wRtcpBackPort = tPlayVideoInfo.wRtcpAudioPort;

                tRtspSwitch.tSwitchParam.tRemotePortInfo.wRemoteVideoPort = 59000;
                tRtspSwitch.tSwitchParam.tRemotePortInfo.wRemoteAudioPort = 59002;

                //strcpy(tRtspSwitch.szAdmin, m_sUser);
                //strcpy(tRtspSwitch.szPassword, m_sPass);
                //strcpy(tRtspSwitch.szMediaURL, tRtspInfo.szurl);

                tRtspSwitch.szAdmin = Encoding.Default.GetBytes(strName);
                tRtspSwitch.szPassword = Encoding.Default.GetBytes(strPasswd);
                tRtspSwitch.szMediaURL =tRtspInfo.szurl;

                // 按需
                tRtspSwitch.tSwitchParam.tEncNameAndPayload.eEncName = tagEncName.E_ENCNAME_H264;
                tRtspSwitch.tSwitchParam.tEncNameAndPayload.byPayload = 'j';
                tRtspSwitch.bAlarm = true;      // 是否开启接收前端智能告警回调
                tRtspSwitch.bNoStream = false;//FALSE申请rtsp码流， TRUE不申请rtsp码流，只申请rtsp告警链路

                int nMediaRet = MEDIA_SetRtspSwitch(pdwMediaId, lIp, dwLocalIp, tRtspSwitch);

                // TRTSPPARAM tRtspParam;

                //string strVersion="";
                //   bl = IPCSdk.IPC_GetVersion(out strVersion, 1000, ref iErro);
                // MessageBox.Show(strVersion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 开始录像
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStartRec_Click(object sender, EventArgs e)
        {
            try
            {
                string strPath = @"D:\test1.mp4";
                  bool bl_start_rec = IPCSdk.PLAYKD_StartLocalRecord(iPort_play, strPath, 1);
               // bool bl_start_rec = IPCSdk.PLAYKD_StartLocalRecordExt(iPort_play, strPath, 1, 0, 102400, true);

              
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 结束录像
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStopRec_Click(object sender, EventArgs e)
        {
            bool bl_stop_rec = IPCSdk.PLAYKD_StopLocalRecord(iPort_play);
            IPCSdk.PLAYKD_FreePort(iPort_play);
            IPCSdk.PLAYKD_Cleanup();
        }
        /// <summary>
        /// 窗口加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
           // ipcsdk = new IPCSdk();
        }
    }
}
