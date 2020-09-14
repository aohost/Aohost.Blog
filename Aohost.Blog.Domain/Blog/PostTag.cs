using Volo.Abp.Domain.Entities;

namespace Aohost.Blog.Domain.Blog
{
    public class PostTag:Entity<int>
    {
        /// <summary>
        /// 文章ID
        /// </summary>
        public int PostId { get; set; }

        /// <summary>
        /// 标签ID
        /// </summary>
        public int TagId { get; set; }
    }
}