using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aohost.Blog.Application.Contracts.Wallpaper;
using Aohost.Blog.Application.Contracts.Wallpaper.Params;
using Aohost.Blog.ToolKits.Base;

namespace Aohost.Blog.Caching.Wallpaper
{
    public interface IWallpaperCacheService
    {
        /// <summary>
        /// 分页查询壁纸
        /// </summary>
        /// <param name="input"></param>
        /// <param name="factory"></param>
        /// <returns></returns>
        Task<ServiceResult<PagedList<WallpaperDto>>> QueryWallpapersAsync(QueryWallpapersInput input,
            Func<Task<ServiceResult<PagedList<WallpaperDto>>>> factory);

        /// <summary>
        /// 获取所有壁纸类型
        /// </summary>
        /// <returns></returns>
        Task<ServiceResult<IEnumerable<EnumResponse>>> GetWallpaperTypesAsync(
            Func<Task<ServiceResult<IEnumerable<EnumResponse>>>> factory);
    }
}