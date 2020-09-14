using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Aohost.Blog.Application.Contracts.Blog.Tag;
using Aohost.Blog.Caching;
using Aohost.Blog.Domain.Shared;
using Aohost.Blog.ToolKits.Base;
using Aohost.Blog.ToolKits.Extensions;

namespace Aohost.BlogApplication.Caching.Blog.Impl
{
    public partial class BlogCacheService
    {
        private const string TagPrefix = CachePrefix.BlogTag;

        private const string KEY_GetTag = TagPrefix + ":GetTag-{0}";
        private const string KEY_QueryTags = TagPrefix + ":QueryTags";
        private const string KEY_QueryTagsForAdmin = TagPrefix + ":QueryTagsForAdmin";

        /// <summary>
        /// 获取标签列表
        /// </summary>
        /// <param name="factory"></param>
        /// <returns></returns>
        public async Task<ServiceResult<IEnumerable<QueryTagDto>>> QueryTagsAsync(Func<Task<ServiceResult<IEnumerable<QueryTagDto>>>> factory)
        {
            return await Cache.GetOrAddAsync(KEY_QueryTags, factory, CacheStrategy.ONE_DAY);
        }

        /// <summary>
        /// 获取标签名称
        /// </summary>
        /// <param name="name"></param>
        /// <param name="factory"></param>
        /// <returns></returns>
        public async Task<ServiceResult<string>> GetTagAsync(string name, Func<Task<ServiceResult<string>>> factory)
        {
            return await Cache.GetOrAddAsync(KEY_GetTag.FormatWith(name), factory, CacheStrategy.HALF_DAY);
        }
    }
}
