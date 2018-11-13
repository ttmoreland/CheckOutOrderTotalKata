using CheckOutOrderTotalKata.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheckOutOrderTotalKata.Util
{
    public static class CachedGroceryStore
    {
        public static GroceryStore GetCachedGroceryStore(this IMemoryCache cache)
        {
            GroceryStore groceryStore;
            if (!cache.TryGetValue(CacheKeys.GroceryStore, out groceryStore))
            {
                //Cache doesn't have value so initialize
                groceryStore = new GroceryStore();

                cache.SetCachedGroceryStore(groceryStore);
            }

            return groceryStore;
        }

        public static void SetCachedGroceryStore(this IMemoryCache cache, GroceryStore groceryStore)
        {
            //Set cache value options
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetPriority(CacheItemPriority.NeverRemove);

            //Set cache item
            cache.Set(CacheKeys.GroceryStore, groceryStore, cacheEntryOptions);
        }
    }
}
