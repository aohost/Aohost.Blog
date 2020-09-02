using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aohost.Blog.Application.Contracts;
using Aohost.Blog.Application.Contracts.Blog.Category;
using Aohost.Blog.Application.Contracts.Blog.Post;
using Aohost.Blog.Domain.Blog;
using Aohost.Blog.Domain.Shared;
using Aohost.Blog.ToolKits;
using Aohost.Blog.ToolKits.Extensions;

namespace Aohost.Blog.Application.Blog.Impl
{
    public partial class BlogService
    {
        /// <summary>
        /// 查询文章
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ServiceResult<PagedList<QueryPostForAdminDto>>> QueryPostsForAdminAsync(PagingInput input)
        {
            return await _blogCacheService.QueryPostsForAdminAsync(input, async () =>
            {
                var result = new ServiceResult<PagedList<QueryPostForAdminDto>>();

                var count = _postRepository.GetCountAsync();
                var list = _postRepository.OrderByDescending(x => x.CreationTime).PageByIndex(input.Page, input.Limit)
                    .Select(x => new PostBriefForAdminDto
                    {
                        CreationTime = x.CreationTime.TryToDatetime(),
                        Id = x.Id,
                        Title = x.Title,
                        Url = x.Url,
                        Year = x.CreationTime.Year
                    }).GroupBy(x => x.Year)
                    .Select(x => new QueryPostForAdminDto
                    {
                        Year = x.Key,
                        Posts = x.ToList()
                    }).ToList();

                result.IsSuccess(new PagedList<QueryPostForAdminDto>(count.TryToInt(), list));
                return result;
            });
        }

        public async Task<ServiceResult> InsertPostAsync(EditPostInput input)
        {
            var result = new ServiceResult();

            var post = ObjectMapper.Map<EditPostInput, Post>(input);
            post.Url = $"{post.CreationTime.ToString("yyyy-MM-dd").Replace('-', '/')}/{post.Url}/";
         
            await _postRepository.InsertAsync(post);

            result.IsSuccess(ResponseText.INSERT_SUCCESS);
            return result;
        }

        /// <summary>
        /// 管理后台获取分类信息
        /// </summary>
        /// <returns></returns>
        public async Task<ServiceResult<IEnumerable<QueryCategoryForAdminDto>>> QueryCategoriesForAdmin()
        {
            return await _blogCacheService.QueryCategoriesForAdmin(async () =>
            {
                var result = new ServiceResult<IEnumerable<QueryCategoryForAdminDto>>();

                var posts = await _postRepository.GetListAsync();
                var categories = await _categoryRepository.GetListAsync();

                return result;
            });
        }

        /// <summary>
        /// 新增类别
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ServiceResult> InsertCategoryAsync(EditCategoryInput input)
        {
            var result = new ServiceResult();
            var category = ObjectMapper.Map<EditCategoryInput, Category>(input);
            await _categoryRepository.InsertAsync(category);
            result.IsSuccess(ResponseText.INSERT_SUCCESS);
            return result;
        }

        /// <summary>
        /// 更新类别信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ServiceResult> UpdateCategoryAsync(int id, EditCategoryInput input)
        {
            var result = new ServiceResult();
            var category = await _categoryRepository.GetAsync(id);
            category.DisplayName = input.DisplayName;
            category.CategoryName = input.CategoryName;

            await _categoryRepository.UpdateAsync(category);

            result.IsSuccess(ResponseText.UPDATE_SUCCESS);
            return result;
        }

        /// <summary>
        /// 删除类别信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ServiceResult> DeleteCategoryAsync(int id)
        {
            var result = new ServiceResult();
            await _categoryRepository.DeleteAsync(id);
            result.IsSuccess(ResponseText.DELETE_SUCCESS);
            return result;
        }
    }
}
