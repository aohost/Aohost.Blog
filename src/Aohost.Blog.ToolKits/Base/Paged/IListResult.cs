using System.Collections.Generic;

namespace Aohost.Blog.ToolKits.Base.Paged
{
    public interface IListResult<T>
    {
        IReadOnlyList<T> Item { get; set; }
    }
}