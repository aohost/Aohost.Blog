using System;
using Aohost.Blog.Domain.Configuration;

namespace Aohost.Blog.ToolKits.GitHub
{
    public class AuthorizeRequest
    {
        public string ClientId = GitHubConfig.ClientId;

        public string RedirectUri = GitHubConfig.RedirectUri;

        public string State { get; set; } = Guid.NewGuid().ToString("N");

        public string Scope { get; set; } = "user,public_repo";
    }
}