using Aohost.Blog.ToolKits.Helper;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Aohost.Blog.HttpApi.Hosting.Filters
{
    public class BlogExceptionFilter:IExceptionFilter
    {
        private readonly ILogger<BlogExceptionFilter> _logger;

        public BlogExceptionFilter(ILogger<BlogExceptionFilter> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 异常处理
        /// </summary>
        /// <param name="context"></param>
        public void OnException(ExceptionContext context)
        {
            // 日志记录
            _logger.LogError($"{context.HttpContext.Request.Path}|{context.Exception.Message}", context.Exception);
        }
    }
}