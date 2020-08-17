using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aohost.Blog.Application.Contracts.Wallpaper;
using Aohost.Blog.Application.Contracts.Wallpaper.Params;
using Aohost.Blog.Caching.Base;
using Aohost.Blog.Domain.Shared;
using Aohost.Blog.ToolKits;
using Aohost.Blog.ToolKits.Extensions;

namespace Aohost.Blog.Caching.Wallpaper.Impl
{
    public class WallpaperCacheService: CachingServiceBase, IWallpaperCacheService
    {
        private const string KYE_GetWallpaperTypes = "Wallpaper:GetWallpaperTypes";
        private const string KEY_QueryWallpapers = "Wallpaper:QueryWallpapers-{0}-{1}-{2}-{3}";

        public async Task<ServiceResult<IEnumerable<EnumResponse>>> GetWallpaperTypesAsync(
            Func<Task<ServiceResult<IEnumerable<EnumResponse>>>> factory)
        {
            return await Cache.GetOrAddAsync(KYE_GetWallpaperTypes, factory, CacheStrategy.NEVER);
        }

        /// <summary>
        /// 分页查询壁纸
        /// </summary>
        /// <param name="input"></param>
        /// <param name="factory"></param>
        /// <returns></returns>
        public async Task<ServiceResult<PagedList<WallpaperDto>>> QueryWallpapersAsync(QueryWallpapersInput input,
            Func<Task<ServiceResult<PagedList<WallpaperDto>>>> factory)
        {
            return await Cache.GetOrAddAsync(
                KEY_QueryWallpapers.FormatWith(input.Page, input.Limit, input.Type, input.KeysWords), factory,
                CacheStrategy.HALF_HOURS);
        }

    }
}