using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SV19T1081011.Lib
{
    /// <summary>
    /// Các hàm tiện ích và mở rộng phương thức liên quan đến dữ liệu kiểu chuỗi
    /// </summary>
    public static partial class StringUtils
    {
        /// <summary>
        /// Trích ra từ một chuỗi các giá trị số
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string GetNumericString(this string s)
        {
            string sTemp = "";
            for (int i = 0; i < s.Length; i++)
            {
                if (char.IsDigit(s[i]))
                {
                    sTemp += s[i];
                }
            }
            return sTemp;
        }
        /// <summary>
        /// Kiểm tra xem 1 chuỗi có phải là chuỗi số hay không
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsNumericString(this string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return false;
            }
            else
            {
                for (int i = 0; i < s.Length; i++)
                {
                    if (!char.IsDigit(s[i]))
                        return false;
                }
                return true;
            }
        }
        /// <summary>
        /// Mã hóa MD5
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string Md5(this string s)
        {
            return CryptHelper.EncodeMD5(s);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string Md5UsingUTF8(this string s)
        {
            return CryptHelper.EncodeMD5UsingUTF8(s);
        }
        /// <summary>
        /// Kiểm tra tên đăng nhập hợp lệ hay không?
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsNormalUserName(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return false;
            if (s.Length < 6 || s.Length > 50)
                return false;
            return Regex.IsMatch(s, @"^[_a-zA-Z0-9]+$");
        }
        /// <summary>
        /// Tạo số điện thoại hợp lệ theo mã vùng Việt Nam
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string NormalPhoneNumber(this string s)
        {
            s = s.Trim();
            if (s[0] == '0')
            {
                s = "84" + s.Substring(1);
            }

            return s;
        }
        /// <summary>
        /// Chuỗi là số điện thoại hợp lệ?
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsPhoneNumber(this string s)
        {
            return Regex.IsMatch(s, @"^[0-9]");
        }
        /// <summary>
        /// Cắt từ trong chuỗi
        /// </summary>
        /// <param name="s"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static string ExtractWords(this string s, int count)
        {
            try
            {
                string _result = string.Empty;
                var _s = s.Split(' ');
                int c = 1;
                foreach (var item in _s)
                {
                    if (c < count)
                    {
                        _result = _result + " " + item;
                        c = c + 1;
                    }
                }
                return _result.Trim();
            }
            catch
            {
                return "";
            }
        }
        /// <summary>
        /// Bỏ các ký tự định dạng HTML ra khỏi chuỗi
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string StripHtml(this string html)
        {
            try
            {
                if (html == null || html == string.Empty)
                    return string.Empty;

                return System.Text.RegularExpressions.Regex.Replace(html, "<[^>]*>", string.Empty);
            }
            catch
            {
                return "";
            }
        }
        /// <summary>
        /// Kiểm tra email hợp lệ
        /// </summary>
        /// <param name="Email"></param>
        /// <returns></returns>
        public static bool IsEmailValid(this string Email)
        {
            if (string.IsNullOrEmpty(Email))
                return false;
            return Regex.IsMatch(Email, @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
        }
        /// <summary>
        /// Kiểm tra xem mật khẩu có hợp lệ hay không
        /// </summary>
        /// <param name="Password"></param>
        /// <param name="minLength">Độ dài tối thiểu của mật khẩu</param>
        /// <param name="useComplexPassword">Có yêu cầu phải sử dụng mật khẩu phức tạp hay không?</param>
        /// <returns></returns>
        public static bool IsValidPassword(this string Password, int minLength = 6, bool useComplexPassword = false)
        {
            if (string.IsNullOrWhiteSpace(Password)) //Mật khẩu rỗng
            {
                return false;
            }
            if (Password.Length < minLength)
            {
                return false;
            }
            //Danh sách các password cấm sử dụng
            string[] _DeniedPasswords = new string[]
            {
                "123456",
                "1234567",
                "12345678",
                "123456789",
                "1234567890",
                "654321",
                "7654321",
                "87654321",
                "987654321",
                "0987654321",
                "abcdef",
                "qwerty",
                "abc123",
                "abc123456",
                "abcd123",
                "abcd1234",
                "password",
                "matkhau"
            };
            if (_DeniedPasswords.Contains(Password.ToLower()))
            {
                return false;
            }
            if (useComplexPassword)
            {
                Regex regexlittelnumber = new Regex(@"^(?=.*?[_A-Za-z])(?=.*?[0-9]).{8,}$");
                Regex regexnumber = new Regex(@"^(?=.*?[_A-Za-z])(?=.*?[#?!@$%^&*-]).{8,}$");
                Regex regexchar = new Regex(@"^(?=.*?[#?!@$%^&*-])(?=.*?[0-9]).{8,}$");
                if (regexlittelnumber.Match(Password).Success || regexnumber.Match(Password).Success || regexchar.Match(Password).Success)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return true;
        }        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GetRandomAlphanumericString(int length)
        {
            const string alphanumericCharacters =
                "ABCDEFGHIJKLMNPQRSTUVWXYZ" +
                "abcdefghijkmnpqrstuvwxyz" +
                "23456789";
            return GetRandomString(length, alphanumericCharacters);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GetRandomNumericString(int length)
        {
            const string numericCharacters = "0123456789";
            return GetRandomString(length, numericCharacters);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="length"></param>
        /// <param name="characterSet"></param>
        /// <returns></returns>
        private static string GetRandomString(int length, IEnumerable<char> characterSet)
        {
            if (length < 0)
                throw new ArgumentException("length must not be negative", "length");
            if (length > int.MaxValue / 4) // 500 million chars ought to be enough for anybody
                throw new ArgumentException("length is too big", "length");
            if (characterSet == null)
                throw new ArgumentNullException("characterSet");
            var characterArray = characterSet.Distinct().ToArray();
            if (characterArray.Length == 0)
                throw new ArgumentException("characterSet must not be empty", "characterSet");

            var bytes = new byte[length * 4];
            new RNGCryptoServiceProvider().GetBytes(bytes);
            var result = new char[length];
            for (int i = 0; i < length; i++)
            {
                uint value = BitConverter.ToUInt32(bytes, i * 4);
                result[i] = characterArray[value % characterArray.Length];
            }
            return new string(result);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_string"></param>
        /// <param name="charPlace"></param>
        /// <param name="start"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static string RegexPlace(string _string, string charPlace, int start, int count)
        {
            try
            {
                if (_string.Length >= start + count)
                {
                    string StrSearch = _string.Substring(start, count);
                    Regex regex = new Regex("[a-zA-Z0-9-\\~#%&*{}/:<>?|\"-_@.]");
                    return _string.Replace(StrSearch, regex.Replace(StrSearch, charPlace));
                }
                return _string;
            }
            catch
            {
            }
            return _string;
        }
        /// <summary>
        /// Tạo mật khẩu ngẫu nhiên với độ dài length
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GeneratePassword(int length)
        {
            string[] arrStr = new string[]
                    {
                        "0", "1", "2", "3", "4", "5", "6", "7", "8", "9"
                        //"a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "p", "q", "r", "s", "t", "u", "v"
                        //"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "P", "Q", "R", "S", "T", "U", "V",
                        //"*", "!", "#", "@"
                    };
            Random random = new Random();
            string s = "";
            int index = 0;
            for (int i = 0; i < length; i++)
            {
                index = random.Next(0, arrStr.Length - 1);
                s = s + arrStr[index];
            }
            return s;
        }
        /// <summary>
        /// Nén chuỗi
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string Compress(string text)
        {
            try
            {
                byte[] buffer = Encoding.UTF8.GetBytes(text);
                var memoryStream = new MemoryStream();
                using (var gZipStream = new GZipStream(memoryStream, CompressionMode.Compress, true))
                {
                    gZipStream.Write(buffer, 0, buffer.Length);
                }

                memoryStream.Position = 0;

                var compressedData = new byte[memoryStream.Length];
                memoryStream.Read(compressedData, 0, compressedData.Length);

                var gZipBuffer = new byte[compressedData.Length + 4];
                Buffer.BlockCopy(compressedData, 0, gZipBuffer, 4, compressedData.Length);
                Buffer.BlockCopy(BitConverter.GetBytes(buffer.Length), 0, gZipBuffer, 0, 4);
                return Convert.ToBase64String(gZipBuffer);
            }
            catch
            {
                return text;
            }
        }
        /// <summary>
        /// Giải nén chuỗi
        /// </summary>
        /// <param name="compressedText">Chuỗi cần giải nén</param>
        /// <returns></returns>
        public static string Decompress(string compressedText)
        {
            try
            {
                byte[] gZipBuffer = Convert.FromBase64String(compressedText);
                using (var memoryStream = new MemoryStream())
                {
                    int dataLength = BitConverter.ToInt32(gZipBuffer, 0);
                    memoryStream.Write(gZipBuffer, 4, gZipBuffer.Length - 4);

                    var buffer = new byte[dataLength];

                    memoryStream.Position = 0;
                    using (var gZipStream = new GZipStream(memoryStream, CompressionMode.Decompress))
                    {
                        gZipStream.Read(buffer, 0, buffer.Length);
                    }

                    return Encoding.UTF8.GetString(buffer);
                }
            }
            catch
            {
                return compressedText;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public static string Base64Encode(string plainText)
        {
            try
            {
                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
                return System.Convert.ToBase64String(plainTextBytes);
            }
            catch (Exception)
            {
                return "";
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="base64EncodedData"></param>
        /// <returns></returns>
        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string hmacSHA256(string data, string key)
        {
            using (HMACSHA256 hmac = new HMACSHA256(Encoding.ASCII.GetBytes(key)))
            {
                byte[] arr = hmac.ComputeHash(Encoding.ASCII.GetBytes(data));
                return BitConverter.ToString(arr).Replace("-", "").ToLower();
            }
        }

        /// <summary>
        /// Chuyển các ký tự Tiếng việt có dấu thành không dấu
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ToUnSign(this string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }
    }
}
