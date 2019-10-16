using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authorizer
{
    public class RSA
    {
        public static string Encrypt(byte[] source, BigInteger d, BigInteger n)
        {
            BigInteger biText = new BigInteger(source);
            BigInteger biEnText = biText.modPow(d, n);
            return biEnText.ToHexString();
        }

        public static string Encrypt(byte[] source, string d, string n)
        {
            byte[] N = Convert.FromBase64String(n);
            byte[] D = Convert.FromBase64String(d);
            BigInteger biN = new BigInteger(N);
            BigInteger biD = new BigInteger(D);
            return Encrypt(source, biD, biN);
        }


        private static byte[] Decrypt(string source, BigInteger e, BigInteger n)
        {

            string block = source;
            BigInteger biText = new BigInteger(block, 16);
            BigInteger biEnText = biText.modPow(e, n);
            return biEnText.getBytes();
        }

        //public static string EncryptProcess(string source, string d, string n)
        //{
        //    byte[] N = Convert.FromBase64String(n);
        //    byte[] D = Convert.FromBase64String(d);
        //    BigInteger biN = new BigInteger(N);
        //    BigInteger biD = new BigInteger(D);
        //    return EncryptString(source, biD, biN);
        //}

        /* 
         解密过程,其中e、n是RSACryptoServiceProvider生成的Exponent、Modulus 
        */
        public static byte[] Decrypt(string source, string e, string n)
        {
            byte[] N = Convert.FromBase64String(n);
            byte[] E = Convert.FromBase64String(e);
            BigInteger biN = new BigInteger(N);
            BigInteger biE = new BigInteger(E);
            return Decrypt(source, biE, biN);
        }
    }
}
