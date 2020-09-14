using Volo.Abp.Domain.Repositories;

namespace Aohost.Blog.Domain.Blog.Repositories
{
    public interface IPostRepository:IRepository<Post, int>
    {
        
    }
}