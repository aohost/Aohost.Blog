using Aohost.Blog.Caching;
using Volo.Abp.AutoMapper;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;

namespace Aohost.Blog.Application
{
    [DependsOn(typeof(AbpIdentityApplicationModule),
        typeof(AbpAutoMapperModule),
        typeof(BlogApplicationCachingModule))]
    public class BlogApplicationModule:AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<BlogApplicationModule>(validate: true);
                options.AddProfile<BlogAutoMapperProfile>(validate: true);
            });
        }
    }
}