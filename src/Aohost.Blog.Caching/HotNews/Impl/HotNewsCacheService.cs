using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aohost.Blog.Application.Contracts.HotNews;
using Aohost.Blog.Domain.Shared;
using Aohost.Blog.ToolKits.Base;
using Aohost.Blog.ToolKits.Extensions;

namespace Aohost.Blog.Caching.HotNews.Impl
{
    public class HotNewsCacheService:CachingServiceBase, IHotNewsCacheService
    {
        private const string KEY_GetHotNewsSource = "HotNews:GetHotNewsSource";
        private const string KEY_QueryHotNews = "HotNews:QueryHotNews-{0}";

        /// <summary>
        /// 根据来源获取每日热点
        /// </summary>
        /// <param name="sourceId"></param>
        /// <param name="factory"></param>
        /// <returns></returns>
        public async Task<ServiceResult<IEnumerable<HotNewsDto>>> QueryHotNewsAsync(int sourceId, Func<Task<ServiceResult<IEnumerable<HotNewsDto>>>> factory)
        {
            return await Cache.GetOrAddAsync(KEY_QueryHotNews.FormatWith(sourceId), factory,
                CacheStrategy.FIVE_MINUTES);
        }

        /// <summary>
        /// 获取每日热点来源列表
        /// </summary>
        /// <param name="factory"></param>
        /// <returns></returns>
        public async Task<ServiceResult<IEnumerable<EnumResponse>>> GetHotNewsSourceAsync(Func<Task<ServiceResult<IEnumerable<EnumResponse>>>> factory)
        {
            return await Cache.GetOrAddAsync(KEY_GetHotNewsSource, factory, CacheStrategy.NEVER);
        }
    }
}