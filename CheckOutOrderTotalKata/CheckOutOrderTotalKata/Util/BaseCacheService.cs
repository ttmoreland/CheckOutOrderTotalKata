using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheckOutOrderTotalKata.Util
{
    public abstract class BaseCacheService<T> : IBaseService<T>
    {
        /// <summary>
        /// The cart
        /// </summary>
        private List<T> _items;

        /// <summary>
        /// The cache
        /// </summary>
        private readonly IMemoryCache _cache;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseCacheService{T}" /> class.
        /// </summary>
        /// <param name="cache">The cache.</param>
        public BaseCacheService(IMemoryCache cache)
        {
            _items = cache.GetCachedItem<T>(CacheKey);
            _cache = cache;
        }

        /// <summary>
        /// Adds the specified new item.
        /// </summary>
        /// <param name="newItem">The new item.</param>
        /// <returns></returns>
        public T Add(T newItem)
        {
            _items = GetAllItems();
            _items.Add(newItem);
            _cache.SetCachedItem(CacheKey, _items);
            return newItem;
        }

        /// <summary>
        /// Gets all items.
        /// </summary>
        /// <returns></returns>
        public List<T> GetAllItems()
        {
            return _cache.GetCachedItem<T>(CacheKey);
        }

        /// <summary>
        /// Removes the specified item name.
        /// </summary>
        /// <param name="item">The item.</param>
        public void Remove(T item)
        {
            _items = GetAllItems();
            _items.Remove(item);
            _cache.SetCachedItem(CacheKey, _items);
        }

        /// <summary>
        /// Gets the item.
        /// </summary>
        /// <param name="itemName">Name of the item.</param>
        /// <returns></returns>
        abstract public T GetItem(string itemName);

        /// <summary>
        /// Gets the temperament.
        /// </summary>
        /// <value>
        /// The temperament.
        /// </value>
        public abstract string CacheKey { get; }
    }
}
