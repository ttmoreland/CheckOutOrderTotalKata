using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CheckOutOrderTotalKata.Models;
using Microsoft.Extensions.Caching.Memory;

namespace CheckOutOrderTotalKata.Util
{
    public class StoreService : IBaseService<StoreItem>
    {
        private readonly List<StoreItem> _cart;

        private readonly IMemoryCache _cache;

        public StoreService(IMemoryCache cache)
        {
            _cart = cache.GetCachedItem<StoreItem>(CacheKeys.Store);
            _cache = cache;
        }

        public StoreItem Add(StoreItem newItem)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<StoreItem> GetAllItems()
        {
            return _cart;
        }

        public StoreItem GetItem(string itemName)
        {
            return _cart.Where(a => a.Name == itemName).FirstOrDefault();
        }

        public void Remove(string itemName)
        {
            throw new NotImplementedException();
        }
    }
}
