using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SV19T1081011.Lib
{
    /// <summary>
    /// Lớp cung cấp các hàm mã hóa/giải mã dữ liệu
    /// </summary>
    public partial class CryptHelper
    {        
        /// <summary>
        /// Hàm mã hóa chuỗi
        /// </summary>
        /// <param name="toEncrypt">Chuỗi cần mã hóa</param>
        /// <param name="key">Key dùng để mã hóa</param>
        /// <param name="useMd5HashingKey">Có mã hóa MD5 đối với key hay không?</param>
        /// <returns></returns>
        public static string Encrypt(string toEncrypt, string key, bool useMd5HashingKey)
        {
            try
            {
                byte[] keyArray;
                byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

                if (useMd5HashingKey)
                {
                    MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                    keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                }
                else
                    keyArray = UTF8Encoding.UTF8.GetBytes(key);

                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
                tdes.Key = keyArray;
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = tdes.CreateEncryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                return Convert.ToBase64String(resultArray, 0, resultArray.Length);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Hàm giải mã chuỗi
        /// </summary>
        /// <param name="toDecrypt">Chuỗi cần giải mã</param>
        /// <param name="key">Key dùng để giải mã</param>
        /// <param name="useMd5HashingKey">Có mã hóa MD5 đối với key hay không?</param>
        /// <returns></returns>
        public static string Decrypt(string toDecrypt, string key, bool useMd5HashingKey)
        {
            try
            {
                byte[] keyArray;
                byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);

                if (useMd5HashingKey)
                {
                    MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                    keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                }
                else
                    keyArray = UTF8Encoding.UTF8.GetBytes(key);

                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
                tdes.Key = keyArray;
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = tdes.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                return UTF8Encoding.UTF8.GetString(resultArray);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Mã hóa MD5 cho chuỗi text
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string EncodeMD5(string text)
        {
            try
            {
                MD5 md5 = MD5CryptoServiceProvider.Create();
                byte[] dataMd5 = md5.ComputeHash(Encoding.Default.GetBytes(text));
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < dataMd5.Length; i++)
                    sb.AppendFormat("{0:x2}", dataMd5[i]);
                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Mã hóa MD5 cho chuỗi text (sử dụng encoding UTF-8)
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string EncodeMD5UsingUTF8(string text)
        {
            try
            {
                MD5 md5 = MD5CryptoServiceProvider.Create();
                byte[] dataMd5 = md5.ComputeHash(Encoding.UTF8.GetBytes(text));
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < dataMd5.Length; i++)
                    sb.AppendFormat("{0:x2}", dataMd5[i]);
                return sb.ToString();
            }
            catch
            {
                return "";
            }
        }
        /// <summary>
        /// Mã hóa HMAC SHA256
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string HMACSHA256(string data, string key)
        {
            using (HMACSHA256 hmac = new HMACSHA256(Encoding.ASCII.GetBytes(key)))
            {
                byte[] arr = hmac.ComputeHash(Encoding.ASCII.GetBytes(data));
                return BitConverter.ToString(arr).Replace("-", "").ToLower();
            }
        }
    }
}
