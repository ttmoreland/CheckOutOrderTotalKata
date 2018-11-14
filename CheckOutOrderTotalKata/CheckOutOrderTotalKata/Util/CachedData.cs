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
        public static List<CartItem> GetCachedCart(this IMemoryCache cache)
        {
            List<CartItem> cart;
            if (!cache.TryGetValue(CacheKeys.Cart, out cart))
            {
                //Cache doesn't have value so initialize
                cart = new List<CartItem>();

                cache.SetCachedCart(cart);
            }

            return cart;
        }

        public static void SetCachedCart(this IMemoryCache cache, List<CartItem> cart)
        {
            //Set cache value options
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetPriority(CacheItemPriority.NeverRemove);

            //Set cache item
            cache.Set(CacheKeys.Cart, cart, cacheEntryOptions);
        }
    }
}
