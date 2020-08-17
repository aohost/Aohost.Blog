using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aohost.Blog.Application.Contracts.HotNews;
using Aohost.Blog.Caching.Base;
using Aohost.Blog.ToolKits;

namespace Aohost.Blog.Caching.HotNews
{
    public interface IHotNewsCacheService
    {
        /// <summary>
        /// 根据来源获取每日热点列表
        /// </summary>
        /// <param name="sourceId"></param>
        /// <param name="factory"></param>
        /// <returns></returns>
        Task<ServiceResult<IEnumerable<HotNewsDto>>> QueryHotNewsAsync(int sourceId,Func<Task<ServiceResult<IEnumerable<HotNewsDto>>>> factory);

        /// <summary>
        /// 获取每日热点来源列表
        /// </summary>
        /// <param name="factory"></param>
        /// <returns></returns>
        Task<ServiceResult<IEnumerable<EnumResponse>>> GetHotNewsSourceAsync(Func<Task<ServiceResult<IEnumerable<EnumResponse>>>> factory);
    }
}