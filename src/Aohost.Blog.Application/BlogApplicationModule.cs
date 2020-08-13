using Volo.Abp.Identity;
using Volo.Abp.Modularity;

namespace Aohost.Blog.Application
{
    [DependsOn(typeof(AbpIdentityApplicationModule))]
    public class BlogApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            //Configure<AbpAutoMapperOptions>(options =>
            //{
            //    options.AddMaps<BlogApplicationModule>();
            //});
        }
    }
}
