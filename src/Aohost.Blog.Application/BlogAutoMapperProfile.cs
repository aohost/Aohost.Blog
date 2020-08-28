using Aohost.Blog.Application.Contracts.Blog;
using Aohost.Blog.Application.Contracts.Blog.Post;
using Aohost.Blog.Application.Contracts.Wallpaper;
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


            CreateMap<Domain.Wallpaper.Wallpaper, WallpaperDto>();

            CreateMap<WallpaperDto, Domain.Wallpaper.Wallpaper>().ForMember(x => x.Type, opt => opt.Ignore())
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.CreateTime, opt => opt.Ignore());
        }
    }
}