using System;
using Aohost.Blog.Domain;
using Volo.Abp.Caching;
using Volo.Abp.Modularity;

namespace Aohost.Blog.Caching
{
    [DependsOn(typeof(AbpCachingModule), typeof(BlogDomainModule))]
    public class BlogApplicationCachingModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            base.ConfigureServices(context);
        }
    }
}
