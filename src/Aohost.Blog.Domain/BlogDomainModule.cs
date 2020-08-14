using Aohost.Blog.Domain.Shared;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;

namespace Aohost.Blog.Domain
{
    [DependsOn(typeof(AbpIdentityDomainModule), typeof(BlogDomainSharedModule))]
    public class BlogDomainModule:AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
        }
    }
}