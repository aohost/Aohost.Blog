﻿using System;
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

        /// <summary>
        /// 获取分类名称
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<ServiceResult<string>> GetCategoryAsync(string name);
    }
}
