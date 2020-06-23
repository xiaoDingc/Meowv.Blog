using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Meowv.Blog.HotNews.Repositories
{
    public interface IHotNewsRepository : IRepository<HotNews, Guid>
    {
        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="hotNewses"></param>
        /// <returns></returns>
        Task BulkInsertAsync(IEnumerable<HotNews> hotNewses);
    }
}