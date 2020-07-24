using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Meowv.Blog.Application.Contracts.Blog;
using Meowv.Blog.Application.Contracts.Blog.Params;
using Meowv.Blog.Blog;
using Meowv.Blog.Domain.Shared;
using Meowv.Blog.ToolKits.Base;
using Meowv.Blog.ToolKits.Base.Paged;
using Meowv.Blog.ToolKits.Extensions;

namespace Meowv.Blog.Application.Blog.Impl
{
    public partial class BlogService
    {
        /// <summary>
        /// 根据URL获取文章详情
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<ServiceResult<PostDetailDto>> GetPostDetailAsync(string url)
        {
            return await _blogCacheService.GetPostDetailAsync(url, async () =>
           {
               var result = new ServiceResult<PostDetailDto>();

               var post = await _postRepository.FindAsync(x => x.Url.Equals(url));
               if (post == null)
               {
                   result.IsFailed(ResponseText.WHAT_NOT_EXIST);
                   return result;
               }
               var category = await _categoryRepository.GetAsync(post.Id);

               var tags = from post_tags in await _postTagRepository.GetListAsync()
                          join tag in await _tagRepository.GetListAsync()
                           on post_tags.TagId equals tag.Id
                          where post_tags.PostId.Equals(post.Id)
                          select new TagDto
                          {
                              TagName = tag.TagName,
                              DisplayName = tag.DisplayName
                          };

               var previous = _postRepository.Where(x => x.CreationTime > post.CreationTime).Take(1).FirstOrDefault();
               var next = _postRepository.Where(x => x.CreationTime < post.CreationTime).OrderByDescending(x => x.CreationTime).Take(1).FirstOrDefault();

               var postDetail = new PostDetailDto
               {
                   Title = post.Title,
                   Author = post.Author,
                   Url = post.Url,
                   Html = post.Html,
                   Markdown = post.Markdown,
                   CreationTime = post.CreationTime.TryToDateTime(),
                   Category = new CategoryDto
                   {
                       CategoryName = category.CategoryName,
                       DisplayName = category.DisplayName
                   },
                   Tags = tags,
                   Previous = previous == null ? null : new PostForPagedDto
                   {
                       Title = previous.Title,
                       Url = previous.Url
                   },
                   Next = next == null ? null : new PostForPagedDto
                   {
                       Title = next.Title,
                       Url = next.Url
                   }
               };

               result.IsSuccess(postDetail);
               return result;

           });
        }

        public async Task<ServiceResult<PagedList<QueryPostDto>>> QueryPostsAsync(PagingInput input)
        {
            return await _blogCacheService.QueryPostsAsync(input, async () =>
            {
                var result = new ServiceResult<PagedList<QueryPostDto>>();

                var count = await _postRepository.GetCountAsync();

                var list = _postRepository.OrderByDescending(x => x.CreationTime)
                                          .PageByIndex(input.Page, input.Limit)
                                          .Select(x => new PostBriefDto
                                          {
                                              Title = x.Title,
                                              Url = x.Url,
                                              Year = x.CreationTime.Year,
                                              CreationTime = x.CreationTime.TryToDateTime()
                                          }).GroupBy(x => x.Year)
                                          .Select(x => new QueryPostDto
                                          {
                                              Year = x.Key,
                                              PostDtos = x.ToList()
                                          }).ToList();

                result.IsSuccess(new PagedList<QueryPostDto>(count.TryToInt(), list));
                return result;
            });
        }

        /// <summary>
        /// 通过标签名称查询文章列表
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<ServiceResult<IEnumerable<QueryPostDto>>> QueryPostsByTagAsync(string name)
        {
            return await _blogCacheService.QueryPostsByTagAsync(name, async () =>
            {
                var result = new ServiceResult<IEnumerable<QueryPostDto>>();

                var list = (from post_tags in await _postTagRepository.GetListAsync()
                            join tags in await _tagRepository.GetListAsync()
                                on post_tags.TagId equals tags.Id
                            join posts in await _postRepository.GetListAsync()
                                on post_tags.PostId equals posts.Id
                            where tags.DisplayName.Equals(name)
                            orderby posts.CreationTime descending
                            select new PostBriefDto
                            {
                                Title = posts.Title,
                                Url = posts.Url,
                                Year = posts.CreationTime.Year,
                                CreationTime = posts.CreationTime.TryToDateTime()
                            })
                    .GroupBy(x => x.Year)
                    .Select(x => new QueryPostDto
                    {
                        Year = x.Key,
                        PostDtos = x.ToList()
                    });

                result.IsSuccess(list);
                return result;
            });
        }

