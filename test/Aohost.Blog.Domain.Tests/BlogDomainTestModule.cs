using Aohost.Blog.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Aohost.Blog
{
    [DependsOn(
        typeof(BlogEntityFrameworkCoreTestModule)
        )]
    public class BlogDomainTestModule : AbpModule
    {

    }
}