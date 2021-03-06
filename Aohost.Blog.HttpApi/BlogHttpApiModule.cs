﻿using Aohost.Blog.Application;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;

namespace Aohost.Blog
{
    [DependsOn(
        typeof(AbpIdentityHttpApiModule),
        typeof(BlogApplicationModule)
        )]
    public class BlogHttpApiModule : AbpModule
    {
        
    }
}
