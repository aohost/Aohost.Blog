using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Aohost.Blog.Application.Contracts.Blog.Category;
using Aohost.Blog.ToolKits.Base;

namespace Aohost.Blog.Application.Blog
{
    public partial interface IBlogService
    {
        /// <summary>
        /// 查询类别列表
        /// </summary>
        /// <returns></returns>
        Task<ServiceResult<IEnumerable<QueryCategoryDto>>> QueryCategoriesAsync();
    }
}
