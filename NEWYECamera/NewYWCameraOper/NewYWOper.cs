/**
 *  新品牌亿维摄像头操作类
 * 
 * 
 * */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace NewYWCameraOper
{
    public class NewYWOper
    {

        #region 公共变量

        public NewYWCameraOper.NewYWcameraParam YWparam = null;//新品牌亿维摄像头参数
        IntPtr hLogonServer = IntPtr.Zero;//登录句柄
        IntPtr hOpenChannel = IntPtr.Zero;//打开通道句柄
        IntPtr hFile = IntPtr.Zero;//初始化录像文件句柄
        IntPtr iOpenPic = IntPtr.Zero;//拍照句柄


        public string strVideoPath = "";//视频录像文件路径

     //   private static HHNetInterface.ChannelStreamCallback delChannelStreamCallback = null;
       // private static HHNetInterface.PictureCallback delPicCallback = null;

        public NewYWOper(NewYWCameraOper.NewYWcameraParam ywparam)
        {
            YWparam = ywparam;
           
        }
        #endregion

        #region SDK初始化与关闭
        /// <summary>
        /// 启动网络服务，初始化SDK
        /// </summary>
        /// <returns>0:成功 其他：错误码</returns>
        public int  ClientInit(IntPtr hNotifyWnd, uint nCommandID, string pLocalAddr)
        {
            int iErr = (int)HHNetInterface.HHNET_Startup(hNotifyWnd, nCommandID, 0, false, false, pLocalAddr);
            return iErr;
        }
        /// <summary>
        /// 关闭网络服务
        /// </summary>
        /// <returns></returns>
        public int ClientFree()
        {
            return (int)HHNetInterface.HHNET_Cleanup();
        }

        #endregion

        #region 登录预览

        public int sdnLogonServer(out string strMsg)
        {
            if(YWparam==null){
                strMsg="请先初始化登录参数！";
                return -1;
            }
            int iErr = (int)HHNetInterface.HHNET_LogonServer(YWparam.pServerIP, YWparam.pServerPort, YWparam.pDeviceName, YWparam.pUserName, YWparam.pUserPassword, YWparam.dwClientID, ref hLogonServer, IntPtr.Zero);
            strMsg = "调用函数成功";
            return iErr;
        }
        /// <summary>
        /// 注销
        /// </summary>
        /// <returns></returns>
        public int sdnLogonOffServer()
        {
            if ((int)hLogonServer > 0)
            {
                return (int)HHNetInterface.HHNET_LogoffServer(hLogonServer);
            }
            return -1;
        }

        /// <summary>
        /// 打开音视频通道
        /// </summary>
        /// <param name="strMsg"></param>
        /// <returns></returns>
        public int sdnOpenChannel(out string strMsg)
        {
            if (YWparam == null)
            {
                strMsg = "请先初始化打开音视频通道参数";
                return -1;
            }
            else
            {
                //设置委托
                HHNetInterface.ChannelStreamCallback  delChannelStreamCallback = new HHNetInterface.ChannelStreamCallback(sdnChannelStramCallBack);
                HHNetInterface.HHOPEN_CHANNEL_INFO open_channel_info = new HHNetInterface.HHOPEN_CHANNEL_INFO();
                open_channel_info.dwClientID = 0;
                open_channel_info.nOpenChannel = 0;
                open_channel_info.protocolType = NewYWCameraOper.HHNetInterface.NET_PROTOCOL_TYPE.NET_PROTOCOL_TCP;
               // open_channel_info.funcStreamCallback = null;
                open_channel_info.pCallbackContext = IntPtr.Zero;
                open_channel_info.funcStreamCallback = delChannelStreamCallback;
                //YWparam.Open_Channel_Info = delChannelStreamCallback;
                int iSizeOfStruct = Marshal.SizeOf(typeof(HHNetInterface.HHOPEN_CHANNEL_INFO));
                IntPtr pStructChannel = Marshal.AllocHGlobal(iSizeOfStruct);
                Marshal.StructureToPtr(open_channel_info, pStructChannel, false);
                YWparam.Open_Channel_Info = pStructChannel;
                GCHandle.Alloc(delChannelStreamCallback);  //防止被回收
            }
           
            HHNetInterface.HHERR_CODE errcode = HHNetInterface.HHNET_OpenChannel(YWparam.pServerIP, YWparam.pServerPort, YWparam.pDeviceName, YWparam.pUserName, YWparam.pUserPassword, YWparam.Open_Channel_Info, ref hOpenChannel, IntPtr.Zero);
            int iErr = (int)errcode;
            strMsg = "调用函数成功";
            return iErr;
        }
        /// <summary>
        /// 读取通道
        /// </summary>
        /// <param name="strMsg"></param>
        /// <returns></returns>
        public int sdnReadChannelInfo(out string strMsg)
        {
            if (hOpenChannel == IntPtr.Zero)
            {
                strMsg = "请先打开通道";
                return -1;
            }
            else
            {
                HHNetInterface.HH_CHANNEL_INFO channelInfo = new HHNetInterface.HH_CHANNEL_INFO();
                strMsg = "成功";

                int iErr = (int)HHNetInterface.HHNET_ReadChannelInfo(hOpenChannel,ref channelInfo);
                return iErr;
            }
        }
        /// <summary>
        /// 录像函数
        /// </summary>
        /// <param name="lBufferSize"></param>
        /// <param name="strFileName"></param>
        /// <returns></returns>
        public int sdnStartRecVideo(int lBufferSize, string strFileName,out string strMsg)
        {
            hFile = HHNetInterface.HHFile_InitWriter();//初始文件生成
            YWparam.VideoFileName = strFileName;
            if (hFile != null && hFile != IntPtr.Zero)
            {
                int iErr=0;
                if ((iErr = HHNetInterface.HHFile_SetCacheBufferSize(hFile, lBufferSize)) == 0) //设置缓存区大小
                {
                    if((iErr=HHNetInterface.HHFile_StartWrite(hFile,strFileName))==0){ //开始录像成功
                        strMsg = "SDK:录像成功";
                        return (int)hFile;
                    }
                    else
                    {
                        strMsg = "SDK:开始录像失败";
                        return iErr;
                    }
                }
                else{
                    strMsg = "SDK:设置缓冲区大小失败";
                    return iErr;
                }

            }else
            {
                strMsg = "SDK:初始文件生成失败";
                return -1;
            }
        }
        /// <summary>
        /// 停止录像
        /// </summary>
        /// <param name="iFileHandle"></param>
        /// <returns></returns>
        public int sdnStopRecVideo(int iFileHandle)
        {
           try
           {
               if (iFileHandle <= 0)
               {
                   iFileHandle = (int)hFile;
               }
               IntPtr ih = new IntPtr(iFileHandle);
              // HHNetInterface.HHFile_StopWrite(ih);
               HHNetInterface.HHFile_ReleaseWriter(ih);
               return 0;
           }
            catch(Exception ex)
           {
               return -1;
           }

        }
        /// <summary>
        /// 关闭录像通道
        /// </summary>
        /// <returns></returns>
        public int sdnCloseChannel()
        {
            try
            {
              return  (int)HHNetInterface.HHNET_CloseChannel(hOpenChannel);
            }
            catch
            {
                return -1;
            }
        }

        public IntPtr sdnCapturePic(string strImgName)
        {
            //1 打开拍照
            if(YWparam ==null)
            {
                return IntPtr.Zero;
            }
            YWparam.ImgFileName = strImgName;
            
            HHNetInterface.HHOPEN_PICTURE_INFO hhPicInfo = new HHNetInterface.HHOPEN_PICTURE_INFO();
           
            hhPicInfo.dwClientID = 0;
           HHNetInterface.PictureCallback delPicCallback = new HHNetInterface.PictureCallback(sdnCapturePictureCallback);
         //   delPicCallback = new HHNetInterface.PictureCallback(sdnCapturePictureCallback);
            hhPicInfo.funcPictureCallback = delPicCallback;
            hhPicInfo.nOpenChannel = 0;
            hhPicInfo.pCallbackContext = IntPtr.Zero;
            hhPicInfo.protocolType = HHNetInterface.NET_PROTOCOL_TYPE.NET_PROTOCOL_TCP;

            int iPicSize = Marshal.SizeOf(typeof(HHNetInterface.HHOPEN_PICTURE_INFO));
             IntPtr iHPicInfo = Marshal.AllocHGlobal(iPicSize);
             Marshal.StructureToPtr(hhPicInfo, iHPicInfo, false);

             GCHandle.Alloc(delPicCallback);  //防止被回收

            HHNetInterface.HHNET_OpenPicture(YWparam.pServerIP, YWparam.pServerPort, YWparam.pDeviceName, YWparam.pUserName, YWparam.pUserPassword, iHPicInfo, ref iOpenPic, IntPtr.Zero);

            if (iOpenPic != null && iOpenPic != IntPtr.Zero) //如果拍摄图片句柄不为空或者值有效
            {
                // 拍照
               int iCapture =  (int)HHNetInterface.HHNET_CapturePicture(iOpenPic); //拍照
                HHNetInterface.HH_PICTURE_INFO structPicInfo = new HHNetInterface.HH_PICTURE_INFO();
                IntPtr iStructPicInfo = IntPtr.Zero;
                HHNetInterface.HHNET_ReadPictureInfo(iOpenPic, ref structPicInfo);
               //structPicInfo = (HHNetInterface.HH_PICTURE_INFO)Marshal.PtrToStructure(iStructPicInfo, typeof(HHNetInterface.HH_PICTURE_INFO));
                return iOpenPic;
            }
            return IntPtr.Zero;
        }


        public IntPtr sdnCaptureCallback(string strImgName,HHNetInterface.PictureCallback delCallback )
        {
            //1 打开拍照
            if (YWparam == null)
            {
                return IntPtr.Zero;
            }
            YWparam.ImgFileName = strImgName;

            HHNetInterface.HHOPEN_PICTURE_INFO hhPicInfo = new HHNetInterface.HHOPEN_PICTURE_INFO();

            hhPicInfo.dwClientID = 0;
       //    HHNetInterface.PictureCallback delPicCallback = new HHNetInterface.PictureCallback(sdnCapturePictureCallback);
          //  delPicCallback = delCallback;
            GCHandle.Alloc(delCallback);  //防止被回收
            hhPicInfo.funcPictureCallback = delCallback;
            hhPicInfo.nOpenChannel = 0;
            hhPicInfo.pCallbackContext = IntPtr.Zero;
            hhPicInfo.protocolType = HHNetInterface.NET_PROTOCOL_TYPE.NET_PROTOCOL_TCP;

            int iPicSize = Marshal.SizeOf(typeof(HHNetInterface.HHOPEN_PICTURE_INFO));
            IntPtr iHPicInfo = Marshal.AllocHGlobal(iPicSize);
            Marshal.StructureToPtr(hhPicInfo, iHPicInfo, false);

            HHNetInterface.HHNET_OpenPicture(YWparam.pServerIP, YWparam.pServerPort, YWparam.pDeviceName, YWparam.pUserName, YWparam.pUserPassword, iHPicInfo, ref iOpenPic, IntPtr.Zero);

            if (iOpenPic != null && iOpenPic != IntPtr.Zero) //如果拍摄图片句柄不为空或者值有效
            {
                // 拍照
                int iCapture = (int)HHNetInterface.HHNET_CapturePicture(iOpenPic); //拍照
                HHNetInterface.HH_PICTURE_INFO structPicInfo = new HHNetInterface.HH_PICTURE_INFO();
             //   IntPtr iStructPicInfo = IntPtr.Zero;
                HHNetInterface.HHNET_ReadPictureInfo(iOpenPic, ref structPicInfo);
                return iOpenPic;
            }
            return IntPtr.Zero;
        }

        /// <summary>
        /// 关闭图片拍摄通道
        /// </summary>
        /// <param name="iOpenPic"></param>
        /// <returns></returns>
        public int sdnStopCapture(IntPtr iOpenP)
        {
            if((int)iOpenPic>0)
            {
                iOpenPic = iOpenP;
            }
           return  (int)HHNetInterface.HHNET_ClosePicture(iOpenPic);

        }

        /// <summary>
        /// 数据流回调函数
        /// </summary>
        /// <param name="hOpenChannel"></param>
        /// <param name="pStreamData"></param>
        /// <param name="dwClientID"></param>
        /// <param name="pConetxt"></param>
        /// <param name="encodeVideoType"></param>
        /// <param name="pAVInfo"></param>
        /// <returns></returns>
        private int sdnChannelStramCallBack(IntPtr hOpenChannel, IntPtr pStreamData, uint dwClientID, IntPtr pConetxt, HHNetInterface.ENCODE_VIDEO_TYPE encodeVideoType, IntPtr pAVInfo)
        {
            try
            {

                HHNetInterface.HV_FRAME_HEAD hv_frameHead = new HHNetInterface.HV_FRAME_HEAD();
                hv_frameHead = (HHNetInterface.HV_FRAME_HEAD)Marshal.PtrToStructure(pStreamData, typeof(HHNetInterface.HV_FRAME_HEAD));

                //**************************单氐楠 2017年5月5日15:52:16
                //  if (hFile==IntPtr.Zero||hFile==null) //如果录像文件句柄为0 或者null 
                // {
                //     return -1;//录像失败
                //  }
                //**************************单氐楠 2017年5月5日15:52:16


                int iSize = Marshal.SizeOf(typeof(HHNetInterface.HV_FRAME_HEAD));//得到实体的大小
                byte[] sdnByte = new byte[(int)(iSize + hv_frameHead.nByteNum)];
                Marshal.Copy(pStreamData, sdnByte, 0, (int)(iSize + hv_frameHead.nByteNum));

                // int i = HHNetInterface.HHFile_InputFrame(hFile, sdnByte, iSize + hv_frameHead.nByteNum, (uint)encodeVideoType);  //2017年5月5日15:42:59 单氐楠

                SaveVideo.WriteOptDisk(sdnByte, strVideoPath);

            }catch
            {
                
            }
            return 0;
        }

        /// <summary>
        /// 前段抓图回掉函数
        /// </summary>
        /// <param name="hPictureChn">拍照句柄</param>
        /// <param name="pPicData">图片信息</param>
        /// <param name="nPicLen">图片长度</param>
        /// <param name="dwClientID"></param>
        /// <param name="pContext"></param>
        /// <returns></returns>
        private int sdnCapturePictureCallback(IntPtr hPictureChn, IntPtr pPicData, int nPicLen, uint dwClientID, IntPtr pContext)
        {
            HHNetInterface.HH_PICTURE_INFO hhPicInfo = new HHNetInterface.HH_PICTURE_INFO();//图片信息结构体

            IntPtr iPicInfo = IntPtr.Zero;
            //HHNetInterface.HHNET_ReadPictureInfo(hPictureChn, ref iPicInfo); //读取图片信息
            /*
            hhPicInfo = (HHNetInterface.HH_PICTURE_INFO)Marshal.PtrToStructure(iPicInfo, typeof(HHNetInterface.HH_PICTURE_INFO)); //把句柄转换成结构体
            if (YWparam != null && string.IsNullOrEmpty(YWparam.ImgFileName)) //YWparam 摄像头参数存在值
            {
                YWparam.ImgFileName = YWparam.ImgFileName.Replace("jpg", "");//
                if(hhPicInfo.picFormatType==0)//jpg
                {
                    YWparam.ImgFileName = YWparam.ImgFileName + "jpg";

                }else if(hhPicInfo.picFormatType==1)//bmp
                {
                    YWparam.ImgFileName = YWparam.ImgFileName + "bmp";
                }
                else
                {
                    return 0;
                }
             * 
             * */
                byte[] sdnByteheader = new byte[((int)nPicLen) + 1];
                Marshal.Copy(pPicData, sdnByteheader, 0, (int)nPicLen); //复制句柄指定内存到字节数组

                //将缓冲区里的JPEG图片数据写入文件
                FileStream fs = new FileStream(YWparam.ImgFileName, FileMode.Create);
                int iLen = (int)nPicLen;
                fs.Write(sdnByteheader, 0, iLen);
                fs.Close();
                return 0;
           // }
            return -1;
        }


        public string sdnCaptureImg()
        {
            IntPtr iPicInfo = IntPtr.Zero;
            int iLen = 5 * 1024 ;
            int iLeg = 0;
            byte[] sdnByteheader = new byte[((int)iLen) + 1];
            HHNetInterface.HHERR_CODE hh = HHNetInterface.HHNET_GetServerConfig(hLogonServer, HHNetInterface.HHCMD_NET.HHCMD_GET_CAPTURE_PIC, ref sdnByteheader, iLen, ref iLeg);
           // byte[] sdnByteheader = new byte[((int)iLen) + 1];
            Marshal.Copy(iPicInfo, sdnByteheader, 0, (int)iLen); //复制句柄指定内存到字节数组
            return "";
        }


        #endregion
    }
}
