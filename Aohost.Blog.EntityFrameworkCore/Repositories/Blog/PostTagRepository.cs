using System.Collections.Generic;
using System.Threading.Tasks;
using Aohost.Blog.Domain.Blog;
using Aohost.Blog.Domain.Blog.Repositories;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Aohost.Blog.EntityFrameworkCore.Repositories.Blog
{
    public class PostTagRepository:EfCoreRepository<BlogDbContext, PostTag, int>,IPostTagRepository
    {
        public PostTagRepository(IDbContextProvider<BlogDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task BulkInsertAsync(IEnumerable<PostTag> postTags)
        {
            await DbContext.AddRangeAsync(postTags);
            await DbContext.SaveChangesAsync();
        }
    }
}