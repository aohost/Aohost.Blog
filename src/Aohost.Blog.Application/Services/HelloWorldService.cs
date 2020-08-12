using Volo.Abp.Application.Services;

namespace Aohost.Blog.Services
{
    public class HelloWorldService: BlogApplicationServiceBase, IHelloWorldService
    {
        public string HelloWorld()
        {
            return "Hello World!";
        }
    }
}