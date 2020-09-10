using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Aohost.Blog.EntityFrameworkCore.DbMigrations
{
    public class BlogMigrationsDbContextFactory: IDesignTimeDbContextFactory<BlogMigrationsDbContext>
    {
        public BlogMigrationsDbContext CreateDbContext(string[] args)
        {
            var config = BuildConfiguration();

            var enableDb = config["ConnectionStrings:Enable"];
            var builder = new DbContextOptionsBuilder<BlogMigrationsDbContext>();
            
            switch (enableDb)
            {
                case "Mysql":
                    builder.UseMySql(config.GetConnectionString(enableDb));
                    break;
                case "SqlServer":
                    builder.UseSqlServer(config.GetConnectionString(enableDb));
                    break;
                case "Sqlite":
                    builder.UseSqlite(config.GetConnectionString(enableDb));
                    break;
                case "PostgreSql":
                    builder.UseNpgsql(config.GetConnectionString(enableDb));
                    break;
                default:
                    builder.UseSqlServer(config.GetConnectionString(enableDb));
                    break;
            }

            return new BlogMigrationsDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true);

            return builder.Build();
        }
    }
}