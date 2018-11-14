using CheckOutOrderTotalKata.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheckOutOrderTotalKata.Util
{
    public static class CachedData
    {
        public static List<T> GetCachedItem<T>(this IMemoryCache cache, string cacheKey)
        {
            List<T> item;
            if (!cache.TryGetValue(CacheKeys.Cart, out item))
            {
                //Cache doesn't have value so initialize
                item = new List<T>();

                cache.SetCachedItem(cacheKey, item);
            }

            return item;
        }

        public static void SetCachedItem<T>(this IMemoryCache cache, string cacheKey, List<T> items)
        {
            //Set cache value options
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetPriority(CacheItemPriority.NeverRemove);

            //Set cache item
            cache.Set(cacheKey, items, cacheEntryOptions);
        }
    }
}
