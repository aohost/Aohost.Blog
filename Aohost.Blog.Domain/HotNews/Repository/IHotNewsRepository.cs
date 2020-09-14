using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Aohost.Blog.Domain.HotNews.Repository
{
    public interface IHotNewsRepository:IRepository<HotNews, Guid>
    {
        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="hotNews"></param>
        /// <returns></returns>
        Task BulkInsertAsync(IEnumerable<HotNews> hotNews);
    }
}