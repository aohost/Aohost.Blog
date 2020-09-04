using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aohost.Blog.Application.Contracts;
using Aohost.Blog.Application.Contracts.Blog.Category;
using Aohost.Blog.Application.Contracts.Blog.Post;
using Aohost.Blog.Domain.Blog;
using Aohost.Blog.Domain.Shared;
using Aohost.Blog.ToolKits;
using Aohost.Blog.ToolKits.Extensions;

namespace Aohost.Blog.Application.Blog.Impl
{
    public partial class BlogService
    {
        /// <summary>
        /// 管理后台获取文章详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ServiceResult<PostForAdminDto>> GetPostForAdminAsync(int id)
        {
            return await _blogCacheService.GetPostForAdminAsync(id, async () =>
            {
                var result = new ServiceResult<PostForAdminDto>();
                
                var post = await _postRepository.GetAsync(x => x.Id == id);
                var tags = from tag in await _tagRepository.GetListAsync()
                    join post_tags in await _postTagRepository.GetListAsync() on tag.Id equals post_tags.PostId
                    where post_tags.PostId.Equals(post.Id)
                    select tag.TagName;
               
                var dto = ObjectMapper.Map<Post, PostForAdminDto>(post);
                dto.Tags = tags;
                dto.Url = post.Url.Split("/").Last(x => !string.IsNullOrEmpty(x));
                
                result.IsSuccess(dto);
                return result;
            });
        }

        /// <summary>
        /// 查询文章
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ServiceResult<PagedList<QueryPostForAdminDto>>> QueryPostsForAdminAsync(PagingInput input)
        {
            return await _blogCacheService.QueryPostsForAdminAsync(input, async () =>
            {
                var result = new ServiceResult<PagedList<QueryPostForAdminDto>>();

                var count = _postRepository.GetCountAsync();
                var list = _postRepository.OrderByDescending(x => x.CreationTime).PageByIndex(input.Page, input.Limit)
                    .Select(x => new PostBriefForAdminDto
                    {
                        CreationTime = x.CreationTime.TryToDatetime(),
                        Id = x.Id,
                        Title = x.Title,
                        Url = x.Url,
                        Year = x.CreationTime.Year
                    }).GroupBy(x => x.Year)
                    .Select(x => new QueryPostForAdminDto
                    {
                        Year = x.Key,
                        Posts = x.ToList()
                    }).ToList();

                result.IsSuccess(new PagedList<QueryPostForAdminDto>(count.TryToInt(), list));
                return result;
            });
        }

        /// <summary>
        /// 新增文章
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ServiceResult> InsertPostAsync(EditPostInput input)
        {
            var result = new ServiceResult();

            var post = ObjectMapper.Map<EditPostInput, Post>(input);
            post.Url = $"{post.CreationTime.ToString("yyyy-MM-dd").Replace('-', '/')}/{post.Url}/";
            await _postRepository.InsertAsync(post);

            var tags = await _tagRepository.GetListAsync();
            var newTags = input.Tags.Where(item => !tags.Any(x => x.TagName.Equals(item)))
                .Select(item => new Tag
                {
                    TagName = item,
                    DisplayName = item
                }).ToArray();
            
            if (newTags.Length > 0)
            {
                await _tagRepository.BulkInsertAsync(newTags);
            }

            var postTags = input.Tags.Select(x => new PostTag
            {
                PostId = post.Id,
                TagId = _tagRepository.FirstOrDefault(item => x != null && item.TagName == x).Id
            }).ToArray();
            await _postTagRepository.BulkInsertAsync(postTags);

            result.IsSuccess(ResponseText.INSERT_SUCCESS);
            return result;
        }

