using System;
using System.Collections.Generic;
using System.Text;
using Aohost.Blog.Domain.Shared;

namespace Aohost.BlogApplication.Caching.Blog.Impl
{
    public partial class BlogCacheService
    {
        private const string CategoryPrefix = CachePrefix.BlogCategory;

        private const string KEY_GetCategory = CategoryPrefix + ":GetCategory-{0}";
        private const string KEY_QueryCategories = CategoryPrefix + ":QueryCategories";

    }
}
