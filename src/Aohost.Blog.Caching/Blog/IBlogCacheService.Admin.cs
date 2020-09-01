using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Aohost.Blog.Application.Contracts;
using Aohost.Blog.Application.Contracts.Blog.Category;
using Aohost.Blog.Application.Contracts.Blog.Post;
using Aohost.Blog.ToolKits;

namespace Aohost.BlogApplication.Caching.Blog
{
    public partial interface IBlogCacheService
    {
        Task<ServiceResult<PagedList<QueryPostForAdminDto>>> QueryPostsForAdminAsync(PagingInput input,
            Func<Task<ServiceResult<PagedList<QueryPostForAdminDto>>>> factory);

        /// <summary>
        /// 管理后台查询分类列表
        /// </summary>
        /// <returns></returns>
        Task<ServiceResult<IEnumerable<QueryCategoryForAdminDto>>> QueryCategoriesForAdmin(Func<Task<ServiceResult<IEnumerable<QueryCategoryForAdminDto>>>> factory);
    }
}
