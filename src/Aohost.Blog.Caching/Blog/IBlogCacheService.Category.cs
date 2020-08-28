using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Aohost.Blog.Application.Contracts.Blog.Category;
using Aohost.Blog.ToolKits;

namespace Aohost.BlogApplication.Caching.Blog
{
    public partial interface IBlogCacheService
    {
        Task<ServiceResult<IEnumerable<QueryCategoryDto>>> QueryCategoriesAsync(Func<Task<ServiceResult<IEnumerable<QueryCategoryDto>>>> factory);
    }
}
