using System;
using Aohost.Blog.ToolKits.Enum;

namespace Aohost.Blog.ToolKits
{
    public class ServiceResult
    {
        public ServiceResultCode Code { get; set; }

        public string Message { get; set; }

        public bool Success => Code == ServiceResultCode.Succeed;

        public long Timestamp { get; } = DateTime.Now.ToUniversalTime().Ticks;

        /// <summary>
        /// 响应成功
        /// </summary>
        /// <param name="message"></param>
        public void IsSuccess(string message = "")
        {
            Message = message;
            Code = ServiceResultCode.Succeed;
        }

        /// <summary>
        /// 响应失败
        /// </summary>
        /// <param name="exception"></param>
        public void IsFailed(Exception exception)
        {
            Message = exception.InnerException?.StackTrace;
            Code = ServiceResultCode.Failed;
        }
    }
}
