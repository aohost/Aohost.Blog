using Aohost.Blog.Domain;
using Aohost.Blog.Domain.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.SqlServer;
using Volo.Abp.Modularity;

namespace Aohost.Blog.EntityFrameworkCore
{
    [DependsOn(typeof(BlogDomainModule),
        typeof(AbpEntityFrameworkCoreModule),
        typeof(AbpEntityFrameworkCoreSqlServerModule))]
    public class BlogFrameworkCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<BlogDbContext>(options =>
            {
                options.AddDefaultRepositories(includeAllEntities: true);
            });

            Configure<AbpDbContextOptions>(options =>
            {
                switch (AppSettings.EnableDb)
                {
                    case "SqlServer":
                        options.UseSqlServer();
                        break;
                    default:
                        options.UseSqlServer();
                        break;
                }
            });
        }
    }
}