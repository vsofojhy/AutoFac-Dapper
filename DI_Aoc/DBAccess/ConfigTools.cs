using System;
using System.Collections.Generic;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace DBAccess
{
    public class ConfigTools
    {
        private static string _connection = "";

        /// <summary>
        /// 数据库2
        /// </summary>
        private static string _connection2 = "";


        /// <summary>
        /// 主数据库连接字符串
        /// </summary>
        public static string GetConnection()
        {
            if (_connection == "")
            {
                //开发环境数据库地址
                _connection = ConfigurationManager.AppSettings["Connection"];
                if (string.IsNullOrEmpty(_connection))
                {
                    //生产环境
                    _connection = Decrypt(ConfigurationManager.ConnectionStrings["Connection"].ToString());
                }
            }
            return _connection;
        }

        /// <summary>
        /// 数据库2连接字符串
        /// </summary>
        public static string GetConnection2()
        {
            if (_connection2 == "")
            {
                _connection2 = ConfigurationManager.AppSettings["Connection2"];
                if (string.IsNullOrEmpty(_connection2))
                {
                    _connection2 = Decrypt(ConfigurationManager.ConnectionStrings["Connection2"].ToString());
                }
            }
            return _connection2;
        }



        #region 对称加 解密

        public static readonly string sKey = "GF%^.456fdaDa6";
        /// <summary>
        /// 3des解密字符串
        /// </summary>
        /// <param name="sSource">要解密的字符串</param>
        ///  
        /// <remarks>静态方法，指定编码方式</remarks>
        public static string Decrypt(string sSource)
        {
            return Decrypt3DES(sSource, sKey, Encoding.UTF8);
        }

        /// <summary>
        /// 3des解密字符串
        /// </summary>
        /// <param name="sSource">要解密的字符串</param>
        /// <param name="sKey">密钥</param>
        /// <param name="eEncoding">编码方式</param>
        /// <returns>解密后的字符串</returns>
        /// <exception cref="">密钥错误</exception>
        /// <remarks>静态方法，指定编码方式</remarks>
        public static string Decrypt3DES(string sSource, string sKey, Encoding eEncoding)
        {
            string result = "";

            try
            {
                TripleDESCryptoServiceProvider DES = new TripleDESCryptoServiceProvider();
                MD5CryptoServiceProvider hashMD5 = new MD5CryptoServiceProvider();

                DES.Key = hashMD5.ComputeHash(eEncoding.GetBytes(sKey));
                DES.Mode = CipherMode.ECB;

                ICryptoTransform DESDecrypt = DES.CreateDecryptor();

                byte[] Buffer = Convert.FromBase64String(sSource);
                result = eEncoding.GetString(DESDecrypt.TransformFinalBlock(Buffer, 0, Buffer.Length));
            }
            catch {; }
            return result;
        }

        /// <summary>
        /// 3des加密字符串
        /// </summary>
        /// <param name="sSource">要加密的字符串</param>
        /// <param name="sKey">密钥</param>
        /// <param name="eEncoding">编码方式</param>
        /// <returns>加密后并经base64编码的字符串</returns>
        /// <remarks>重载，指定编码方式</remarks>
        public static string Encrypt3DES(string sSource, string sKey, Encoding eEncoding)
        {
            TripleDESCryptoServiceProvider DES = new TripleDESCryptoServiceProvider();
            MD5CryptoServiceProvider hashMD5 = new MD5CryptoServiceProvider();

            DES.Key = hashMD5.ComputeHash(eEncoding.GetBytes(sKey));
            DES.Mode = CipherMode.ECB;

            ICryptoTransform DESEncrypt = DES.CreateEncryptor();

            byte[] Buffer = eEncoding.GetBytes(sSource);
            return Convert.ToBase64String(DESEncrypt.TransformFinalBlock(Buffer, 0, Buffer.Length));
        }

        #endregion
    }
}
