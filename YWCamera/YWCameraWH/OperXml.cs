using Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace YWCameraWH
{
    public class OperXml
    {
        public DataView GetCameraDataByXml(string strXmlPath, string strWhere)
        {
            try
            {
                XMLProcess xmlProcess = new XMLProcess(strXmlPath); //xml文件存放路径
                DataView dv = xmlProcess.GetDataViewByXml(strWhere, null, "STAGE");
                if (dv != null && dv.Count > 0)
                {
                    return dv;
                }
                return null;
            }
            catch (Exception ex)
            {
                SysLog.WriteLog(ex, AppDomain.CurrentDomain.BaseDirectory);//记录日志文件
                return null;
            }
        }
    }
}
