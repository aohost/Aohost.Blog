using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Aohost.Blog.Domain.Configuration;
using Aohost.Blog.ToolKits;
using Aohost.Blog.ToolKits.GitHub;

namespace Aohost.Blog.Application.Authorize.Impl
{
    public class AuthorizeService:ServiceBase, IAuthorizeService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AuthorizeService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ServiceResult<string>> GetLoginAddressAsync()
        {
            var result = new ServiceResult<string>();

            var request = new AuthorizeRequest();
            var address = string.Concat(new string[]
            {
                GitHubConfig.API_Authorize,
                "?client_id=", request.ClientId,
                "&scope=", request.Scope,
                "&state=", request.State,
                "&redirect_uri", request.RedirectUri
            });
            result.IsSuccess(address);
            return await Task.FromResult(result);
        }

        public async Task<ServiceResult<string>> GetAccessTokenAsync(string code)
        {
            var result = new ServiceResult<string>();
            if (string.IsNullOrEmpty(code))
            {
                result.IsFailed("Code为空");
                return result;
            }
            
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
        }

        public Task<ServiceResult<string>> GenerateTokenAsync(string access_token)
        {
            throw new System.NotImplementedException();
        }
    }
}