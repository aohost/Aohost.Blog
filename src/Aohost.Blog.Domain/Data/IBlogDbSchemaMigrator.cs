using System.Threading.Tasks;

namespace Aohost.Blog.Data
{
    public interface IBlogDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
