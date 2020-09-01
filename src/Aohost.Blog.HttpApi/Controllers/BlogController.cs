using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Aohost.Blog.Application.Blog;
using Aohost.Blog.Application.Contracts;
using Aohost.Blog.Application.Contracts.Blog;
using Aohost.Blog.Application.Contracts.Blog.Category;
using Aohost.Blog.Application.Contracts.Blog.FriendLink;
using Aohost.Blog.Application.Contracts.Blog.Post;
using Aohost.Blog.Domain.Shared;
using Aohost.Blog.ToolKits;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace Aohost.Blog.HttpApi.Controllers
{
    /// <summary>
    /// 博客
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = Grouping.GroupName_v1)]
    public partial class BlogController:AbpController
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        /// <summary>
        /// 分页查询文章列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("posts")]
        public async Task<ServiceResult<PagedList<QueryPostDto>>> QueryPostsAsync([FromQuery]PagingInput input)
        {
            return await _blogService.QueryPostsAsync(input);
        }

        /// <summary>
        /// 根据url获取文章详情
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("post")]
        public async Task<ServiceResult<PostDetailDto>> GetPostDetailAsync([Required] string url)
        {
            return await _blogService.GetPostDetailAsync(url);
        }

        /// <summary>
        /// 根绝类别获取文章详情
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("posts/category")]
        public async Task<ServiceResult<IEnumerable<QueryPostDto>>> GetPostByCategoryAsync([Required] string name)
        {
            return await _blogService.QueryPostsByCategoryAsync(name);
        }

        /// <summary>
        /// 根据标签获取文章详情
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("posts/tag")]
        public async Task<ServiceResult<IEnumerable<QueryPostDto>>> GetPostByTagAsync([Required] string name)
        {
            return await _blogService.QueryPostsByTagAsync(name);
        }

        /// <summary>
        /// 获取类别列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("categories")]
        public async Task<ServiceResult<IEnumerable<QueryCategoryDto>>> QueryCategoriesAsync()
        {
            return await _blogService.QueryCategoriesAsync();
        }

        /// <summary>
        /// 获取FriendLink
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("friendlinks")]
        public async Task<ServiceResult<IEnumerable<FriendLinkDto>>> QueryFriendLinksAsync()
        {
            return await _blogService.QueryFriendLinksAsync();
        }
    }
}