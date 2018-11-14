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
        private readonly List<StoreItem> _store;

        private readonly IMemoryCache _cache;

        public StoreService(IMemoryCache cache)
        {
            _store = cache.GetCachedItem<StoreItem>(CacheKeys.Store);
            _cache = cache;
        }

        public StoreItem Add(StoreItem newItem)
        {
            _store.Add(newItem);
            _cache.SetCachedItem(CacheKeys.Store, _store);
            return newItem;
        }

        public IEnumerable<StoreItem> GetAllItems()
        {
            return _store;
        }

        public StoreItem GetItem(string itemName)
        {
            return _store.Where(a => a.Name == itemName).FirstOrDefault();
        }

        public void Remove(string itemName)
        {
            var existing = this.GetItem(itemName);
            _cache.SetCachedItem(CacheKeys.Store, _store);
            _store.Remove(existing);
        }
    }
}
