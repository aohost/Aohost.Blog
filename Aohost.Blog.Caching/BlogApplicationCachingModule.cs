using System;
using Aohost.Blog.Domain;
using Aohost.Blog.Domain.Configuration;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Redis;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Caching;
using Volo.Abp.Modularity;

namespace Aohost.Blog.Caching
{
    [DependsOn(typeof(AbpCachingModule), typeof(BlogDomainModule))]
    public class BlogApplicationCachingModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var csRedis = new CSRedis.CSRedisClient(AppSettings.Caching.RedisConnectionString);
            RedisHelper.Initialization(csRedis);
            context.Services.AddSingleton<IDistributedCache>(new CSRedisCache(RedisHelper.Instance));
        }
    }
}
