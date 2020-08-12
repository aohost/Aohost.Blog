using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace Aohost.Blog.HttpApi.Client.ConsoleTestApp
{
    [DependsOn(
        typeof(BlogHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class BlogConsoleApiClientModule : AbpModule
    {
        
    }
}
