using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using Aohost.Blog.Application.Contracts;
using Aohost.Blog.Application.Contracts.Blog.Category;
using Aohost.Blog.Application.Contracts.Blog.Post;
using Aohost.Blog.Domain.Shared;
using Aohost.Blog.ToolKits;
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

        public async Task<ServiceResult> 
    }
}
