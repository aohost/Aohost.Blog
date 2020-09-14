using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Aohost.Blog.Application.Contracts.Blog.Category;
using Aohost.Blog.Caching;
using Aohost.Blog.Domain.Shared;
using Aohost.Blog.ToolKits.Base;
using Aohost.Blog.ToolKits.Extensions;

namespace Aohost.BlogApplication.Caching.Blog.Impl
{
    public partial class BlogCacheService
    {
        private const string CategoryPrefix = CachePrefix.BlogCategory;

        private const string KEY_GetCategory = CategoryPrefix + ":GetCategory-{0}";
        private const string KEY_QueryCategories = CategoryPrefix + ":QueryCategories";

        /// <summary>
        /// 获取分类名称
        /// </summary>
        /// <param name="name"></param>
        /// <param name="factory"></param>
        /// <returns></returns>
        public async Task<ServiceResult<string>> GetCategoryAsync(string name, Func<Task<ServiceResult<string>>> factory)
        {
            return await Cache.GetOrAddAsync(KEY_GetCategory.FormatWith(name), factory, CacheStrategy.ONE_DAY);
        }

        /// <summary>
        /// 查询分类列表
        /// </summary>
        /// <param name="factory"></param>
        /// <returns></returns>
        public async Task<ServiceResult<IEnumerable<QueryCategoryDto>>> QueryCategoriesAsync(Func<Task<ServiceResult<IEnumerable<QueryCategoryDto>>>> factory)
        {
            return await Cache.GetOrAddAsync(KEY_QueryCategories, factory, CacheStrategy.HALF_DAY);
        }
    }
}
