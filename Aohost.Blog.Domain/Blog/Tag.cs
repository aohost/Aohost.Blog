using Volo.Abp.Domain.Entities;

namespace Aohost.Blog.Domain.Blog
{
    public class Tag:Entity<int>
    {
        /// <summary>
        /// 标签名称
        /// </summary>
        public string TagName { get; set; }

        /// <summary>
        /// 展示名称
        /// </summary>
        public string DisplayName { get; set; }
    }
}