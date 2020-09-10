using Aohost.Blog.Domain;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace Aohost.Blog.EntityFrameworkCore.DbMigrations
{
    [DependsOn(typeof(BlogDomainModule), typeof(BlogFrameworkCoreModule))]
    public class BlogEntityFrameworkCoreDbMigrationsModule:AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<BlogMigrationsDbContext>();
        }
    }
}