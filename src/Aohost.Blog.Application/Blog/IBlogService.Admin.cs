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

namespace Aohost.Blog.Application.Blog
{
    public partial interface IBlogService
    {
        /// <summary>
        /// 管理后台获取文章详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ServiceResult<PostForAdminDto>> GetPostForAdminAsync(int id);

        /// <summary>
        /// 管理后台获取文章列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ServiceResult<PagedList<QueryPostForAdminDto>>> QueryPostsForAdminAsync(PagingInput input);

        /// <summary>
        /// 管理后台查询分类列表
        /// </summary>
        /// <returns></returns>
        Task<ServiceResult<IEnumerable<QueryCategoryForAdminDto>>> QueryCategoriesForAdmin();

        /// <summary>
        /// 新增文章
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ServiceResult> InsertPostAsync(EditPostInput input);

        /// <summary>
        /// 更新文章
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ServiceResult> UpdatePostAsync(int id, EditPostInput input);

        /// <summary>
        /// 删除文章
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ServiceResult> DeletePostAsync(int id);

        /// <summary>
        /// 添加分类
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ServiceResult> InsertCategoryAsync(EditCategoryInput input);

        /// <summary>
        /// 更新分类信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ServiceResult> UpdateCategoryAsync(int id, EditCategoryInput input);

        /// <summary>
        /// 删除分类
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ServiceResult> DeleteCategoryAsync(int id);

        /// <summary>
        /// 管理后台获取标签详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ServiceResult<QueryTagForAdminDto>> GetTagForAdminAsync(int id);

        /// <summary>
        /// 管理后台分页获取标签详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ServiceResult<PagedList<QueryTagForAdminDto>>> QueryTagsForAdminAsync(PagingInput input);

        /// <summary>
        /// 管理后台获取所有标签详情
        /// </summary>
        /// <returns></returns>
        Task<ServiceResult<IEnumerable<QueryTagForAdminDto>>> QueryTagsForAdminAsync();

        /// <summary>
        /// 新增标签
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ServiceResult> InsertTagAsync(EditTagDto input);

        /// <summary>
        /// 更新标签
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ServiceResult> UpdateTagAsync(int id, EditTagDto input);

        /// <summary>
        /// 删除标签
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ServiceResult> DeleteTagAsync(int id);


        #region FriendLink

        /// <summary>
        /// 获取FriendLink列表
        /// </summary>
        /// <returns></returns>
        Task<ServiceResult<IEnumerable<QueryFriendLinkForAdminDto>>> QueryFriendLinkForAdminAsync();

        /// <summary>
        /// 新增FriendLink
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ServiceResult> InsertFriendLinkAsync(EditFriendLinkDto input);

        /// <summary>
        /// 更新FriendLink
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ServiceResult> UpdateFriendLinkAsync(int id, EditFriendLinkDto input);

        /// <summary>
        /// 删除FriendLink
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ServiceResult> DeleteFriendLinkAsync(int id);

        #endregion
    }
}
