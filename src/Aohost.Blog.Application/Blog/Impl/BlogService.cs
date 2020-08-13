using System.Threading.Tasks;
using Aohost.Blog.Application.Contracts.Blog;
using Aohost.Blog.Domain.Blog;
using Aohost.Blog.Domain.Blog.Repositories;

namespace Aohost.Blog.Application.Blog.Impl
{
    public class BlogService:ServiceBase, IBlogService
    {
        private readonly IPostRepository _postRepository;

        public BlogService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<bool> InsertPostAsync(PostDto dto)
        {
            var entity = new Post
            {
                Title = dto.Title,
                Author = dto.Author,
                Url = dto.Url,
                Html = dto.Html,
                Markdown = dto.Markdown,
                CategoryId = dto.CategoryId,
                CreationTime = dto.CreationTime
            };

            var post = await _postRepository.InsertAsync(entity, true);
            return post != null;
        }

        public async Task<bool> DeletePostAsync(int id)
        {
            await _postRepository.DeleteAsync(id);

            return true;
        }

        public async Task<bool> UpdatePostAsync(int id, PostDto dto)
        {
            var post = await _postRepository.GetAsync(id);

            post.Title = dto.Title;
            post.Author = dto.Author;
            post.CreationTime = dto.CreationTime;
            post.Html = dto.Html;
            post.Markdown = dto.Markdown;
            post.CategoryId = dto.CategoryId;
            post.Url = dto.Url;

            await _postRepository.UpdateAsync(post);
            return true;
        }

        public async Task<PostDto> GetPostAsync(int id)
        {
            var entity = await _postRepository.GetAsync(id);
            return new PostDto
            {
                Title = entity.Title,
                Author = entity.Author,
                CategoryId = entity.Id,
                CreationTime = entity.CreationTime,
                Html = entity.Html,
                Markdown = entity.Markdown
            };
        }
    }
}