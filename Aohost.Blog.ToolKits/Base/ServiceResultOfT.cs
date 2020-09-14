using Aohost.Blog.ToolKits.Base.Enum;

namespace Aohost.Blog.ToolKits.Base
{
    public class ServiceResult<T> : ServiceResult where T : class
    {
        public T Result { get; set; }

        public void IsSuccess(T result = null, string message = "")
        {
            Message = message;
            Result = result;
            Code = ServiceResultCode.Succeed;
        }
    }
}