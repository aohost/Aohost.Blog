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
                //.AddJsonFile($"appsettings.{}");

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

        /// <summary>
        /// JWT 认证相关配置
        /// </summary>
        public static class JWT
        {
            public static string Domain => _config["JWT:Domain"];

            public static string SecurityKey => _config["JWT:SecurityKey"];

            public static int Expires => Convert.ToInt32(_config["JWT:Expires"]);
        }

        /// <summary>
        /// Caching
        /// </summary>
        public static class Caching
        {
            /// <summary>
            /// Redis Connection String
            /// </summary>
            public static string RedisConnectionString => _config["Caching:RedisConnectionString"];
        }

        /// <summary>
        /// HangFire配置
        /// </summary>
        public static class HangFire
        {
            /// <summary>
            /// 登录名
            /// </summary>
            public static string Login => _config["HangFire:Login"];

            /// <summary>
            /// 登录密码
            /// </summary>
            public static string Password => _config["HangFire:Password"];
        }
    }
}