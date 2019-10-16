using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Authorizer
{
    public class RC2RSA
    {
        public RC2RSA()
        {

        }

        public static string Encrypt(string value, string d, string n)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }
            var keyIV = RC2.GetKeyIV();
            var encryptKey = RSA.Encrypt(keyIV.Key, d, n);
            var encryptIV = RSA.Encrypt(keyIV.IV, d, n);
            var rc2Encypt = RC2.Encrypt(value, keyIV.Key, keyIV.IV);
            return string.Concat(encryptKey, "-", encryptIV, "-", rc2Encypt);
        }


        public static string Decrypt(string value, string e, string n)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }
            var pos = value.IndexOf('-');
            if (pos <= 0)
            {
                throw new Exception("");
            }
            var pos2 = value.IndexOf('-', pos + 1);
            if (pos2 - pos <= 0)
            {
                throw new Exception("");
            }


            var decryptKey = value.Substring(0, pos);
            var decryptIV = value.Substring(pos + 1, pos2 - pos - 1);
            var decryptValue = value.Substring(pos2 + 1);
            var key = RSA.Decrypt(decryptKey, e, n);
            var iv = RSA.Decrypt(decryptIV, e, n);

            return RC2.Decrypt(decryptValue, key, iv);
        }
    }
}
