using System.Threading.Tasks;
using Aohost.Blog.Application.Contracts.Blog;
using Aohost.Blog.Domain.Blog;
using Aohost.Blog.Domain.Blog.Repositories;
using Aohost.Blog.ToolKits;

namespace Aohost.Blog.Application.Blog.Impl
{
    public class BlogService:ServiceBase, IBlogService
    {
        private readonly IPostRepository _postRepository;

        public BlogService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<ServiceResult<string>> InsertPostAsync(PostDto dto)
        {
            var result = new ServiceResult<string>();

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
            if (post == null)
            {
                result.IsFailed("添加失败");
                return result;
            }

            result.IsSuccess("添加成功");
            return result;
        }

        public async Task<ServiceResult> DeletePostAsync(int id)
        {
            var result = new ServiceResult();

            await _postRepository.DeleteAsync(id);

            return result;
        }

        public async Task<ServiceResult<string>> UpdatePostAsync(int id, PostDto dto)
        {
            var result = new ServiceResult<string>();

            var post = await _postRepository.GetAsync(id);

            post.Title = dto.Title;
            post.Author = dto.Author;
            post.CreationTime = dto.CreationTime;
            post.Html = dto.Html;
            post.Markdown = dto.Markdown;
            post.CategoryId = dto.CategoryId;
            post.Url = dto.Url;

            await _postRepository.UpdateAsync(post);
            result.IsSuccess("更新成功");
            return result;
        }

        public async Task<ServiceResult<PostDto>> GetPostAsync(int id)
        {
            var entity = await _postRepository.GetAsync(id);
            var dto = new PostDto
            {
                Title = entity.Title,
                Author = entity.Author,
                CategoryId = entity.Id,
                CreationTime = entity.CreationTime,
                Html = entity.Html,
                Markdown = entity.Markdown
            };

            var result = new ServiceResult<PostDto>();
            result.IsSuccess(dto);
            return result;
        }
    }
}