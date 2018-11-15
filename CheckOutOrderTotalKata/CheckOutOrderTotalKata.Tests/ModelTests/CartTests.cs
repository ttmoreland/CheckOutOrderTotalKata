using System.Collections.Generic;
using Xunit;
using CheckOutOrderTotalKata.Controllers;
using CheckOutOrderTotalKata.Util;
using Microsoft.AspNetCore.Mvc;
using CheckOutOrderTotalKata.Models;
using System.Linq;
using Microsoft.Extensions.Caching.Memory;
using CheckOutOrderTotalKata.ModelTests.ControllersTests;
namespace CheckOutOrderTotalKata.ModelTests
{
    public class CartTests
    {
        BaseCacheService<CartItem> _cart;
        BaseCacheService<StoreItem> _store;

        [Fact]
        public void Cart_TestProperties()
        {
            var cache = new MemoryCache(new MemoryCacheOptions());
            _cart = new CartServiceMock(cache);
            _store = new StoreServiceMock(cache);

            Cart cart = new Cart();
            cart.AddPricedItems(_cart.GetAllItems(), _store.GetAllItems());

            Assert.Equal(32.5625m, cart.Total);
        }
    }
}
