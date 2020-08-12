using Aohost.Blog.Services;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace Aohost.Blog.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController:AbpController
    {
        private readonly IHelloWorldService _helloWorldService;

        public HomeController(IHelloWorldService helloWorldService)
        {
            _helloWorldService = helloWorldService;
        }

        [HttpGet]
        public string Index()
        {
            return _helloWorldService.HelloWorld();
        }
    }
}