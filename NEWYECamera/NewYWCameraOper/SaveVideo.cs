using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace NewYWCameraOper
{
    public class SaveVideo
    {
        /// <summary>
        /// 根据字节和文件名保存视频文件
        /// </summary>
        /// <param name="bytData">要保存的字节</param>
        /// <param name="name">文件全路径</param>
        public static void WriteOptDisk(byte[] bytData, string name)
        {
            FileStream fs = null;
            try
            {
                //string path = name;
                if (!File.Exists(name))
                {
                    int iIndex = name.LastIndexOf('\\');
                    string strPath = name.Substring(0, iIndex);
                    Directory.CreateDirectory(strPath);
                    File.Create(name).Close();
                }
                fs = new FileStream(name, FileMode.Append, FileAccess.Write, FileShare.ReadWrite, 2048);
                fs.Write(bytData, 0, bytData.Length);
            }
            catch
            {

            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
            }
        }
    }
}
