using System.Collections.Generic;

namespace Aohost.Blog.BlazorApp.Response.Blog.Post
{
    public class PostForAdminDto : PostDto
    {
        /// <summary>
        /// 标签列表
        /// </summary>
        public IEnumerable<string> Tags { get; set; }
    }
}