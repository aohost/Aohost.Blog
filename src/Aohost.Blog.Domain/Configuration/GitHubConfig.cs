namespace Aohost.Blog.Domain.Configuration
{
    public class GitHubConfig
    {
        /// <summary>
        /// GET请求，跳转GitHub登录界面，获取用户授权，得到code
        /// </summary>
        public static string API_Authorize = "https://github.com/login/oauth/authorize";

        /// <summary>
        /// POST请求，根据code得到access_token
        /// </summary>
        public static string API_AccessToken = "https://github.com/login/oauth/access_token";

        /// <summary>
        /// GET请求，根据access_token得到用户信息
        /// </summary>
        public static string API_User = "https://api.github.com/user";

        /// <summary>
        /// GitHub UserID
        /// </summary>
        public static int UserId = AppSettings.GitHub.UserId;

        /// <summary>
        /// Client ID
        /// </summary>
        public static string ClientId = AppSettings.GitHub.ClientId;

        /// <summary>
        /// Client Secret
        /// </summary>
        public static string ClientSecret = AppSettings.GitHub.ClientSecret;

        /// <summary>
        /// Authorization callback URL
        /// </summary>
        public static string RedirectUri = AppSettings.GitHub.RedirectUri;

        /// <summary>
        /// Application Name
        /// </summary>
        public static string ApplicationName = AppSettings.GitHub.ApplicationName;
    }
}