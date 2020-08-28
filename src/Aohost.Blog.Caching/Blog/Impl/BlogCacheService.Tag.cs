using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Aohost.Blog.Application.Contracts.Blog.Tag;
using Aohost.Blog.Caching;
using Aohost.Blog.Domain.Shared;
using Aohost.Blog.ToolKits;

namespace Aohost.BlogApplication.Caching.Blog.Impl
{
    public partial class BlogCacheService
    {
        private const string TagPrefix = CachePrefix.BlogTag;
        private const string KEY_QueryTags = TagPrefix + ":QueryTags";

        public async Task<ServiceResult<IEnumerable<QueryTagDto>>> QueryTagsAsync(Func<Task<ServiceResult<IEnumerable<QueryTagDto>>>> factory)
        {
            return await Cache.GetOrAddAsync(KEY_QueryTags, factory, CacheStrategy.ONE_DAY);
        }
    }
}
