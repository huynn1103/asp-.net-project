using System;

namespace SV19T1081011.Lib
{
    /// <summary>
    /// Lớp cung cấp các hàm dùng để chuyển đổi kiểu dữ liệu thông dụng
    /// </summary>
    public partial class Converter
    {
        #region Chuyển đổi sang giá trị số

        /// <summary>
        /// Đổi sang kiểu int (trả về 0 nếu lỗi)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int ToInt(object value)
        {
            try
            {
                return Convert.ToInt32(Convert.ToDouble(value));
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Chuyển sang kiểu int (trả về errorValue nếu lỗi)
        /// </summary>
        /// <param name="value"></param>
        /// <param name="errorValue"></param>
        /// <returns></returns>
        public static int ToInt(object value, int errorValue)
        {
            try
            {
                return Convert.ToInt32(Convert.ToDouble(value));
            }
            catch
            {
                return errorValue;
            }
        }

        /// <summary>
        /// Chuyển sang kiểu long (trả về 0 nếu lỗi)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static long ToLong(object value)
        {
            try
            {
                return Convert.ToInt64(Convert.ToDouble(value));
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Chuyển sang kiểu long (trả về errorValue nếu lỗi)
        /// </summary>
        /// <param name="value"></param>
        /// <param name="errorValue"></param>
        /// <returns></returns>
        public static long ToLong(object value, long errorValue)
        {
            try
            {
                return Convert.ToInt64(Convert.ToDouble(value));
            }
            catch
            {
                return errorValue;
            }
        }

        /// <summary>
        /// Chuyển sang kiểu decimal (trả về 0 nếu lỗi)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static decimal ToDecimal(object value)
        {
            try
            {
                return Convert.ToDecimal(value);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Chuyển sang kiểu decimal (trả về errorValue nếu lỗi)
        /// </summary>
        /// <param name="value"></param>
        /// <param name="errorValue"></param>
        /// <returns></returns>
        public static decimal ToDecimal(object value, decimal errorValue)
        {
            try
            {
                return Convert.ToDecimal(value);
            }
            catch
            {
                return errorValue;
            }
        }

        /// <summary>
        /// Chuyển sang kiểu double (trả về 0 nếu lỗi)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double ToDouble(object value)
        {
            try
            {
                return Convert.ToDouble(value);
            }
            catch
            {
                return 0.0;
            }
        }

        /// <summary>
        /// Chuyển sang kiểu double (trả về errorValue nếu lỗi)
        /// </summary>
        /// <param name="value"></param>
        /// <param name="errorValue"></param>
        /// <returns></returns>
        public static double ToDouble(object value, double errorValue)
        {
            try
            {
                return Convert.ToDouble(value);
            }
            catch
            {
                return errorValue;
            }
        }

        #endregion

        #region Chuyển đổi sang chuỗi

        /// <summary>
        /// Chuyển sang chuỗi (trả về "" nếu lỗi)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToString(object value)
        {
            try
            {
                return Convert.ToString(value);
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// Chuyển object sang chuỗi (trả về errorValue nếu lỗi)
        /// </summary>
        /// <param name="value"></param>
        /// <param name="errorValue"></param>
        /// <returns></returns>
        public static string ToString(object value, string errorValue)
        {
            try
            {
                return Convert.ToString(value);
            }
            catch
            {
                return errorValue;
            }
        }

        /// <summary>
        /// Chuyển object (DateTime) sang chuỗi ngày theo định dạng dd/MM/yyyy (trả về chuỗi rỗng nếu lỗi)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToShortDateString(object value)
        {
            try
            {
                return string.Format("{0:dd/MM/yyyy}", value);
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// Chuyển object (DateTime) sang chuỗi ngày theo định dạng dd/MM/yyyy (trả về errorValue nếu lỗi)
        /// </summary>
        /// <param name="value"></param>
        /// <param name="errorValue"></param>
        /// <returns></returns>
        public static string ToShortDateString(object value, string errorValue)
        {
            try
            {
                return string.Format("{0:dd/MM/yyyy}", value);
            }
            catch
            {
                return errorValue;
            }
        }

        /// <summary>
        /// Chuyển object (DateTime) sang chuỗi ngày giờ theo định dạng dd/MM/yyyy hh:mm:ss
        /// (trả về chuỗi rỗng nếu lỗi)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToLongDateString(object value)
        {
            try
            {
                return string.Format("{0:dd/MM/yyyy HH:mm:ss}", value);
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// Chuyển object (DateTime) sang chuỗi ngày giờ theo định dạng dd/MM/yyyy hh:mm:ss
        /// (trả về errorValue nếu lỗi)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToLongDateString(object value, string errorValue)
        {
            try
            {
                return string.Format("{0:dd/MM/yyyy HH:mm:ss}", value);
            }
            catch
            {
                return errorValue;
            }
        }

        /// <summary>
        /// Chuyển object (DateTime) sang chuỗi giờ theo định dạng hh:mm:ss
        /// (trả về chuỗi rỗng nếu lỗi)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToTimeString(object value)
        {
            try
            {
                return string.Format("{0:HH:mm:ss}", value);
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// Chuyển object (DateTime) sang chuỗi giờ theo định dạng hh:mm:ss
        /// (trả về chuỗi rỗng nếu lỗi)
        /// </summary>
        /// <param name="value"></param>
        /// <param name="errorValue"></param>
        /// <returns></returns>
        public static string ToTimeString(object value, string errorValue)
        {
            try
            {
                return string.Format("{0:HH:mm:ss}", value);
            }
            catch
            {
                return errorValue;
            }
        }

        #endregion

        #region Chuyển đổi kiểu ngày

        /// <summary>
        /// Chuyển sang kiểu ngày, nếu lỗi trả về 1/1/1900
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(object value)
        {
            try
            {
                return Convert.ToDateTime(value);
            }
            catch
            {
                return new DateTime(1900, 1, 1);
            }
        }

        /// <summary>
        /// Chuyển sang kiểu ngày, nếu lỗi trả về errorValue
        /// </summary>
        /// <param name="value"></param>
        /// <param name="errorValue"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(object value, DateTime errorValue)
        {
            try
            {
                return Convert.ToDateTime(value);
            }
            catch
            {
                return errorValue;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static long ToUnixTimestamp(DateTime value)
        {
            try
            {
                TimeSpan span = (value - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime());
                return (long)span.TotalSeconds;
            }
            catch
            {
                return 0;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime? ToNullableDateTime(object value)
        {
            try
            {
                if (value == null || value == DBNull.Value)
                    return null;
                return Convert.ToDateTime(value);
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// Chuyển đổi chuỗi sang giá trị kiểu ngày (hàm trả về null nếu không convert được)
        /// </summary>
        /// <param name="value">Chuỗi cần chuyển đổi</param>
        /// <param name="format">Chuỗi định dạng: DMY, MDY, YMD</param>
        /// <returns></returns>
        public static DateTime? ToNullableDateFromString(string value, string format)
        {
            try
            {
                format = format.ToUpper();
                string[] arrValues = value.Split(new char[] { '/', '-', '.' });
                if (arrValues.Length != 3)
                {
                    return null;
                }
                else
                {
                    switch (format)
                    {
                        case "DMY":
                            return new DateTime(ToInt(arrValues[2]), ToInt(arrValues[1]), ToInt(arrValues[0]));
                        case "MDY":
                            return new DateTime(ToInt(arrValues[2]), ToInt(arrValues[0]), ToInt(arrValues[1]));
                        case "YMD":
                            return new DateTime(ToInt(arrValues[0]), ToInt(arrValues[1]), ToInt(arrValues[2]));
                        default:
                            return null;
                    }

                }
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// Lấy ngày thứ 2 đầu tuần của 1 ngày bất kỳ trong tuần
        /// </summary>
        /// <param name="d">Ngày bất kỳ trong tuần</param>
        /// <returns></returns>
        public static DateTime FirstDateOfWeek(DateTime d)
        {
            switch (d.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    return d;
                case DayOfWeek.Tuesday:
                    return d.AddDays(-1);
                case DayOfWeek.Wednesday:
                    return d.AddDays(-2);
                case DayOfWeek.Thursday:
                    return d.AddDays(-3);
                case DayOfWeek.Friday:
                    return d.AddDays(-4);
                case DayOfWeek.Saturday:
                    return d.AddDays(-5);
                default:
                    return d.AddDays(-6);
            }
        }
        /// <summary>
        /// Lấy ngày cuối tuần (chủ nhật) của một ngày bất kỳ trong tuần
        /// </summary>
        /// <param name="d">Ngày bất kỳ trong tuần</param>
        /// <returns></returns>
        public static DateTime LastDateOfWeek(DateTime d)
        {
            switch (d.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    return d.AddDays(6);
                case DayOfWeek.Tuesday:
                    return d.AddDays(5);
                case DayOfWeek.Wednesday:
                    return d.AddDays(4);
                case DayOfWeek.Thursday:
                    return d.AddDays(3);
                case DayOfWeek.Friday:
                    return d.AddDays(2);
                case DayOfWeek.Saturday:
                    return d.AddDays(1);
                default:
                    return d;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="TimeMilliseconds"></param>
        /// <returns></returns>
        public static DateTime TimeMillisecondsToDateTime(double TimeMilliseconds)
        {
            return (new DateTime(1970, 1, 1)).AddMilliseconds(TimeMilliseconds);
        }

        /// <summary>
        /// Chuyển 1 chuỗi ngày tháng có dạng yyyyMMddHHmmss sang kiểu DateTime
        /// </summary>
        /// <param name="dmy"></param>
        /// <returns></returns>
        public static DateTime YMDHmsStringToDateTime(string dmy)
        {
            try
            {
                string formatString = "yyyyMMddHHmmss";
                DateTime dt = DateTime.ParseExact(dmy, formatString, null);
                return dt;
            }
            catch
            {
                return new DateTime(1900, 1, 1);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime DMYStringToDateTime(string value, DateTime errorValue)
        {
            try
            {
                string[] arr = value.Split(new char[] { '/', '-' });
                if (arr.Length < 3)
                {
                    return errorValue;
                }
                return new DateTime(ToInt(arr[2]), ToInt(arr[1]), ToInt(arr[0]));
            }
            catch
            {
                return errorValue;
            }
        }

        #endregion

        #region Chuyển đổi kiểu bool

        /// <summary>
        /// Chuyển đổi sang kiểu bool, trả về false nếu lỗi
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool ToBoolean(object value)
        {
            try
            {
                return Convert.ToBoolean(value);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Chuyển sang kiểu bool, trả về errorValue nếu lỗi
        /// </summary>
        /// <param name="value"></param>
        /// <param name="errorValue"></param>
        /// <returns></returns>
        public static bool ToBoolean(object value, bool errorValue)
        {
            try
            {
                return Convert.ToBoolean(value);
            }
            catch
            {
                return errorValue;
            }
        }
        
        #endregion
        
    }
}
