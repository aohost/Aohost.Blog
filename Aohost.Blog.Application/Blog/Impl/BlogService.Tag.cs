using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aohost.Blog.Application.Contracts.Blog.Tag;
using Aohost.Blog.Domain.Shared;
using Aohost.Blog.ToolKits.Base;
using Aohost.Blog.ToolKits.Extensions;

namespace Aohost.Blog.Application.Blog.Impl
{
    public partial class BlogService
    {
        /// <summary>
        /// 查询标签列表
        /// </summary>
        /// <returns></returns>
        public async Task<ServiceResult<IEnumerable<QueryTagDto>>> QueryTagsAsync()
        {
            return await _blogCacheService.QueryTagsAsync(async () =>
            {
                var result = new ServiceResult<IEnumerable<QueryTagDto>>();

                var list = from tags in await _tagRepository.GetListAsync()
                    join post_tags in await _postTagRepository.GetListAsync() on tags.Id equals post_tags.TagId
                    group tags by new
                    {
                        tags.TagName,
                        tags.DisplayName
                    }
                    into g
                    select new QueryTagDto
                    {
                        DisplayName = g.Key.DisplayName,
                        TagName = g.Key.TagName,
                        Count = g.Count()
                    };

                result.IsSuccess(list);
                return result;
            });
        }

        /// <summary>
        /// 获取标签名称
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<ServiceResult<string>> GetTagAsync(string name)
        {
            return await _blogCacheService.GetTagAsync(name, async () =>
            {
                var result = new ServiceResult<string>();

                var tag = await _tagRepository.FindAsync(x => x.DisplayName == name);

                if (tag == null)
                {
                    result.IsFailed(ResponseText.WAHT_NOT_EXIST.FormatWith("标签", name));
                    return result;
                }

                result.IsSuccess(tag.TagName);
                return result;
            });
        }
    }
}
