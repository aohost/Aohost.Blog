using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Aohost.Blog.Application.Contracts;
using Aohost.Blog.Application.Contracts.Blog.Category;
using Aohost.Blog.Application.Contracts.Blog.Post;
using Aohost.Blog.Caching;
using Aohost.Blog.Domain.Shared;
using Aohost.Blog.ToolKits;
using Aohost.Blog.ToolKits.Extensions;

namespace Aohost.BlogApplication.Caching.Blog.Impl
{
    public partial class BlogCacheService
    {
        private const string KEY_AdminQueryPosts = "Admin:QueryTags-{0}-{1}";
        private const string KEY_AdminQueryCategory = "Admin:QueryCategories";

        public async Task<ServiceResult<PagedList<QueryPostForAdminDto>>> QueryPostsForAdminAsync(PagingInput input, Func<Task<ServiceResult<PagedList<QueryPostForAdminDto>>>> factory)
        {
            return await Cache.GetOrAddAsync(KEY_AdminQueryPosts.FormatWith(input.Page, input.Limit), factory,
                CacheStrategy.TWENTY_MINUTES);
        }

        /// <summary>
        /// 管理后台获取分类列表信息
        /// </summary>
        /// <param name="factory"></param>
        /// <returns></returns>
        public async Task<ServiceResult<IEnumerable<QueryCategoryForAdminDto>>> QueryCategoriesForAdmin(Func<Task<ServiceResult<IEnumerable<QueryCategoryForAdminDto>>>> factory)
        {
            return await Cache.GetOrAddAsync(KEY_AdminQueryCategory, factory, CacheStrategy.HALF_DAY);
        }
    }
}
