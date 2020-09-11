using System.Threading.Tasks;
using Aohost.Blog.Application.Authorize;
using Aohost.Blog.Domain.Shared;
using Aohost.Blog.ToolKits.Base;
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
        [Route("url")]
        public async Task<ServiceResult<string>> GetLoginAddressAsync()
        {
            return await _authorizeService.GetLoginAddressAsync();
        }

        [HttpGet]
        [Route("access_token")]
        public async Task<ServiceResult<string>> GetAccessTokenAsync(string code)
        {
            return await _authorizeService.GetAccessTokenAsync(code);
        }

        /// <summary>
        /// 登录成功，生成token
        /// </summary>
        /// <param name="access_token"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("token")]
        public async Task<ServiceResult<string>> GenerateTokenAsync(string access_token)
        {
            return await _authorizeService.GenerateTokenAsync(access_token);
        }

        /// <summary>
        /// 验证token是否合法
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<ServiceResult> VerifyToken(string token)
        {
            return await _authorizeService.VerifyToken(token);
        }
    }
}