using Aohost.Blog.Domain.Configuration;

namespace Aohost.Blog.ToolKits.GitHub
{
    public class AccessTokenRequest
    {
        public string ClientId = GitHubConfig.ClientId;

        public string ClientSecret = GitHubConfig.ClientSecret;

        public string Code { get; set; }

        public string RedirectUri = GitHubConfig.RedirectUri;
        public string State { get; set; }
    }
}