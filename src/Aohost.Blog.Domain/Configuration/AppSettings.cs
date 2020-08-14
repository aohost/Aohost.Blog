using System;
using System.IO;
using System.Security;
using Microsoft.Extensions.Configuration;

namespace Aohost.Blog.Domain.Configuration
{
    public class AppSettings
    {
        private static readonly IConfigurationRoot _config;

        static AppSettings()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true);

            _config = builder.Build();
        }

        public static string EnableDb => _config["ConnectionStrings:Enable"];

        public static string ApiVersion => _config["ApiVersion"];

        public static string ConnectionStrings => _config.GetConnectionString(EnableDb);

        /// <summary>
        /// GitHub 第三方登录相关配置
        /// </summary>
        public static class GitHub
        {
            public static int UserId => Convert.ToInt32(_config["GitHub:UserId"]);

            public static string ClientId => _config["GitHub:ClientID"];

            public static string ClientSecret => _config["GitHub:ClientSecret"];

            public static string RedirectUri => _config["Github:RedirectUri"];

            public static string ApplicationName => _config["GitHub:ApplicationName"];
        }

        public static class JWT
        {
            public static string Domain => _config["JWT:Domain"];

            public static string SecurityKey => _config["JWT:SecurityKey"];

            public static int Expires => Convert.ToInt32(_config["JWT:Expires"]);
        }
    }
}