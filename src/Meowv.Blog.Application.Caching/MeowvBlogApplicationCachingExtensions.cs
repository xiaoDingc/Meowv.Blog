using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Meowv.Blog.Domain.Shared;
using Meowv.Blog.ToolKits.Extensions;
using Microsoft.Extensions.Caching.Distributed;

namespace Meowv.Blog.Application.Caching
{
    public static class MeowvBlogApplicationCachingExtensions
    {
        public static async Task<TCacheItem> GetOrAddAsync<TCacheItem>(this IDistributedCache cache, string key, Func<Task<TCacheItem>> factory, int minutes)
        {
            TCacheItem cacheItem;
            var result = await cache.GetStringAsync(key);
            if (string.IsNullOrEmpty(result))
            {
                cacheItem = await factory.Invoke();
                var options = new DistributedCacheEntryOptions();
                if (minutes != CacheStrategy.NEVER)
                {
                    options.AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(minutes);
                }
                await cache.SetStringAsync(key, cacheItem.ToJson(), options);
            }
            else
            {
                cacheItem = result.FromJson<TCacheItem>();
            }

            return cacheItem;
        }
    }
}
