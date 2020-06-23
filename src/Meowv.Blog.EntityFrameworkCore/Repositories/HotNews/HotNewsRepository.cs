using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Meowv.Blog.Domain.Wallpaper;
using Meowv.Blog.HotNews.Repositories;
using Meowv.Blog.Wallpaper.Repositories;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Meowv.Blog.Repositories.HotNews
{
    public class HotNewsRepository : EfCoreRepository<MeowvBlogDbContext, Meowv.Blog.HotNews.HotNews, Guid>, IHotNewsRepository
    {
        public HotNewsRepository(IDbContextProvider<MeowvBlogDbContext> dbContextProvider) : base(dbContextProvider)
        {

        }

        public async Task BulkInsertAsync(IEnumerable<Meowv.Blog.HotNews.HotNews> hotNewses)
        {
            await DbContext.Set<Meowv.Blog.HotNews.HotNews>().AddRangeAsync(hotNewses);
            await DbContext.SaveChangesAsync();
        }
    }
}
