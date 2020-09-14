using System.Collections.Generic;
using System.Threading.Tasks;
using Aohost.Blog.Application.Contracts.HotNews;
using Aohost.Blog.Application.Contracts.HotNews.Params;
using Aohost.Blog.ToolKits.Base;

namespace Aohost.Blog.Application.HotNews
{
    public interface IHotNewService
    {
        /// <summary>
        /// 批量插入每日热点数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ServiceResult<string>> BulkInsertAsync(BulkInsertHotNewsInput input);

        /// <summary>
        /// 根据来源获取每日热点
        /// </summary>
        /// <param name="sourceId"></param>
        /// <returns></returns>
        Task<ServiceResult<IEnumerable<HotNewsDto>>> QueryHotNewsAsync(int sourceId);

        /// <summary>
        /// 获取每日热点来源列表
        /// </summary>
        /// <returns></returns>
        Task<ServiceResult<IEnumerable<EnumResponse>>> GetHotNewsSourceAsync();
    }
}