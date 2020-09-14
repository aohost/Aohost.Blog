using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aohost.Blog.Application.Contracts.HotNews;
using Aohost.Blog.Application.Contracts.HotNews.Params;
using Aohost.Blog.Caching.HotNews;
using Aohost.Blog.Domain.HotNews.Repository;
using Aohost.Blog.Domain.Shared;
using Aohost.Blog.Domain.Shared.Enum;
using Aohost.Blog.ToolKits.Base;
using Aohost.Blog.ToolKits.Extensions;

namespace Aohost.Blog.Application.HotNews.Impl
{
    public class HotNewService:ServiceBase, IHotNewService
    {
        private readonly IHotNewsRepository _hotNewsRepository;
        private readonly IHotNewsCacheService _hotNewsCacheService;

        public HotNewService(IHotNewsRepository hotNewsRepository, IHotNewsCacheService hotNewsCacheService)
        {
            _hotNewsRepository = hotNewsRepository;
            _hotNewsCacheService = hotNewsCacheService;
        }

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ServiceResult<string>> BulkInsertAsync(BulkInsertHotNewsInput input)
        {
            var result = new ServiceResult<string>();

            if (!input.HotNews.Any())
            {
                result.IsFailed(ResponseText.DATA_IS_NONE);
                return result;
            }

            var hotNews = ObjectMapper.Map<IEnumerable<HotNewsDto>, IEnumerable<Domain.HotNews.HotNews>>(input.HotNews);
            foreach (var item in hotNews)
            {
                item.SourceId = (int) input.Source;
                item.CreateTime = DateTime.Now;
            }

            await _hotNewsRepository.DeleteAsync(x => x.SourceId == (int) input.Source);
            await _hotNewsRepository.BulkInsertAsync(hotNews);

            result.IsSuccess(ResponseText.INSERT_SUCCESS);
            return result;
        }

        /// <summary>
        /// 根据来源获取每日热点
        /// </summary>
        /// <param name="sourceId"></param>
        /// <returns></returns>
        public async Task<ServiceResult<IEnumerable<HotNewsDto>>> QueryHotNewsAsync(int sourceId)
        {
            return await _hotNewsCacheService.QueryHotNewsAsync(sourceId, async () =>
            {
                var result = new ServiceResult<IEnumerable<HotNewsDto>>();

                var hotNews = _hotNewsRepository.Where(x => x.SourceId == sourceId).ToList();
                var list = ObjectMapper.Map<IEnumerable<Domain.HotNews.HotNews>, IEnumerable<HotNewsDto>>(hotNews);

                result.IsSuccess(list);
                return await Task.FromResult(result);
            });
        }

        /// <summary>
        /// 获取每日热点来源列表
        /// </summary>
        /// <returns></returns>
        public async Task<ServiceResult<IEnumerable<EnumResponse>>> GetHotNewsSourceAsync()
        {
            return await _hotNewsCacheService.GetHotNewsSourceAsync(async () =>
            {
                var result = new ServiceResult<IEnumerable<EnumResponse>>();

                var types = typeof(HotNewsEnum).TryToList();
                result.IsSuccess(types);

                return await Task.FromResult(result);
            });
        }
    }
}