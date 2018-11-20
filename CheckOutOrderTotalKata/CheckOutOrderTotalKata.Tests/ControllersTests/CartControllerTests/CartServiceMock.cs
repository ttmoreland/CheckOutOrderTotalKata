using CheckOutOrderTotalKata.Models;
using CheckOutOrderTotalKata.Util;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckOutOrderTotalKata.ModelTests.ControllersTests
{
    public class CartServiceMock : BaseCacheService<CartItem>
    {
        private readonly List<CartItem> _cart;

        public CartServiceMock(IMemoryCache cache) : base(cache)
        {
            _cart = new List<CartItem>()
            {
                new CartItem("Soup", 1.00m),
                new CartItem("Steak", 4.75m),
                new CartItem("Apple", 3.00m)
            };

            cache.SetCachedItem(CacheKey, _cart);
        }

        public override string CacheKey => CacheKeys.Cart;
    }
}
