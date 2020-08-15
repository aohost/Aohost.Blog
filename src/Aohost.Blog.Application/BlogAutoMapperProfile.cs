using Aohost.Blog.Application.Contracts.Blog;
using Aohost.Blog.Domain.Blog;
using AutoMapper;

namespace Aohost.Blog.Application
{
    public class BlogAutoMapperProfile : Profile
    {
        public BlogAutoMapperProfile()
        {
            CreateMap<Post, PostDto>();

            CreateMap<PostDto, Post>().ForMember(x => x.Id, opt => opt.Ignore());
        }
    }
}