        /// <summary>
        /// 分页查询文章列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ServiceResult<PagedList<QueryPostForAdminDto>>> QueryPostsForAdminAsync(PagingInput input)
        {

            var result = new ServiceResult<PagedList<QueryPostForAdminDto>>();

            var count = await _postRepository.GetCountAsync();


            var list = _postRepository.OrderByDescending(x => x.CreationTime)
                .PageByIndex(input.Page, input.Limit)
                .Select(x => new PostBriefForAdminDto()
                {
                    Id = x.Id,
                    Title = x.Title,
                    Url = x.Url,
                    Year = x.CreationTime.Year,
                    CreationTime = x.CreationTime.TryToDateTime()
                })
                .GroupBy(x => x.Year)
                .Select(x => new QueryPostForAdminDto
                {
                    Year = x.Key,
                    Posts = x.ToList()
                }).ToList();

            result.IsSuccess(new PagedList<QueryPostForAdminDto>(count.TryToInt(), list));
            return result;
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
            post.Url = $"{post.CreationTime.ToString(" yyyy MM dd ").Replace(" ", "/")}{post.Url}/";
            await _postRepository.InsertAsync(post);

            var tags = await _tagRepository.GetListAsync();

            var newTags = input.Tags.Where(item => !tags.Any(x => x.TagName.Equals(item)))
                .Select(item => new Tag()
                {
                    TagName = item,
                    DisplayName = item,
                });
            await _tagRepository.BulkInsertAsync(newTags);

            var postTags = input.Tags.Select(item => new PostTag()
            {
                PostId = post.Id,
                TagId = _tagRepository.FirstOrDefault(x => x.TagName == item).Id
            });
            await _postTagRepository.BulkInsertAsync(postTags);

            result.IsSuccess(ResponseText.INSERT_SUCCESS);

            return result;
        }

        public async Task<ServiceResult> UpdatePostAsync(int id, EditPostInput input)
        {
            var result = new ServiceResult();

            var post = await _postRepository.GetAsync(id);
            post.Title = input.Title;
            post.Author = input.Author;
            post.Url = $"{input.CreationTime.ToString(" yyyy MM dd ").Replace(" ", "/")}{input.Url}/";
            post.Html = input.Html;
            post.Markdown = input.Markdown;
            post.CreationTime = input.CreationTime;
            post.CategoryId = input.CategoryId;

            await _postRepository.UpdateAsync(post);

            var tags = await _tagRepository.GetListAsync();

            var oldPostTags = from post_tags in await _postTagRepository.GetListAsync()
                              join tag in await _tagRepository.GetListAsync()
                              on post_tags.TagId equals tag.Id
                              where post_tags.PostId.Equals(post.Id)
                              select new
                              {
                                  post_tags.Id,
                                  tag.TagName
                              };

            var removedIds = oldPostTags.Where(item => !input.Tags.Any(x => x == item.TagName) &&
                                                       tags.Any(t => t.TagName == item.TagName))
                                        .Select(item => item.Id);
            await _postTagRepository.DeleteAsync(x => removedIds.Contains(x.Id));

            var newTags = input.Tags
                               .Where(item => !tags.Any(x => x.TagName == item))
                               .Select(item => new Tag
                               {
                                   TagName = item,
                                   DisplayName = item
                               });
            await _tagRepository.BulkInsertAsync(newTags);

            var postTags = input.Tags
                                .Where(item => !oldPostTags.Any(x => x.TagName == item))
                                .Select(item => new PostTag
                                {
                                    PostId = id,
                                    TagId = _tagRepository.FirstOrDefault(x => x.TagName == item).Id
                                });
            await _postTagRepository.BulkInsertAsync(postTags);

            result.IsSuccess(ResponseText.UPDATE_SUCCESS);
            return result;
        }

        /// <summary>
        /// 删除文章
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ServiceResult> DeletePostAsync(int id)
        {
            var result = new ServiceResult();

            var post = await _postRepository.GetAsync(id);
            if (null == post)
            {
                result.IsFailed(ResponseText.WHAT_NOT_EXIST.FormatWith("Id", id));

                return result;
            }

            await _postRepository.DeleteAsync(id);
            await _postTagRepository.DeleteAsync(x => x.PostId == id);

            result.IsSuccess(ResponseText.DELETE_SUCCESS);
            return result;
        }
    }
}
