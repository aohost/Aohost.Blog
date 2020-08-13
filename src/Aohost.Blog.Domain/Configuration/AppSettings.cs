using System.IO;
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
        }

        public static string EnableDb => _config["ConnectionStrings:Enable"];

        public static string ConnectionStrings => _config.GetConnectionString(EnableDb);
    }
}