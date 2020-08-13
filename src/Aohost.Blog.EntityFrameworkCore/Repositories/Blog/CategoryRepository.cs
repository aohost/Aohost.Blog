using Aohost.Blog.Domain.Blog;
using Aohost.Blog.Domain.Blog.Repositories;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Aohost.Blog.EntityFrameworkCore.Repositories.Blog
{
    public class CategoryRepository:EfCoreRepository<BlogDbContext, Category, int>, ICategoryRepository
    {
        public CategoryRepository(IDbContextProvider<BlogDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}