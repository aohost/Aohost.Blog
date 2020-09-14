using System.Collections.Generic;
using Aohost.Blog.Domain.Shared.Enum;

namespace Aohost.Blog.Application.Contracts.Wallpaper.Params
{
    public class BulkInsertWallpaperInput
    {
        /// <summary>
        /// 类型
        /// </summary>
        public WallpaperEnum Type { get; set; }

        public IEnumerable<WallpaperDto> Wallpapers { get; set; }
    }
}