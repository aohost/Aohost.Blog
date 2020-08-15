using Aohost.Blog.Domain.Blog;
using Aohost.Blog.Domain.Wallpaper;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Aohost.Blog.EntityFrameworkCore
{
    public class BlogDbContext : AbpDbContext<BlogDbContext>
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<PostTag> PostTags { get; set; }
        public DbSet<FriendLink> FriendLinks { get; set; }

        public DbSet<Wallpaper> Wallpapers { get; set; }

        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Configure();
        }
    }
}