using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Aohost.Blog.Application.Blog;
using Aohost.Blog.Application.Contracts;
using Aohost.Blog.Application.Contracts.Blog.Category;
using Aohost.Blog.Application.Contracts.Blog.FriendLink;
using Aohost.Blog.Application.Contracts.Blog.Post;
using Aohost.Blog.Application.Contracts.Blog.Tag;
using Aohost.Blog.ToolKits.Base;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;
using static Aohost.Blog.Domain.Shared.BlogConsts;

namespace Aohost.Blog.HttpApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = Grouping.GroupName_v1)]
    public partial class BlogController : AbpController
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        #region Posts

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
        /// 分页查询文章列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("posts")]
        [Route("post/query")]
        public async Task<ServiceResult<PagedList<QueryPostDto>>> QueryPostsAsync([FromQuery] PagingInput input)
        {
            return await _blogService.QueryPostsAsync(input);
        }

        /// <summary>
        /// 根绝类别获取文章详情
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("posts/category")]
        [Route("post/query_by_category")]
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
        [Route("post/query_by_tag")]
        public async Task<ServiceResult<IEnumerable<QueryPostDto>>> GetPostByTagAsync([Required] string name)
        {
            return await _blogService.QueryPostsByTagAsync(name);
        }

        #endregion

        #region Category
        
        /// <summary>
        /// 获取分类名称
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("category")]
        public async Task<ServiceResult<string>> GetCategoryAsync([Required] string name)
        {
            return await _blogService.GetCategoryAsync(name);
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

        #endregion

        #region Tag
        /// <summary>
        /// 获取标签名称
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("tag")]
        public async Task<ServiceResult<string>> GetTagAsync(string name)
        {
            return await _blogService.GetTagAsync(name);
        }

        /// <summary>
        /// 获取标签列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("tags")]
        public async Task<ServiceResult<IEnumerable<QueryTagDto>>> QueryTagsAsync()
        {
            return await _blogService.QueryTagsAsync();
        }

        #endregion

        #region FriendLink

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

        #endregion
    }
}