using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Meowv.Blog.Blog;
using Meowv.Blog.Blog.Repositories;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Meowv.Blog.Repositories.Blog
{
   public class PostTagRepository:EfCoreRepository<MeowvBlogDbContext,PostTag,int>,IPostTagRepository
    {
        public PostTagRepository(IDbContextProvider<MeowvBlogDbContext> contextProvider):base(contextProvider)
        {
            
        }

        public async Task BulkInsertAsync(IEnumerable<PostTag> postTags)
        {
            await DbContext.Set<PostTag>().AddRangeAsync(postTags);
            await DbContext.SaveChangesAsync();
        }
    }
}
