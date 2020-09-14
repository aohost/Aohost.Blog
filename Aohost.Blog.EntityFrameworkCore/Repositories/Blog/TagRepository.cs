using System.Collections.Generic;
using System.Threading.Tasks;
using Aohost.Blog.Domain.Blog;
using Aohost.Blog.Domain.Blog.Repositories;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Aohost.Blog.EntityFrameworkCore.Repositories.Blog
{
    public class TagRepository:EfCoreRepository<BlogDbContext, Tag, int>, ITagRepository
    {
        public TagRepository(IDbContextProvider<BlogDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task BulkInsertAsync(IEnumerable<Tag> tags)
        {
            await DbContext.Set<Tag>().AddRangeAsync(tags);
            await DbContext.SaveChangesAsync();
        }
    }
}