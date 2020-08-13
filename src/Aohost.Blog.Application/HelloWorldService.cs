namespace Aohost.Blog.Application
{
    public class HelloWorldService: BlogApplicationServiceBase, IHelloWorldService
    {
        public string HelloWorld()
        {
            return "Hello World!";
        }
    }
}