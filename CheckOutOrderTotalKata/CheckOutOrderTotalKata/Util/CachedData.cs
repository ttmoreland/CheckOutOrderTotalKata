using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;

namespace CheckOutOrderTotalKata.Util
{
    /// <summary>
    /// Cached Data
    /// </summary>
    public static class CachedData
    {
        /// <summary>
        /// Gets the cached item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cache">The cache.</param>
        /// <param name="cacheKey">The cache key.</param>
        /// <returns></returns>
        public static List<T> GetCachedItem<T>(this IMemoryCache cache, string cacheKey)
        {
            List<T> item;
            if (!cache.TryGetValue(cacheKey, out item))
            {
                //Cache doesn't have value so initialize
                item = new List<T>();

                cache.SetCachedItem(cacheKey, item);
            }

            return item;
        }

        /// <summary>
        /// Sets the cached item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cache">The cache.</param>
        /// <param name="cacheKey">The cache key.</param>
        /// <param name="items">The items.</param>
        public static void SetCachedItem<T>(this IMemoryCache cache, string cacheKey, List<T> items)
        {
            //Set cache value options
            MemoryCacheEntryOptions cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetPriority(CacheItemPriority.NeverRemove);

            //Set cache item
            cache.Set(cacheKey, items, cacheEntryOptions);
        }
    }
}
