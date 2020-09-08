namespace Aohost.Blog.Application.Contracts.Blog.Tag
{
    public class QueryTagDto:TagDto
    {
        /// <summary>
        /// 文章数量
        /// </summary>
        public int Count { get; set; }
    }
}