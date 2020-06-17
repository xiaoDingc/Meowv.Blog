using Meowv.Blog.Domain.Wallpaper;
using Meowv.Blog.Wallpaper;
using Meowv.Blog.Wallpaper.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Meowv.Blog.Repositories.Wallpaper
{
    public class WallpaperRepository : EfCoreRepository<MeowvBlogDbContext, WallPaper, Guid>, IWallpaperRepository
    {
        public WallpaperRepository(IDbContextProvider<MeowvBlogDbContext> contextProvider) : base(contextProvider)
        {

        }

        public async Task BulkInsertAsync(IEnumerable<WallPaper> wallpapers)
        {
            await DbContext.Set<WallPaper>().AddRangeAsync(wallpapers);
            await DbContext.SaveChangesAsync();
        }
    }
}
