using Aohost.Blog.Application.Contracts.Blog.Category;
using Aohost.Blog.Domain.Blog.Repositories;
using Aohost.Blog.ToolKits;
using Aohost.BlogApplication.Caching.Blog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aohost.Blog.Application.Blog.Impl
{
    public partial class BlogService:ServiceBase, IBlogService
    {
        private readonly IBlogCacheService _blogCacheService;
        private readonly IPostRepository _postRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IPostTagRepository _postTagRepository;
        private readonly IFriendLinkRepository _friendLinkRepository;


        public BlogService(IBlogCacheService blogCacheService, IPostRepository postRepository,
            ICategoryRepository categoryRepository, ITagRepository tagRepository, IPostTagRepository postTagRepository,
            IFriendLinkRepository friendLinkRepository)
        {
            _blogCacheService = blogCacheService;
            _postRepository = postRepository;
            _categoryRepository = categoryRepository;
            _tagRepository = tagRepository;
            _postTagRepository = postTagRepository;
            _friendLinkRepository = friendLinkRepository;
        }

    }
}