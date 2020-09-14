using System.Threading.Tasks;
using Aohost.Blog.ToolKits.Base;

namespace Aohost.Blog.Application.Authorize
{
    public interface IAuthorizeService
    {
        /// <summary>
        /// 获取登录地址(GitHub)
        /// </summary>
        /// <returns></returns>
        Task<ServiceResult<string>> GetLoginAddressAsync();

        /// <summary>
        /// 获取AccessToken
        /// </summary>
        /// <returns></returns>
        Task<ServiceResult<string>> GetAccessTokenAsync(string code);

        /// <summary>
        /// 登录成功，生成Token
        /// </summary>
        /// <param name="access_token"></param>
        /// <returns></returns>
        Task<ServiceResult<string>> GenerateTokenAsync(string access_token);

        /// <summary>
        /// 验证token是否正确
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<ServiceResult> VerifyToken(string token);
    }
}