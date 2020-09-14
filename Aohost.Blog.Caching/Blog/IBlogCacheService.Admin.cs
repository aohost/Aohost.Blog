using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Aohost.Blog.Application.Contracts;
using Aohost.Blog.Application.Contracts.Blog.Category;
using Aohost.Blog.Application.Contracts.Blog.FriendLink;
using Aohost.Blog.Application.Contracts.Blog.Post;
using Aohost.Blog.Application.Contracts.Blog.Tag;
using Aohost.Blog.ToolKits.Base;

namespace Aohost.BlogApplication.Caching.Blog
{
    public partial interface IBlogCacheService
    {
        Task<ServiceResult<PostForAdminDto>> GetPostForAdminAsync(int id, Func<Task<ServiceResult<PostForAdminDto>>> factory);

        Task<ServiceResult<PagedList<QueryPostForAdminDto>>> QueryPostsForAdminAsync(PagingInput input,
            Func<Task<ServiceResult<PagedList<QueryPostForAdminDto>>>> factory);

        /// <summary>
        /// 管理后台查询分类列表
        /// </summary>
        /// <returns></returns>
        Task<ServiceResult<IEnumerable<QueryCategoryForAdminDto>>> QueryCategoriesForAdmin(Func<Task<ServiceResult<IEnumerable<QueryCategoryForAdminDto>>>> factory);


        /// <summary>
        /// 管理后台获取标签详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ServiceResult<QueryTagForAdminDto>> GetTagForAdminAsync(int id, Func<Task<ServiceResult<QueryTagForAdminDto>>> factory);

        /// <summary>
        /// 管理后台分页获取标签详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ServiceResult<PagedList<QueryTagForAdminDto>>> QueryTagsForAdminAsync(PagingInput input, Func<Task<ServiceResult<PagedList<QueryTagForAdminDto>>>> factory);

        /// <summary>
        /// 管理后台获取所有标签详情
        /// </summary>
        /// <returns></returns>
        Task<ServiceResult<IEnumerable<QueryTagForAdminDto>>> QueryTagsForAdminAsync(Func<Task<ServiceResult<IEnumerable<QueryTagForAdminDto>>>> factory);

        /// <summary>
        /// 管理后台获取FriendLink列表
        /// </summary>
        /// <param name="factory"></param>
        /// <returns></returns>
        Task<ServiceResult<IEnumerable<QueryFriendLinkForAdminDto>>> QueryFriendLinkForAdminAsync(
            Func<Task<ServiceResult<IEnumerable<QueryFriendLinkForAdminDto>>>> factory);
    }
}
