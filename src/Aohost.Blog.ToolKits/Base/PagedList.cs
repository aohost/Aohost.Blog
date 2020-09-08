using System.Collections.Generic;
using Aohost.Blog.ToolKits.Paged;

namespace Aohost.Blog.ToolKits
{
    public class PagedList<T>:ListResult<T>, IPagedList<T>
    {
        public int Total { get; set; }

        public PagedList()
        {
            
        }

        public PagedList(int total, IReadOnlyList<T> result) : base(result)
        {
            Total = total;
        }
    }
}