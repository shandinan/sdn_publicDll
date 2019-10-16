using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;

namespace Authorizer
{
    public class Local
    {
        /// <summary>
        /// 获取本机的MAC地址
        /// </summary>
        /// <returns></returns>
        public static string GetLocalMac()
        {
            string mac = null;
            ManagementObjectSearcher query = new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection queryCollection = query.Get();
            foreach (ManagementObject mo in queryCollection)
            {
                if (mo["IPEnabled"].ToString() == "True")
                    mac = mo["MacAddress"].ToString();
            }
            return (mac);
        }

        /// <summary>
        /// 得到CPU序列号
        /// </summary>
        /// <returns></returns>
        public static string GetCpuID()
        {
            try
            {
                //获取CPU序列号代码 
                string cpuInfo = "";//cpu序列号 
                ManagementClass mc = new ManagementClass("Win32_Processor");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    cpuInfo = mo.Properties["ProcessorId"].Value.ToString();
                }
                moc = null;
                mc = null;
                return cpuInfo;
            }
            catch
            {
                return "unknow";
            }
            finally
            {
            }
        }

        /// <summary>
        /// 获取硬盘ID
        /// </summary>
        /// <returns>硬盘ID</returns>
        public static string GetHardID()
        {
            string HDInfo = "";
            ManagementClass cimobject1 = new ManagementClass("Win32_DiskDrive");
            ManagementObjectCollection moc1 = cimobject1.GetInstances();
            foreach (ManagementObject mo in moc1)
            {
                HDInfo = (string)mo.Properties["Model"].Value;
            }
            return HDInfo;
        }
    }
}
