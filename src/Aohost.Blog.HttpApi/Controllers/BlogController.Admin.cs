using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using Aohost.Blog.Application.Contracts;
using Aohost.Blog.Application.Contracts.Blog.Category;
using Aohost.Blog.Application.Contracts.Blog.FriendLink;
using Aohost.Blog.Application.Contracts.Blog.Post;
using Aohost.Blog.Application.Contracts.Blog.Tag;
using Aohost.Blog.Domain.Blog;
using Aohost.Blog.Domain.Shared;
using Aohost.Blog.ToolKits.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aohost.Blog.HttpApi.Controllers
{
    public partial class BlogController
    {
        /// <summary>
        /// 获取文章详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("admin/post")]
        [ApiExplorerSettings(GroupName = Grouping.GroupName_v2)]
        public async Task<ServiceResult<PostForAdminDto>> GetPostForAdminAsync([Required] int id)
        {
            return await _blogService.GetPostForAdminAsync(id);
        }

        /// <summary>
        /// 管理后台获取文章列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("admin/posts")]
        [Authorize]
        [ApiExplorerSettings(GroupName = Grouping.GroupName_v2)]
        public async Task<ServiceResult<PagedList<QueryPostForAdminDto>>> QueryPostsForAdminAsync(PagingInput input)
        {
            return await _blogService.QueryPostsForAdminAsync(input);
        }

        /// <summary>
        /// 新增文章
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("admin/post")]
        [Authorize]
        [ApiExplorerSettings(GroupName = Grouping.GroupName_v2)]
        public async Task<ServiceResult> InsertPostAsync(EditPostInput input)
        {
            return await _blogService.InsertPostAsync(input);
        }

        /// <summary>
        /// 更新文章
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [Route("post")]
        [HttpPost]
        [Authorize]
        [ApiExplorerSettings(GroupName = Grouping.GroupName_v2)]
        public async Task<ServiceResult> UpdatePostAsync(int id, EditPostInput input)
        {
            return await _blogService.UpdatePostAsync(id, input);
        }

        /// <summary>
        /// 删除文章
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize]
        [Route("post")]
        [ApiExplorerSettings(GroupName = Grouping.GroupName_v2)]
        public async Task<ServiceResult> DeletePostAsync(int id)
        {
            return await _blogService.DeletePostAsync(id);
        }

        #region Category

        /// <summary>
        /// 管理后台获取类别
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/admin/categories")]
        [Authorize]
        [ApiExplorerSettings(GroupName = Grouping.GroupName_v2)]
        public async Task<ServiceResult<IEnumerable<QueryCategoryForAdminDto>>> QueryCategories()
        {
            return await _blogService.QueryCategoriesForAdmin();
        }

        #endregion

        #region Tag

        /// <summary>
        /// 获取标签详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("admin/tag")]
        [ApiExplorerSettings(GroupName = Grouping.GroupName_v2)]
        [Authorize]
        public async Task<ServiceResult<QueryTagForAdminDto>> GetTagForAdminAsync([Required]int id)
        {
            return await _blogService.GetTagForAdminAsync(id);
        }

        /// <summary>
        /// 新增标签
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("tag")]
        [ApiExplorerSettings(GroupName = Grouping.GroupName_v2)]
        public async Task<ServiceResult> InsertTagAsync(EditTagDto input)
        {
            return await _blogService.InsertTagAsync(input);
        }

        /// <summary>
        /// 更新标签
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("tag")]
        [ApiExplorerSettings(GroupName = Grouping.GroupName_v2)]
        public async Task<ServiceResult> UpgradeTagAsync(int id, EditTagDto input)
        {
            return await _blogService.UpdateTagAsync(id, input);
        }

        /// <summary>
        /// 删除标签
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("tag")]
        [Authorize]
        [ApiExplorerSettings(GroupName = Grouping.GroupName_v2)]
        public async Task<ServiceResult> DeleteTagAsync(int id)
        {
            return await _blogService.DeleteTagAsync(id);
        }
        #endregion

        #region FriendLink

        /// <summary>
        /// 管理后台获取FriendLink列表
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("admin/friendlink")]
        [ApiExplorerSettings(GroupName = Grouping.GroupName_v2)]
        public async Task<ServiceResult<IEnumerable<QueryFriendLinkForAdminDto>>> QueryFriendLinksForAdminAsync()
        {
            return await _blogService.QueryFriendLinkForAdminAsync();
        }

        /// <summary>
        /// 新增FriendLink
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("friendlink")]
        [ApiExplorerSettings(GroupName = Grouping.GroupName_v2)]
        public async Task<ServiceResult> InsertFriendLinkAsync(EditFriendLinkDto input)
        {
            return await _blogService.InsertFriendLinkAsync(input);
        }

        /// <summary>
        /// 更新FriendLink
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("friendlink")]
        [ApiExplorerSettings(GroupName = Grouping.GroupName_v2)]
        public async Task<ServiceResult> UpdateFriendLinkAsync(int id, EditFriendLinkDto input)
        {
            return await _blogService.UpdateFriendLinkAsync(id, input);
        }

        /// <summary>
        /// 删除FriendLink
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete]
        [Route("friendlink")]
        [ApiExplorerSettings(GroupName = Grouping.GroupName_v2)]
        public async Task<ServiceResult> DeleteFriendLinkAsync(int id)
        {
            return await _blogService.DeleteFriendLinkAsync(id);
        }

        #endregion
    }
}
