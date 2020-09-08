using System.Collections.Generic;

namespace Aohost.Blog.BlazorApp.Response.Base.Paged
{
    public interface IListResult<T>
    {
        IReadOnlyList<T> Item { get; set; }
    }
}