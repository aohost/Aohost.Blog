namespace Aohost.Blog.Application
{
    public class HelloWorldService: ServiceBase, IHelloWorldService
    {
        public string HelloWorld()
        {
            return "Hello World!";
        }
    }
}