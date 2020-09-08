using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Aohost.Blog.Application.Contracts.Blog.Category;
using Aohost.Blog.Caching;
using Aohost.Blog.Domain.Shared;
using Aohost.Blog.ToolKits.Base;

namespace Aohost.BlogApplication.Caching.Blog.Impl
{
    public partial class BlogCacheService
    {
        private const string CategoryPrefix = CachePrefix.BlogCategory;

        private const string KEY_GetCategory = CategoryPrefix + ":GetCategory-{0}";
        private const string KEY_QueryCategories = CategoryPrefix + ":QueryCategories";


        public async Task<ServiceResult<IEnumerable<QueryCategoryDto>>> QueryCategoriesAsync(Func<Task<ServiceResult<IEnumerable<QueryCategoryDto>>>> factory)
        {
            return await Cache.GetOrAddAsync(KEY_QueryCategories, factory, CacheStrategy.HALF_DAY);
        }
    }
}
