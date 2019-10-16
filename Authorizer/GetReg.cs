/**
 * 得到相应的授权信息
 * */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Authorizer
{
    public class GetReg
    {
        #region 基础变量
        string publicKey = "<?xml version=\"1.0\" encoding=\"utf-16\"?><RSAKeyValue><Modulus>7Hqx53cCFNc4bJXcLRcqBNPe4/uGi/ytdahJuXmyc27iiXB23Hu4WGA8P3GNSqJOevBo6IC6iCzmaGBb4n7vts4mJRImLxSGl4Uyt+CyIqhPaLYuEuPpGIvzDLJ6r7XcC5R08rBloJXaL/lReKk2hPuhYSW2QFAD1rwVMbioRxU=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
        
        #endregion

        #region 基础函数

        /// <summary>
        /// 对原始数据进行MD5加密
        /// </summary>
        /// <param name="m_strSource">待加密数据</param>
        /// <returns>返回机密后的数据</returns>
        private string GetHash(string m_strSource)
        {

            HashAlgorithm algorithm = HashAlgorithm.Create("MD5");

            byte[] bytes = Encoding.UTF8.GetBytes(m_strSource);

            byte[] inArray = algorithm.ComputeHash(bytes);

            return Convert.ToBase64String(inArray);

        }

        #endregion

        #region 加密
        /// <summary>
        /// 获取特征码信息
        /// </summary>
        /// <returns></returns>
        public string GetConditionCode()
        {
            //得到本机硬件信息
            string strHardMsg = string.Format("Mac:{0};CpuID:{1};HardID:{2}",
                Local.GetLocalMac(), //本机mac地址
                Local.GetCpuID(), //本机CPU ID
                Local.GetHardID()); //本机硬件ID
            return GetHash(strHardMsg);
        }
        #endregion

        #region 解密
        /// <summary>
        /// 根据公钥解密
        /// </summary>
        /// <param name="strSource"></param>
        /// <returns></returns>
        public string DecryptByPublicKey(string strSource)
        {
            System.Security.Cryptography.RSACryptoServiceProvider p = new System.Security.Cryptography.RSACryptoServiceProvider();
            p.FromXmlString(publicKey);
            var ep = p.ExportParameters(false);
            var exponent = Convert.ToBase64String(ep.Exponent);
            var n = Convert.ToBase64String(ep.Modulus);
            return RC2RSA.Decrypt(strSource, exponent, n);//根据授权码 得到解析后的编码
        }
        #endregion
    }
}
