using Volo.Abp.Modularity;

namespace Aohost.Blog
{
    [DependsOn(
        typeof(BlogApplicationModule),
        typeof(BlogDomainTestModule)
        )]
    public class BlogApplicationTestModule : AbpModule
    {

    }
}