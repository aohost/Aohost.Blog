using System.Collections.Generic;

namespace Aohost.Blog.ToolKits.Paged
{
    public interface IListResult<T>
    {
        IReadOnlyList<T> Item { get; set; }
    }
}