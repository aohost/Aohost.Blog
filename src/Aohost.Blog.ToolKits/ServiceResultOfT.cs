using Aohost.Blog.ToolKits.Enum;

namespace Aohost.Blog.ToolKits
{
    public class ServiceResultOfT<T>:ServiceResult where T : class
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