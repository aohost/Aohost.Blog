using System;
using Aohost.Blog.Application.HelloWorld;
using Aohost.Blog.Domain.Shared;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace Aohost.Blog.HttpApi.Controllers
{
    [ApiController]
    [Route("/HelloWorld")]
    [ApiExplorerSettings(GroupName = Grouping.GroupName_v3)]
    public class HelloWorldController:AbpController
    {
        private readonly IHelloWorldService _helloWorldService;

        public HelloWorldController(IHelloWorldService helloWorldService)
        {
            _helloWorldService = helloWorldService;
        }

        /// <summary>
        /// HelloWorld
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string HelloWorld()
        {
            throw new Exception("This is a exception");
        }
    }
}