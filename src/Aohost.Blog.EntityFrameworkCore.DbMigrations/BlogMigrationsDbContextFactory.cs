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

            var builder =
                new DbContextOptionsBuilder<BlogMigrationsDbContext>().UseSqlServer("Server=(LocalDb)\\MSSQLLocalDB;Database=Blog;Trusted_Connection=True;MultipleActiveResultSets=true");
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