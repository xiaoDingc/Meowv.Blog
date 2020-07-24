﻿using Meowv.Blog.Application.Contracts.Blog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Meowv.Blog.Application.Caching.Blog;
using Meowv.Blog.Blog;
using Meowv.Blog.Blog.Repositories;
using Meowv.Blog.ToolKits.Base;
using Meowv.Blog.ToolKits.Base.Paged;

namespace Meowv.Blog.Application.Blog.Impl
{
    public partial class BlogService : MeowvBlogApplicationServiceBase, IBlogService
    {
        private readonly IBlogCacheService _blogCacheService;
        private readonly IPostRepository _postRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IPostTagRepository _postTagRepository;
        private readonly IFriendLinkRepository _friendLinksRepository;

        public BlogService(IBlogCacheService blogCacheService, IPostRepository postRepository, ICategoryRepository categoryRepository, ITagRepository tagRepository, IPostTagRepository postTagRepository, IFriendLinkRepository friendLinksRepository)
        {
            _blogCacheService = blogCacheService;
            _postRepository = postRepository;
            _categoryRepository = categoryRepository;
            _tagRepository = tagRepository;
            _postTagRepository = postTagRepository;
            _friendLinksRepository = friendLinksRepository;
        }

        // public async Task<ServiceResult> DeletePostAsync(int id)
        // {
        //     var result = new ServiceResult();
        //
        //     await _postRepository.DeleteAsync(id);
        //
        //     return result;
        // }

        public async Task<ServiceResult<string>> UpdatePostAsync(int id, PostDto dto)
        {

            var result = new ServiceResult<string>();
            var post = await _postRepository.GetAsync(id);

            if (post == null)
            {
                result.IsFailed("文章不存在");
                return result;
            }


            //post.Title = dto.Title;
            //post.Author = dto.Author;
            //post.Url = dto.Url;
            //post.Html = dto.Html;
            //post.Markdown = dto.Markdown;
            //post.CategoryId = dto.CategoryId;
            //post.CreationTime = dto.CreationTime;
            post = ObjectMapper.Map<PostDto, Post>(dto);

            await _postRepository.UpdateAsync(post);
            result.IsSuccess("修改成功");

            return result;
        }

        public async Task<ServiceResult<PostDto>> GetPostAsync(int id)
        {
            var result = new ServiceResult<PostDto>();
            var post = await _postRepository.GetAsync(id);

            if (post == null)
            {
                result.IsFailed("文章不存在");
                return result;
            }

            //var dto = new PostDto
            //{
            //    Title = post.Title,
            //    Author = post.Author,
            //    Url = post.Url,
            //    Html = post.Html,
            //    Markdown = post.Markdown,
            //    CategoryId = post.CategoryId,
            //    CreationTime = post.CreationTime
            //};

            var dto = ObjectMapper.Map<Post, PostDto>(post);
            result.IsSuccess(dto, "获取成功");

            return result;
        }

        public async Task<ServiceResult<string>> InsertPostAsync(PostDto dto)
        {
            var result = new ServiceResult<string>();
            //var entity = new Post
            //{
            //    Title = dto.Title,
            //    Author = dto.Author,
            //    Url = dto.Url,
            //    Html = dto.Html,
            //    Markdown = dto.Markdown,
            //    CategoryId = dto.CategoryId,
            //    CreationTime = dto.CreationTime
            //};

            var entity = ObjectMapper.Map<PostDto, Post>(dto);
            var post = await _postRepository.InsertAsync(entity);
            if (post == null)
            {
                result.IsFailed("添加失败");
                return result;
            }
            result.IsSuccess("添加成功");

            return result;
        }


    }
}
