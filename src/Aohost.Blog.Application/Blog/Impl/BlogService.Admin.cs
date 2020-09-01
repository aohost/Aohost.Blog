using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aohost.Blog.Application.Contracts;
using Aohost.Blog.Application.Contracts.Blog.Category;
using Aohost.Blog.Application.Contracts.Blog.Post;
using Aohost.Blog.Domain.Blog;
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

        public Task<ServiceResult> InsertCategoryAsync(EditCategoryInput input)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult> UpdateCategoryAsync(int id, EditCategoryInput input)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult> DeleteCategoryAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
