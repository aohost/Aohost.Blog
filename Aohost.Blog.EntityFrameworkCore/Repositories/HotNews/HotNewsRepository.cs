﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aohost.Blog.Domain.HotNews.Repository;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Aohost.Blog.EntityFrameworkCore.Repositories.HotNews
{
    /// <summary>
    /// HotNewsRepository
    /// </summary>
    public class HotNewsRepository:EfCoreRepository<BlogDbContext, Domain.HotNews.HotNews, Guid>, IHotNewsRepository
    {
        public HotNewsRepository(IDbContextProvider<BlogDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="hotNews"></param>
        /// <returns></returns>
        public async Task BulkInsertAsync(IEnumerable<Domain.HotNews.HotNews> hotNews)
        {
            await DbContext.Set<Domain.HotNews.HotNews>().AddRangeAsync(hotNews);
            await DbContext.SaveChangesAsync();
        }
    }
}