using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace sdnHttpOper
{
    public class sdnHttpUploadFIle
    {
        /// <summary> 
        /// 将本地文件上传到指定的服务器(HttpWebRequest方法) 
        /// </summary> 
        /// <param name="address">文件上传到的服务器</param> 
        /// <param name="fileNamePath">要上传的本地文件（全路径）</param> 
        /// <param name="saveName">文件上传后的名称</param> 
        /// <returns>成功返回1，失败返回0</returns> 
        public int Upload_Request(string address, string fileNamePath, string saveName)
        {
            int returnValue = 0;

            // 要上传的文件 
            FileStream fs = new FileStream(fileNamePath, FileMode.Open, FileAccess.Read);
            BinaryReader r = new BinaryReader(fs);

            //时间戳 
            string strBoundary = "----------" + DateTime.Now.Ticks.ToString("x");
            byte[] boundaryBytes = Encoding.ASCII.GetBytes("\r\n--" + strBoundary + "\r\n");

            //请求头部信息 
            StringBuilder sb = new StringBuilder();
            sb.Append("--");
            sb.Append(strBoundary);
            sb.Append("\r\n");
            sb.Append("Content-Disposition: form-data; name=\"");
            sb.Append("trackdata");
            sb.Append("\"; filename=\"");
            sb.Append(saveName);
            sb.Append("\"");
            sb.Append("\r\n");
            sb.Append("Content-Type: ");
            sb.Append("image/pjpeg,image/bmp");
            sb.Append("\r\n");
            sb.Append("\r\n");
            string strPostHeader = sb.ToString();
            byte[] postHeaderBytes = Encoding.UTF8.GetBytes(strPostHeader);

            // 根据uri创建HttpWebRequest对象 
            HttpWebRequest httpReq = (HttpWebRequest)WebRequest.Create(new Uri(address));
            httpReq.Method = "POST";

            //对发送的数据不使用缓存 
            httpReq.AllowWriteStreamBuffering = false;

            //设置获得响应的超时时间（300秒） 
            httpReq.Timeout = 300000;
            httpReq.ContentType = "multipart/form-data; boundary=" + strBoundary;
            long length = fs.Length + postHeaderBytes.Length + boundaryBytes.Length;
            long fileLength = fs.Length;
            httpReq.ContentLength = length;
            try
            {
                //每次上传4k 
                int bufferLength = 4096;
                byte[] buffer = new byte[bufferLength];

                //已上传的字节数 

                //开始上传时间 
                DateTime startTime = DateTime.Now;
                int size = r.Read(buffer, 0, bufferLength);
                Stream postStream = httpReq.GetRequestStream();

                //发送请求头部消息 
                postStream.Write(postHeaderBytes, 0, postHeaderBytes.Length);
                while (size > 0)
                {
                    postStream.Write(buffer, 0, size);

                    size = r.Read(buffer, 0, bufferLength);
                }
                //添加尾部的时间戳 
                postStream.Write(boundaryBytes, 0, boundaryBytes.Length);
                postStream.Close();

                //获取服务器端的响应 
                WebResponse webRespon = httpReq.GetResponse();
                Stream s = webRespon.GetResponseStream();
                StreamReader sr = new StreamReader(s);

                //读取服务器端返回的消息 
                String sReturnString = sr.ReadLine();
                s.Close();
                sr.Close();
                if (sReturnString == "Success")
                {
                    returnValue = 1;
                }
                else if (sReturnString == "Error")
                {
                    returnValue = 0;
                }

            }
            catch
            {
                returnValue = 0;
            }
            finally
            {
                fs.Close();
                r.Close();
            }

            return returnValue;
        }
    }
}
