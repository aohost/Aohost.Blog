namespace Aohost.Blog.Application.HelloWorld
{
    public class HelloWorldService: ServiceBase, IHelloWorldService
    {
        public string HelloWorld()
        {
            return "Hello World!";
        }
    }
}