using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using Aohost.Blog.Caching.Authorize;
using Aohost.Blog.Domain.Configuration;
using Aohost.Blog.ToolKits.Base;
using Aohost.Blog.ToolKits.Extensions;
using Aohost.Blog.ToolKits.GitHub;
using Microsoft.IdentityModel.Tokens;

namespace Aohost.Blog.Application.Authorize.Impl
{
    public class AuthorizeService:ServiceBase, IAuthorizeService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IAuthorizeCacheService _authorizeCacheService;

        public AuthorizeService(IHttpClientFactory httpClientFactory, IAuthorizeCacheService authorizeCacheService)
        {
            _httpClientFactory = httpClientFactory;
            _authorizeCacheService = authorizeCacheService;
        }

        /// <summary>
        /// 获取GitHub登录地址
        /// </summary>
        /// <returns></returns>
        public async Task<ServiceResult<string>> GetLoginAddressAsync()
        {
            return await _authorizeCacheService.GetLoginAddressAsync(async () =>
            {
                var result = new ServiceResult<string>();

                var request = new AuthorizeRequest();
                var address = string.Concat(new string[]
                {
                    GitHubConfig.API_Authorize,
                    "?client_id=", request.ClientId,
                    "&scope=", request.Scope,
                    "&state=", request.State,
                    "&redirect_uri=", request.RedirectUri
                });

                result.IsSuccess(address);
                return await Task.FromResult(result);
            });
        }

        /// <summary>
        /// 获取AccessToken
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public async Task<ServiceResult<string>> GetAccessTokenAsync(string code)
        {
            var result = new ServiceResult<string>();
            if (string.IsNullOrEmpty(code))
            {
                result.IsFailed("Code为空");
                return result;
            }

            return await _authorizeCacheService.GenerateTokenAsync(code, async ()=>
            {
                var request = new AccessTokenRequest();

                var content =
                    new StringContent(
                        $"code={code}&client_id={request.ClientId}&redirect_uri={request.RedirectUri}&client_secret={request.ClientSecret}");
                content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

                var client = _httpClientFactory.CreateClient();
                var httpResponse = await client.PostAsync(GitHubConfig.API_AccessToken, content);

                var response = await httpResponse.Content.ReadAsStringAsync();
                if (response.StartsWith("access_token"))
                {
                    result.IsSuccess(response.Split("=")[1].Split("&").First());
                }
                else
                {
                    result.IsFailed("Code不正确");
                }

                return result;
            });
        }

        /// <summary>
        /// 登录成功，生成Token
        /// </summary>
        /// <param name="access_token"></param>
        /// <returns></returns>
        public async Task<ServiceResult<string>> GenerateTokenAsync(string access_token)
        {
            var result = new ServiceResult<string>();

            if (string.IsNullOrEmpty(access_token))
            {
                result.IsFailed("access_token为空");
                return result;
            }

            return await _authorizeCacheService.GenerateTokenAsync(access_token, async () =>
            {
                var url = $"{GitHubConfig.API_User}?access_token={access_token}";
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Add("User-Agent",
                    "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/83.0.4103.14 Safari/537.36 Edg/83.0.478.13");
                var httpResponse = await client.GetAsync(url);
                if (httpResponse.StatusCode != HttpStatusCode.OK)
                {
                    result.IsFailed("access_token不正确");
                    return result;
                }

                var content = await httpResponse.Content.ReadAsStringAsync();
                var user = content.FromJson<UserResponse>();
                if (user == null)
                {
                    result.IsFailed("未获取到用户数据");
                    return result;
                }

                if (user.Id != GitHubConfig.UserId)
                {
                    result.IsFailed("当前账号未授权");
                    return result;
                }

                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
                    new Claim(JwtRegisteredClaimNames.Exp,
                        $"{new DateTimeOffset(DateTime.Now.AddMinutes(AppSettings.JWT.Expires)).ToUnixTimeSeconds()}"),
                    new Claim(JwtRegisteredClaimNames.Nbf, $"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}")
                };

                var key = new SymmetricSecurityKey(AppSettings.JWT.SecurityKey.SerializeUtf8());
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var securityToken = new JwtSecurityToken(
                    issuer: AppSettings.JWT.Domain,
                    audience: AppSettings.JWT.Domain,
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(AppSettings.JWT.Expires),
                    signingCredentials: creds);

                var token = new JwtSecurityTokenHandler().WriteToken(securityToken);

                result.IsSuccess(token);
                return await Task.FromResult(result);
            });
        }

        /// <summary>
        /// 验证token是否正确
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<ServiceResult> VerifyToken(string token)
        {
            var result = new ServiceResult();

            var jwt = new JwtSecurityToken(token).Claims;

            var name = jwt.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
            var email = jwt.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;

            if (name != "Aohost")
                result.IsFailed("token不正确");

            return await Task.FromResult(result);
        }
    }
}