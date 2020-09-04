using Aohost.Blog.Application.Contracts.Blog;
using Aohost.Blog.Application.Contracts.Blog.Category;
using Aohost.Blog.Application.Contracts.Blog.FriendLink;
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
            #region Blog

            CreateMap<Post, PostDto>();
            CreateMap<PostDto, Post>().ForMember(x => x.Id, opt => opt.Ignore());
            CreateMap<EditPostInput, Post>().ForMember(x => x.Id, opt => opt.Ignore());
            CreateMap<Post, PostForAdminDto>().ForMember(x => x.Tags, opt => opt.Ignore());

            CreateMap<FriendLink, FriendLinkDto>();

            CreateMap<EditCategoryInput, Category>().ForMember(x => x.Id, opt => opt.Ignore());

            #endregion

            CreateMap<Domain.Wallpaper.Wallpaper, WallpaperDto>();
            CreateMap<WallpaperDto, Domain.Wallpaper.Wallpaper>().ForMember(x => x.Type, opt => opt.Ignore())
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.CreateTime, opt => opt.Ignore());
        }
    }
}