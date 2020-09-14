using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aohost.Blog.Application.Contracts.Blog.Category;
using Aohost.Blog.Domain.Shared;
using Aohost.Blog.ToolKits.Base;
using Aohost.Blog.ToolKits.Extensions;

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

        /// <summary>
        /// 获取分类名称
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<ServiceResult<string>> GetCategoryAsync(string name)
        {
            return await _blogCacheService.GetCategoryAsync(name, async () =>
            {
                var result = new ServiceResult<string>();

                var category = await _categoryRepository.FindAsync(x => x.DisplayName.Equals(name));
                if (null == category)
                {
                    result.IsFailed(ResponseText.WAHT_NOT_EXIST.FormatWith("分类", name));
                    return result;
                }

                result.IsSuccess(category.CategoryName);
                return result;
            });
        }
    }
}
