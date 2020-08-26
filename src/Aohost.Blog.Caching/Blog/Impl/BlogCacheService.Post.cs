using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Aohost.Blog.Application.Contracts;
using Aohost.Blog.Application.Contracts.Blog;
using Aohost.Blog.Caching;
using Aohost.Blog.Domain.Shared;
using Aohost.Blog.ToolKits;
using Aohost.Blog.ToolKits.Extensions;

namespace Aohost.BlogApplication.Caching.Blog.Impl
{
    public partial class BlogCacheService
    {
        private const string PostPrefix = CachePrefix.BlogPost;

        private const string KEY_GetPostDetail = PostPrefix + ":GetPostDetail-{0}";
        private const string KEY_QueryPosts = PostPrefix + ":QueryPosts-{0}-{1}";
        private const string KEY_QueryPostsByCategory = PostPrefix + ":QueryPostsByCategory-{0}";
        private const string KEY_QueryPostsByTag = PostPrefix + ":QueryPostsByTag-{0}";

        /// <summary>
        /// 根据URL获取文章详情
        /// </summary>
        /// <param name="url"></param>
        /// <param name="factory"></param>
        /// <returns></returns>
        public async Task<ServiceResult<PostDetailDto>> GetPostDetailAsync(string url, Func<Task<ServiceResult<PostDetailDto>>> factory)
        {
            return await Cache.GetOrAddAsync(KEY_GetPostDetail.FormatWith(url), factory, CacheStrategy.ONE_DAY);
        }

        /// <summary>
        /// 分页查询文章列表
        /// </summary>
        /// <param name="input"></param>
        /// <param name="factory"></param>
        /// <returns></returns>
        public async Task<ServiceResult<PagedList<QueryPostDto>>> QueryPostsAsync(PagingInput input, Func<Task<ServiceResult<PagedList<QueryPostDto>>>> factory)
        {
            return await Cache.GetOrAddAsync(KEY_QueryPosts.FormatWith(input.Page, input.Limit), factory,
                CacheStrategy.ONE_DAY);
        }

        /// <summary>
        /// 通过分类名称查询文章列表
        /// </summary>
        /// <param name="name"></param>
        /// <param name="factory"></param>
        /// <returns></returns>
        public async Task<ServiceResult<IEnumerable<QueryPostDto>>> QueryPostsByCategoryAsync(string name, Func<Task<ServiceResult<IEnumerable<QueryPostDto>>>> factory)
        {
            return await Cache.GetOrAddAsync(KEY_QueryPostsByCategory.FormatWith(name), factory, CacheStrategy.ONE_DAY);
        }

        /// <summary>
        /// 通过标签名称查询文章列表
        /// </summary>
        /// <param name="name"></param>
        /// <param name="factory"></param>
        /// <returns></returns>
        public async Task<ServiceResult<IEnumerable<QueryPostDto>>> QueryPostsByTagAsync(string name, Func<Task<ServiceResult<IEnumerable<QueryPostDto>>>> factory)
        {
            return await Cache.GetOrAddAsync(KEY_QueryPostsByTag.FormatWith(name), factory, CacheStrategy.ONE_DAY);
        }
    }
}
