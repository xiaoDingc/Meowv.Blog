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
    public class TagRepository : EfCoreRepository<MeowvBlogDbContext, Tag, int>, ITagRepository
    {
        public TagRepository(IDbContextProvider<MeowvBlogDbContext> contextProvider) : base(contextProvider)
        {

        }

        public async Task BulkInsertAsync(IEnumerable<Tag> tags)
        {
            //  await DbContext.Set<Tag>().AddRangeAsync(tags);
            //await DbContext.SaveChangesAsync();
            await DbContext.Set<Tag>().AddRangeAsync(tags);
            await DbContext.SaveChangesAsync();
        }
    }
}
