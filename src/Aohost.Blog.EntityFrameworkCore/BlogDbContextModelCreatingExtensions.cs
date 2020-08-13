using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace Aohost.Blog.EntityFrameworkCore
{
    public static class BlogDbContextModelCreatingExtensions
    {
        public static void Configure(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));
        }
    }
}