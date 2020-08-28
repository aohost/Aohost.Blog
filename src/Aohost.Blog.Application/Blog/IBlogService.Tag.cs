using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Aohost.Blog.Application.Contracts.Blog.Tag;
using Aohost.Blog.ToolKits;

namespace Aohost.Blog.Application.Blog
{
    public partial interface IBlogService
    {
        /// <summary>
        /// 获取标签列表
        /// </summary>
        /// <returns></returns>
        Task<ServiceResult<IEnumerable<QueryTagDto>>> QueryTagsAsync();
    }
}
