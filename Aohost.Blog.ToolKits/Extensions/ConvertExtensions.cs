using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using Aohost.Blog.ToolKits.Base;

namespace Aohost.Blog.ToolKits.Extensions
{
    public static class ConvertExtensions
    {
        /// <summary>
        /// String转int
        /// </summary>
        /// <param name="input"></param>
        /// <param name="defaultNum"></param>
        /// <returns></returns>
        public static int TryToInt(this object input, int defaultNum = 0)
        {
            if (input == null)
            {
                return defaultNum;
            }

            return int.TryParse(input.ToString(), out var num) ? num : defaultNum;
        }

        /// <summary>
        /// String转long
        /// </summary>
        /// <param name="input"></param>
        /// <param name="defaultNum"></param>
        /// <returns></returns>
        public static long TryToLong(this object input, long defaultNum = 0)
        {
            if (input == null)
            {
                return defaultNum;
            }

            return long.TryParse(input.ToString(), out var num) ? num : defaultNum;
        }

        /// <summary>
        /// string转double
        /// </summary>
        /// <param name="input"></param>
        /// <param name="defaultNum"></param>
        /// <returns></returns>
        public static double TryToDouble(this object input, double defaultNum = 0)
        {
            if (input == null)
            {
                return defaultNum;
            }

            return double.TryParse(input.ToString(), out var num) ? num : defaultNum;
        }

        /// <summary>
        /// string转float
        /// </summary>
        /// <param name="input"></param>
        /// <param name="defaultNum"></param>
        /// <returns></returns>
        public static double TryToFloat(this object input, float defaultNum = 0)
        {
            if (input == null)
            {
                return defaultNum;
            }

            return float.TryParse(input.ToString(), out var num) ? num : defaultNum;
        }


        /// <summary>
        /// string转bool
        /// </summary>
        /// <param name="input">输入</param>
        /// <param name="falseVal"></param>
        /// <param name="defaultBool">转换失败默认值</param>
        /// <param name="trueVal"></param>
        /// <returns></returns>
        public static bool TryToBool(this object input, bool defaultBool = false, string trueVal = "1", string falseVal = "0")
        {
            if (input == null)
                return defaultBool;

            var str = input.ToString();
            if (bool.TryParse(str, out var outBool))
            {
                return outBool;
            }

            outBool = defaultBool;

            if (trueVal == str)
            {
                return true;
            }

            return falseVal != str && outBool;
        }

        /// <summary>
        /// 值类型转string
        /// </summary>
        /// <param name="inputObj"></param>
        /// <param name="defaultStr"></param>
        /// <returns></returns>
        public static string TryToString(this ValueType inputObj, string defaultStr = "")
        {
            var output = inputObj.IsNull() ? defaultStr : inputObj.ToString();
            return output;
        }

        /// <summary>
        /// string转datetime
        /// </summary>
        /// <param name="inputStr"></param>
        /// <param name="format"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static DateTime TryToDatetime(this string inputStr, string format, DateTime defaultValue = default)
        {
            if (string.IsNullOrEmpty(inputStr))
            {
                return default;
            }

            return DateTime.TryParseExact(inputStr, format, CultureInfo.CurrentCulture, DateTimeStyles.None,
                out var outputDatetime)
                ? outputDatetime
                : defaultValue;
        }

        /// <summary>
        /// 时间戳转时间
        /// </summary>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        public static DateTime TryToDatetime(this string timestamp)
        {
            var ticks = 621355968000000000 + long.Parse(timestamp) * 10000;
            return new DateTime(ticks);
        }

        /// <summary>
        /// 时间格式转字符串
        /// </summary>
        /// <param name="date"></param>
        /// <param name="format"></param>
        /// <param name="cultureInfo"></param>
        /// <returns></returns>
        public static string TryToDatetime(this DateTime date, string format = "MMMM dd, yyyy HH:mm:ss",
            string cultureInfo = "en-us")
        {
            return date.ToString(format, new CultureInfo(cultureInfo));
        }

        /// <summary>
        /// 字符串去空格
        /// </summary>
        /// <param name="inputStr"></param>
        /// <returns></returns>
        public static string TryToTrim(this string inputStr)
        {
            return inputStr.IsNullOrEmpty() ? inputStr : inputStr.Trim();
        }

        /// <summary>
        /// 字符串转枚举
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static T TryToEnum<T>(this string str, T t = default) where T : struct
        {
            return System.Enum.TryParse<T>(str, out var result) ? result : t;
        }

        public static IEnumerable<EnumResponse> TryToList(this Type type)
        {
            var result = new List<EnumResponse>();

            foreach (var value in System.Enum.GetValues(type))
            {
                var response = new EnumResponse
                {
                    Key = value.ToString(),
                    Value = Convert.ToInt32(value)
                };

                var objArray = value.GetType().GetField(value.ToString())
                    .GetCustomAttributes(typeof(DescriptionAttribute), true);

                if (objArray.Any())
                {
                    response.Description = (objArray.First() as DescriptionAttribute)?.Description;
                }
                result.Add(response);
            }

            return result;
        }
    }
}