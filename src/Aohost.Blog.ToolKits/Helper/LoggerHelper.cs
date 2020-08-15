using System;

namespace Aohost.Blog.ToolKits.Helper
{
    public static class LoggerHelper
    {
        public static void WriteToFile(string message, Exception ex)
        {
            if (string.IsNullOrEmpty(message))
                message = ex.Message;

            //Log.Error(message, ex);
        }
    }
}