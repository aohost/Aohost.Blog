using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Aohost.Blog.Application.Contracts.Blog.FriendLink;
using Aohost.Blog.Domain.Blog;
using Aohost.Blog.ToolKits.Base;

namespace Aohost.Blog.Application.Blog.Impl
{
    public partial class BlogService
    {
        public async Task<ServiceResult<IEnumerable<FriendLinkDto>>> QueryFriendLinksAsync()
        {
            return await _blogCacheService.QueryFriendLinksAsync(async () =>
            {
                var result = new ServiceResult<IEnumerable<FriendLinkDto>>();
                var friendLinks = await _friendLinkRepository.GetListAsync();
                var friendLinksDto = ObjectMapper.Map<IEnumerable<FriendLink>, IEnumerable<FriendLinkDto>>(friendLinks);

                result.IsSuccess(friendLinksDto);
                return result;
            });
        }
    }
}
