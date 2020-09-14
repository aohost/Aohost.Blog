using Aohost.Blog.Domain.Shared.Enum;

namespace Aohost.Blog.Application.Contracts.Wallpaper.Params
{
    public class QueryWallpapersInput:PagingInput
    {
        /// <summary>
        /// 类型
        /// <see cref="WallpaperEnum"/>
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 关键字
        /// </summary>
        public string KeysWords { get; set; }
    }
}