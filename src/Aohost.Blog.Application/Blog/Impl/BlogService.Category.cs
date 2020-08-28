using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aohost.Blog.Application.Contracts.Blog.Category;
using Aohost.Blog.ToolKits;

namespace Aohost.Blog.Application.Blog.Impl
{
    public partial class BlogService
    {
        /// <summary>
        /// 查询类别列表
        /// </summary>
        /// <returns></returns>
        public async Task<ServiceResult<IEnumerable<QueryCategoryDto>>> QueryCategoriesAsync()
        {
            return await _blogCacheService.QueryCategoriesAsync(async () =>
            {
                var result = new ServiceResult<IEnumerable<QueryCategoryDto>>();

                var list = from categories in await _categoryRepository.GetListAsync()
                    join posts in await _postRepository.GetListAsync() on categories.Id equals posts.CategoryId
                    group categories by new
                    {
                        categories.CategoryName,
                        categories.DisplayName
                    }
                    into g
                    select new QueryCategoryDto
                    {
                        DisplayName = g.Key.DisplayName,
                        CategoryName = g.Key.CategoryName,
                        Count = g.Count()
                    };

                result.IsSuccess(list);
                return result;
            });
        }
    }
}
