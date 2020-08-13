using Volo.Abp.Domain.Entities;

namespace Aohost.Blog.Domain.Blog
{
    public class FriendLink:Entity<int>
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 链接
        /// </summary>
        public string LinkUrl { get; set; }
    }
}