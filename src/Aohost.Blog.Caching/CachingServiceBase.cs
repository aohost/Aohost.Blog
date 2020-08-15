using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Volo.Abp.DependencyInjection;

namespace Aohost.Blog.Caching
{
    public class CachingServiceBase : ITransientDependency 
    {
        public IDistributedCache Cache { get; set; }
    }
}