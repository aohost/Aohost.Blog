using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aohost.Blog.Application.Contracts.Wallpaper;
using Aohost.Blog.Application.Contracts.Wallpaper.Params;
using Aohost.Blog.Caching.Base;
using Aohost.Blog.Caching.Wallpaper;
using Aohost.Blog.Domain.Shared;
using Aohost.Blog.Domain.Shared.Enum;
using Aohost.Blog.Domain.Wallpaper.Repositories;
using Aohost.Blog.ToolKits;
using Aohost.Blog.ToolKits.Extensions;

namespace Aohost.Blog.Application.Wallpaper.Impl
{
    public class WallpaperService:ServiceBase, IWallpaperService
    {
        private readonly IWallpaperRepository _wallpaperRepository;
        private readonly IWallpaperCacheService _wallpaperCacheService;

        public WallpaperService(IWallpaperRepository wallpaperRepository, IWallpaperCacheService wallpaperCacheService)
        {
            _wallpaperRepository = wallpaperRepository;
            _wallpaperCacheService = wallpaperCacheService;
        }

        public async Task<ServiceResult<IEnumerable<EnumResponse>>> GetWallpaperTypesAsync()
        {
            return await _wallpaperCacheService.GetWallpaperTypesAsync(async () =>
            {
                var result = new ServiceResult<IEnumerable<EnumResponse>>();

                var types = typeof(WallpaperEnum).TryToList();
                result.IsSuccess(types);

                return result;
            });
        }

        public async Task<ServiceResult<PagedList<WallpaperDto>>> QueryWallpapersAsync(QueryWallpapersInput input)
        {
            return await _wallpaperCacheService.QueryWallpapersAsync(input, async () =>
            {
                var result = new ServiceResult<PagedList<WallpaperDto>>();

                var query = _wallpaperRepository.WhereIf(input.Type > 0, x => x.Type == input.Type)
                    .WhereIf(!string.IsNullOrEmpty(input.KeysWords), x => x.Title.Contains(input.KeysWords))
                    .OrderByDescending(x => x.CreateTime);
                var count = query.Count();
                var wallpapers = query.PageByIndex(input.Page, input.Limit);

                var list = ObjectMapper.Map<IEnumerable<Domain.Wallpaper.Wallpaper>, List<WallpaperDto>>(wallpapers);

                result.IsSuccess(new PagedList<WallpaperDto>(count, list));
                return await Task.FromResult(result);
            });
        }

        /// <summary>
        /// 批量插入壁纸
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ServiceResult<string>> BulkInsertWallpaperAsync(BulkInsertWallpaperInput input)
        {
            var result = new ServiceResult<string>();

            if (!input.Wallpapers.Any())
            {
                result.IsFailed(ResponseText.DATA_IS_NONE);
                return result;
            }

            var urls = _wallpaperRepository.Where(x => x.Type == (int) input.Type).Select(x => x.Url).ToList();
            var wallpapers = ObjectMapper
                .Map<IEnumerable<WallpaperDto>, IEnumerable<Domain.Wallpaper.Wallpaper>>(input.Wallpapers)
                .Where(x => !urls.Contains(x.Url));

            foreach (var wallpaper in wallpapers)
            {
                wallpaper.Type = (int) input.Type;
                wallpaper.CreateTime = wallpaper.Url.Split("/").Last().Split("_").First().TryToDatetime();
            }

            await _wallpaperRepository.BulkInsertAsync(wallpapers);

            result.IsSuccess(ResponseText.INSERT_SUCCESS);
            return result;
        }
    }
}