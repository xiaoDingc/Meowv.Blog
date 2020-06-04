using System;
using System.Collections.Generic;
using System.Text;
using Meowv.Blog.Blog;
using Meowv.Blog.Blog.Repositories;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Meowv.Blog.Repositories.Blog
{
    public class PostRepository : EfCoreRepository<MeowvBlogDbContext, Post, int>, IPostRepository
    {

        public PostRepository(IDbContextProvider<MeowvBlogDbContext> contextProvider) : base(contextProvider)
        {

        }
    }

}
