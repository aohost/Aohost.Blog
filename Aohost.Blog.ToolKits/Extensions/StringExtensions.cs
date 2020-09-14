namespace Aohost.Blog.ToolKits.Extensions
{
    public static class StringExtensions
    {
        #region 空判断

        /// <summary>
        /// </summary>
        /// <param name="inputStr"></param>
        /// <returns></returns>
        //public static bool IsNullOrEmpty(this string inputStr)
        //{
        //    return string.IsNullOrEmpty(inputStr);
        //}

        public static bool IsNullOrWhiteSpace(this string inputStr)
        {
            return string.IsNullOrWhiteSpace(inputStr);
        }

        public static bool IsNotNullOrEmpty(this string inputStr)
        {
            return !string.IsNullOrEmpty(inputStr);
        }

        public static bool IsNotNullOrWhiteSpace(this string inputStr)
        {
            return !string.IsNullOrWhiteSpace(inputStr);
        }

        #endregion

        #region Format

        /// <summary>
        /// Format
        /// </summary>
        /// <param name="format"></param>
        /// <param name="arg0"></param>
        /// <param name="arg1"></param>
        /// <returns></returns>
        public static string Format(this string format, object arg0, object arg1)
        {
            return string.Format(format, arg0, arg1);
        }

        /// <summary>
        /// Format
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string Format(this string format, object[] args)
        {
            return string.Format(format, args);
        }

        /// <summary>
        /// Format
        /// </summary>
        /// <param name="format"></param>
        /// <param name="arg0"></param>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        /// <returns></returns>
        public static string Format(this string format, object arg0, object arg1, object arg2)
        {
            return string.Format(format, arg0, arg1, arg2);
        }

        /// <summary>
        /// Format
        /// </summary>
        /// <param name="format"></param>
        /// <param name="arg0"></param>
        /// <returns></returns>
        public static string Format(this string format, object arg0)
        {
            return string.Format(format, arg0);
        }

        /// <summary>
        /// FormatWith
        /// </summary>
        /// <param name="this"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static string FormatWith(this string @this, params object[] values)
        {
            return string.Format(@this, values);
        }

        /// <summary>
        /// FormatWith
        /// </summary>
        /// <param name="this"></param>
        /// <param name="arg0"></param>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        /// <returns></returns>
        public static string FormatWith(this string @this, object arg0, object arg1, object arg2)
        {
            return string.Format(@this, arg0, arg1, arg2);
        }

        /// <summary>
        /// FormatWith
        /// </summary>
        /// <param name="this"></param>
        /// <param name="arg0"></param>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        /// <returns></returns>
        public static string FormatWith(this string @this, object arg0, object arg1)
        {
            return string.Format(@this, arg0, arg1);
        }

        /// <summary>
        /// FormatWith
        /// </summary>
        /// <param name="this"></param>
        /// <param name="arg0"></param>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        /// <returns></returns>
        public static string FormatWith(this string @this, object arg0)
        {
            return string.Format(@this, arg0);
        }
        #endregion
    }
}