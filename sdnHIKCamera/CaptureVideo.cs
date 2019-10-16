/*
 * 此类是摄像头截图方法
 * 作者：单氐楠
 * 日期：2014-12-05
 * 注意：未经本人同意切换修改
 * */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sdnHIKCamera
{
    public class CaptureVideo
    {
        private CaptureParms captureImg;
        public CaptureVideo(CaptureParms cp)
        {
            this.captureImg = cp;
        }
        public string CaptureImgs()
        {
            int lChannel = captureImg.iChannelNum; //通道号 
            uint iLastErr = 0;
            string str = string.Empty;
            CHCNetSDK.NET_DVR_JPEGPARA lpJpegPara = new CHCNetSDK.NET_DVR_JPEGPARA();
            lpJpegPara.wPicQuality = 0; //图像质量
           // lpJpegPara.wPicSize = 0xff; //抓图分辨率 Picture size: 0xff-Auto(使用当前码流分辨率) 
            lpJpegPara.wPicSize = 0xff; // 0=CIF, 1=QCIF, 2=D1 3=UXGA(1600x1200), 4=SVGA(800x600), 5=HD720p(1280x720),6=VGA
            //抓图分辨率需要设备支持，更多取值请参考SDK文档

            //JEP G抓图，数据保存在缓冲区中 
            uint iBuffSize = 400000; //缓冲区大小需要不小于一张图片数据的大小 
            byte[] byJpegPicBuffer = new byte[iBuffSize];
            uint dwSizeReturned = 0;

            if (!CHCNetSDK.NET_DVR_CaptureJPEGPicture_NEW(captureImg.m_lUserID, lChannel, ref lpJpegPara, byJpegPicBuffer, iBuffSize, ref dwSizeReturned))
            {
                iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                str = "保存图片错误，错误代码为: " + iLastErr;
                //return null;
                return str;
            }
            else
            {
                // string filePath = AppDomain.CurrentDomain.BaseDirectory;
                string filePath;
                if (string.IsNullOrEmpty(captureImg.strFilePath))
                    filePath = Directory.GetCurrentDirectory() + @"\CaptureImgs\";
                else
                    filePath = captureImg.strFilePath;
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                //将缓冲区里的JPEG图片数据写入文件
                FileStream fs = new FileStream(filePath + captureImg.strFileName, FileMode.Create);
                int iLen = (int)dwSizeReturned;
                fs.Write(byJpegPicBuffer, 0, iLen);
                fs.Close();

                str = "成功完成抓取图片，并且保存到硬盘";
            }

            return str;
        }
    }
}
