using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Aohost.Blog.Application.Contracts;
using Aohost.Blog.Application.Contracts.Blog.Category;
using Aohost.Blog.Application.Contracts.Blog.FriendLink;
using Aohost.Blog.Application.Contracts.Blog.Post;
using Aohost.Blog.Application.Contracts.Blog.Tag;
using Aohost.Blog.Caching;
using Aohost.Blog.Domain.Shared;
using Aohost.Blog.ToolKits;
using Aohost.Blog.ToolKits.Extensions;

namespace Aohost.BlogApplication.Caching.Blog.Impl
{
    public partial class BlogCacheService
    {
        private const string KEY_AdminGetPost = "Admin:GetPost-{0}";
        private const string KEY_AdminQueryPosts = "Admin:QueryPosts-{0}-{1}";

        private const string KEY_AdminGetTag = "Admin:GetTag-{0}";
        private const string KEY_AdminQueryTags = "Admin:QueryTags-{0}-{1}";
        private const string KEY_AdminQueryTag = "Admin:QueryTags-{0}-{1}";
        
        private const string KEY_AdminQueryCategory = "Admin:QueryCategories";

        private const string KEY_AdminQueryFriendLink = "Admin:QueryFriendLinks";

        public async Task<ServiceResult<PostForAdminDto>> GetPostForAdminAsync(int id, Func<Task<ServiceResult<PostForAdminDto>>> factory)
        {
            return await Cache.GetOrAddAsync(KEY_AdminGetPost.FormatWith(id), factory, CacheStrategy.HALF_DAY);
        }

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

        /// <summary>
        /// 管理后台获取标签详情
        /// </summary>
        /// <param name="id"></param>
        /// <param name="factory"></param>
        /// <returns></returns>
        public async Task<ServiceResult<QueryTagForAdminDto>> GetTagForAdminAsync(int id, Func<Task<ServiceResult<QueryTagForAdminDto>>> factory)
        {
            return await Cache.GetOrAddAsync(KEY_AdminGetTag.FormatWith(id), factory, CacheStrategy.HALF_DAY);
        }

        public async Task<ServiceResult<PagedList<QueryTagForAdminDto>>> QueryTagsForAdminAsync(PagingInput input, Func<Task<ServiceResult<PagedList<QueryTagForAdminDto>>>> factory)
        {
            return await Cache.GetOrAddAsync(KEY_AdminQueryTags.FormatWith(input.Page, input.Limit), factory,
                CacheStrategy.HALF_HOURS);
        }

        public async Task<ServiceResult<IEnumerable<QueryTagForAdminDto>>> QueryTagsForAdminAsync(Func<Task<ServiceResult<IEnumerable<QueryTagForAdminDto>>>> factory)
        {
            return await Cache.GetOrAddAsync(KEY_AdminQueryTag, factory, CacheStrategy.HALF_DAY);
        }

        public async Task<ServiceResult<IEnumerable<QueryFriendLinkForAdminDto>>> QueryFriendLinkForAdminAsync(Func<Task<ServiceResult<IEnumerable<QueryFriendLinkForAdminDto>>>> factory)
        {
            return await Cache.GetOrAddAsync(KEY_AdminQueryFriendLink, factory, CacheStrategy.HALF_DAY);
        }
    }
}