        /// <summary>
        /// 更新文章
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ServiceResult> UpdatePostAsync(int id, EditPostInput input)
        {
            var result = new ServiceResult();

            var post = await _postRepository.GetAsync(id);
            post.Title = input.Title;
            post.CreationTime = input.CreationTime;
            post.Author = input.Author;
            post.Html = input.Html;
            post.Markdown = input.Markdown;
            post.Url = $"{input.CreationTime.ToString(" yyyy MM dd ").Replace(" ", "/")}{input.Url}";
            post.CategoryId = input.CategoryId;

            await _postRepository.UpdateAsync(post);

            var tags = await _tagRepository.GetListAsync();

            var oldPostTags = from post_tags in await _postTagRepository.GetListAsync()
                join tag in await _tagRepository.GetListAsync() on post_tags.TagId equals tag.Id
                where post_tags.PostId.Equals(post.Id)
                select new
                {
                    post_tags.Id,
                    tag.TagName
                };
            await _postTagRepository.DeleteAsync(x =>
                !oldPostTags.Where(item => input.Tags.All(m => m != item.TagName)).Select(item => item.Id)
                    .Contains(x.Id));

            var newTags = input.Tags.Where(x => tags.Any(m => m.TagName == x)).Select(x => new Tag
            {
                DisplayName = x,
                TagName = x,
            }).ToArray();
            await _tagRepository.BulkInsertAsync(newTags);

            var postTags = newTags.Select(x => new PostTag
            {
                PostId = post.Id,
                TagId = x.Id
            }).ToArray();
            await _postTagRepository.BulkInsertAsync(postTags);

            result.IsSuccess(ResponseText.UPDATE_SUCCESS);
            return result;
        }

        public async Task<ServiceResult> DeletePostAsync(int id)
        {
            var result = new ServiceResult();
            var post = await _postRepository.GetAsync(id);
            if (post == null)
            {
                result.IsFailed(ResponseText.WAHT_NOT_EXIST.FormatWith("Id", id));
                return result;
            }

            await _postRepository.DeleteAsync(id);
            await _postTagRepository.DeleteAsync(x => x.PostId == id);

            result.IsSuccess(ResponseText.DELETE_SUCCESS);
            return result;
        }

        /// <summary>
        /// 管理后台获取分类信息
        /// </summary>
        /// <returns></returns>
        public async Task<ServiceResult<IEnumerable<QueryCategoryForAdminDto>>> QueryCategoriesForAdmin()
        {
            return await _blogCacheService.QueryCategoriesForAdmin(async () =>
            {
                var result = new ServiceResult<IEnumerable<QueryCategoryForAdminDto>>();

                var posts = await _postRepository.GetListAsync();
                var categories = await _categoryRepository.GetListAsync();
                var categoriesDto = categories.Select(x => new QueryCategoryForAdminDto
                {
                    Id = x.Id,
                    CategoryName = x.CategoryName,
                    DisplayName = x.DisplayName,
                    Count = posts.Count(p => p.CategoryId == x.Id)
                });

                result.IsSuccess(categoriesDto);
                return result;
            });
        }

        /// <summary>
        /// 新增类别
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ServiceResult> InsertCategoryAsync(EditCategoryInput input)
        {
            var result = new ServiceResult();
            var category = ObjectMapper.Map<EditCategoryInput, Category>(input);
            await _categoryRepository.InsertAsync(category);
            result.IsSuccess(ResponseText.INSERT_SUCCESS);
            return result;
        }

        /// <summary>
        /// 更新类别信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ServiceResult> UpdateCategoryAsync(int id, EditCategoryInput input)
        {
            var result = new ServiceResult();
            var category = await _categoryRepository.GetAsync(id);
            category.DisplayName = input.DisplayName;
            category.CategoryName = input.CategoryName;

            await _categoryRepository.UpdateAsync(category);

            result.IsSuccess(ResponseText.UPDATE_SUCCESS);
            return result;
        }

        /// <summary>
        /// 删除类别信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ServiceResult> DeleteCategoryAsync(int id)
        {
            var result = new ServiceResult();
            await _categoryRepository.DeleteAsync(id);
            result.IsSuccess(ResponseText.DELETE_SUCCESS);
            return result;
        }
    }
}
