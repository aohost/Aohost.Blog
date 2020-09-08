using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Aohost.Blog.Application.Contracts.Blog.Tag;
using Aohost.Blog.ToolKits.Base;

namespace Aohost.BlogApplication.Caching.Blog
{
    public partial interface IBlogCacheService
    {
        /// <summary>
        /// 获取标签列表
        /// </summary>
        /// <param name="factory"></param>
        /// <returns></returns>
        Task<ServiceResult<IEnumerable<QueryTagDto>>> QueryTagsAsync(Func<Task<ServiceResult<IEnumerable<QueryTagDto>>>> factory);
    }
}
