using Aohost.Blog.Caching;

namespace Aohost.BlogApplication.Caching.Blog.Impl
{
    public partial class BlogCacheService:CachingServiceBase, IBlogCacheService, ICacheRemoveService
    {
    }
}