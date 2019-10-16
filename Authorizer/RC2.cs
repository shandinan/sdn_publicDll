using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Authorizer
{
    public class RC2
    {
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="value">暗号化対象文字列</param>
        /// <returns>暗号化文字列</returns>
        public static string Encrypt(string value, byte[] key, byte[] iv)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }

            byte[] objInputByteArray = Encoding.UTF8.GetBytes(value);
            RC2CryptoServiceProvider objRC2CryptoServiceProvider = new RC2CryptoServiceProvider();


            ICryptoTransform objICryptoTransform = objRC2CryptoServiceProvider.CreateEncryptor(key, iv);

            using (MemoryStream objMemoryStream = new MemoryStream())
            using (CryptoStream objCryptoStream = new CryptoStream(objMemoryStream, objICryptoTransform, CryptoStreamMode.Write))
            {
                objCryptoStream.Write(objInputByteArray, 0, objInputByteArray.Length);
                objCryptoStream.FlushFinalBlock();
                objRC2CryptoServiceProvider.Clear();
                return Convert.ToBase64String(objMemoryStream.ToArray());
            }
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="value">解密对象文字列</param>
        /// <returns>加密文字列</returns>
        public static string Decrypt(string value, byte[] key, byte[] iv)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }
            byte[] objInputByteArray = Convert.FromBase64String(value);
            RC2CryptoServiceProvider objRC2CryptoServiceProvider = new RC2CryptoServiceProvider();
            ICryptoTransform objICryptoTransform = objRC2CryptoServiceProvider.CreateDecryptor(key, iv);

            using (MemoryStream objMemoryStream = new MemoryStream())
            using (CryptoStream objCryptoStream = new CryptoStream(objMemoryStream, objICryptoTransform, CryptoStreamMode.Write))
            {
                objCryptoStream.Write(objInputByteArray, 0, objInputByteArray.Length);
                objCryptoStream.FlushFinalBlock();
                objRC2CryptoServiceProvider.Clear();
                return Encoding.UTF8.GetString(objMemoryStream.ToArray());
            }
        }
        public static RCSIVKey GetKeyIV()
        {
            RC2CryptoServiceProvider rc2CryptoServiceProvider = new RC2CryptoServiceProvider();
            rc2CryptoServiceProvider.GenerateIV();
            rc2CryptoServiceProvider.GenerateKey();
            return new RCSIVKey(rc2CryptoServiceProvider.Key, rc2CryptoServiceProvider.IV);
        }
    }

    public struct RCSIVKey
    {
        readonly byte[] iv;
        readonly byte[] key;
        public RCSIVKey(byte[] key, byte[] iv)
        {
            this.iv = iv;
            this.key = key;
        }
        public byte[] IV
        {
            get
            {
                return this.iv;
            }
        }

        public byte[] Key
        {
            get
            {
                return this.key;
            }
        }
    }
}
