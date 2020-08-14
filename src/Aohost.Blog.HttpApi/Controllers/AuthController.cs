﻿using System.Threading.Tasks;
using Aohost.Blog.Application.Authorize;
using Aohost.Blog.Domain.Shared;
using Aohost.Blog.ToolKits;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace Aohost.Blog.HttpApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [AllowAnonymous]
    [ApiExplorerSettings(GroupName = Grouping.GroupName_v4)]
    public class AuthController : AbpController
    {
        private readonly IAuthorizeService _authorizeService;

        public AuthController(IAuthorizeService authorizeService)
        {
            _authorizeService = authorizeService;
        }

        /// <summary>
        /// 获取登录地址(GitHub)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ServiceResult<string>> GetLoginAddressAsync()
        {
            return await _authorizeService.GetLoginAddressAsync();
        }

        [HttpGet]
        [Route("access_token")]
        public async Task<ServiceResult<string>> GetAccessTokenAsync(string code)
        {
            return await _authorizeService.GetLoginAddressAsync();
        }
    }
}