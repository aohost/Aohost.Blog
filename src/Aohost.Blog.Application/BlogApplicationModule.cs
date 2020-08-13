using Volo.Abp.Identity;
using Volo.Abp.Modularity;

namespace Aohost.Blog.Application
{
    [DependsOn(typeof(AbpIdentityApplicationModule))]
    public class BlogApplicationModule:AbpModule
    {
        
    }
}