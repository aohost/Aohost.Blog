using Aohost.Blog.Application;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace Aohost.Blog
{
    [ApiController]
    [Route("{controller}")]
    public class HelloWorldController:AbpController
    {
        private readonly IHelloWorldService _helloWorldService;

        public HelloWorldController(IHelloWorldService helloWorldService)
        {
            _helloWorldService = helloWorldService;
        }

        [HttpGet]
        public IActionResult HelloWorld()
        {
            return Ok(_helloWorldService.HelloWorld());
        }
    }
}