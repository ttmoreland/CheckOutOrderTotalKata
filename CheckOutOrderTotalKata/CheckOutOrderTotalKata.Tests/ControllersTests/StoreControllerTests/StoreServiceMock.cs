using CheckOutOrderTotalKata.Models;
using CheckOutOrderTotalKata.Util;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckOutOrderTotalKata.ModelTests.ControllersTests
{
    public class StoreServiceMock : BaseCacheService<StoreItem>
    {
        private readonly List<StoreItem> _store;

        public StoreServiceMock(IMemoryCache cache) : base(cache)
        {
            _store = new List<StoreItem>()
            {
                new StoreItem("Soup", 1.00m),
                new StoreItem("Steak", 4.75m),
                new StoreItem("Apple", 3.00m),
                new StoreItem("Bread", 1.59m),
                new StoreItem("Chorizo", 3.99m)
            };

            cache.SetCachedItem(CacheKey, _store);
        }

        public override string CacheKey => CacheKeys.Store;

        public override StoreItem GetItem(string itemName)
        {
            return _store.Where(a => a.Name == itemName).FirstOrDefault();
        }
    }
}
