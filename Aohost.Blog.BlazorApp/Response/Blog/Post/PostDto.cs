using System;

namespace Aohost.Blog.BlazorApp.Response.Blog.Post
{
    public class PostDto
    {
        public string Title { get; set; }

        public string Author { get; set; }

        public string Url { get; set; }

        public string Html { get; set; }

        public string Markdown { get; set; }

        public int CategoryId { get; set; }

        public DateTime CreationTime { get; set; }
    }
}