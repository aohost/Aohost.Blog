namespace Aohost.Blog.BlazorApp.Response.Blog.Category
{
    public class QueryCategoryDto : CategoryDto
    {
        /// <summary>
        /// 该类别下文章数量
        /// </summary>
        public int Count { get; set; }
    }
}