using System;

/*
 * 此类是摄像头SDK初始化方法
 * 作者：单氐楠
 * 日期：2014-12-05
 * 注意：未经本人同意切换修改
 * */
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sdnHIKCamera
{
    
   public class InitSDK
    {
        private uint iLastErr = 0; //错误代码
        /// <summary>
        /// SDK初始化
        /// </summary>
        public  int InitDll(out string strMsg)
        {
            try
            {
                if (CHCNetSDK.NET_DVR_Init() == false)
                {
                     iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    strMsg="初始化SDK错误!错误代码为"+iLastErr;
                    return -1;
                }
                else
                {
                    //保存SDK日志 到C:\\SdkLog\
                    CHCNetSDK.NET_DVR_SetLogToFile(3, "C:\\SdkLog\\", true);
                   // iChannelNum = new int[96];//初始化数组
                    strMsg = "初始化成功";
                    return 1;
                }
            }
            catch (Exception ex)
            {//错误日志
                strMsg = ex.Message;
                return -3;
            }
        }
    }
}
