using Aohost.Blog.Domain.Blog;
using Aohost.Blog.Domain.Blog.Repositories;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Aohost.Blog.EntityFrameworkCore.Repositories.Blog
{
    public class PostRepository:EfCoreRepository<BlogDbContext, Post, int>, IPostRepository
    {
        public PostRepository(IDbContextProvider<BlogDbContext> dbContextProvider) : base(dbContextProvider)
        {

        }
    }
}