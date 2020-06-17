using Meowv.Blog.Domain.Wallpaper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Meowv.Blog.Wallpaper.Repositories
{
    /// <summary>
    /// IWallpaperRepository
    /// </summary>
    public interface IWallpaperRepository : IRepository<WallPaper, Guid>
    {
        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="wallpapers"></param>
        /// <returns></returns>
        Task BulkInsertAsync(IEnumerable<WallPaper> wallpapers);
    }
}
