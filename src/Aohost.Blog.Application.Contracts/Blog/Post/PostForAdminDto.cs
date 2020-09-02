using System.Collections.Generic;

namespace Aohost.Blog.Application.Contracts.Blog.Post
{
    public class PostForAdminDto:PostDto
    {
        /// <summary>
        /// 标签列表
        /// </summary>
        public IEnumerable<string> Tags { get; set; }
    }
}