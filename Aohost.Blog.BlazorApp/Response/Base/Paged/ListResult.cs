using System.Collections.Generic;

namespace Aohost.Blog.BlazorApp.Response.Base.Paged
{
    public class ListResult<T> : IListResult<T>
    {
        private IReadOnlyList<T> _item;

        public IReadOnlyList<T> Item
        {
            get => _item ??= new List<T>();
            set => _item = value;
        }

        public ListResult()
        {

        }

        public ListResult(IReadOnlyList<T> item)
        {
            Item = item;
        }
    }
}