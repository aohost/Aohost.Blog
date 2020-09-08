using System;
using System.Threading.Tasks;
using Aohost.Blog.ToolKits.Base;

namespace Aohost.Blog.Caching.Authorize
{
    public interface IAuthorizeCacheService
    {
        /// <summary>
        /// 获取GitHub登录地址
        /// </summary>
        /// <param name="factory"></param>
        /// <returns></returns>
        Task<ServiceResult<string>> GetLoginAddressAsync(Func<Task<ServiceResult<string>>> factory);

        /// <summary>
        /// 获取AccessToken
        /// </summary>
        /// <param name="code"></param>
        /// <param name="factory"></param>
        /// <returns></returns>
        Task<ServiceResult<string>> GetAccessTokenAsync(string code, Func<Task<ServiceResult<string>>> factory);

        /// <summary>
        /// 登录成功，生成token
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="factory"></param>
        /// <returns></returns>
        Task<ServiceResult<string>> GenerateTokenAsync(string access_token, Func<Task<ServiceResult<string>>> factory);
    }
}