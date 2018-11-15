using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CheckOutOrderTotalKata.Models;
using Microsoft.Extensions.Caching.Memory;

namespace CheckOutOrderTotalKata.Util
{
    /// <summary>
    /// Cart Service
    /// </summary>
    /// <seealso cref="CheckOutOrderTotalKata.Util.ICartService" />
    public class CartService : IBaseService<CartItem>
    {
        /// <summary>
        /// The cart
        /// </summary>
        private readonly List<CartItem> _cart;

        /// <summary>
        /// The cache
        /// </summary>
        private readonly IMemoryCache _cache;

        /// <summary>
        /// Initializes a new instance of the <see cref="CartService"/> class.
        /// </summary>
        /// <param name="cache">The cache.</param>
        public CartService(IMemoryCache cache)
        {
            _cart = cache.GetCachedItem<CartItem>(CacheKeys.Cart);
            _cache = cache;
        }

        /// <summary>
        /// Gets all items.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CartItem> GetAllItems()
        {
            return _cart;
        }

        /// <summary>
        /// Adds the specified new item.
        /// </summary>
        /// <param name="newItem">The new item.</param>
        /// <returns></returns>
        public CartItem Add(CartItem newItem)
        {
            _cart.Add(newItem);
            _cache.SetCachedItem(CacheKeys.Cart, _cart);
            return newItem;
        }

        /// <summary>
        /// Gets the item by name of item.
        /// </summary>
        /// <param name="itemName">Name of the item.</param>
        /// <returns></returns>
        public CartItem GetItem(string itemName)
        {
            return _cart.Where(a => a.Name == itemName).FirstOrDefault();
        }

        /// <summary>
        /// Removes the specified item name.
        /// </summary>
        /// <param name="itemName">Name of the item.</param>
        public void Remove(string itemName)
        {
            var existing = this.GetItem(itemName);
            _cache.SetCachedItem(CacheKeys.Cart, _cart);
            _cart.Remove(existing);
        }
    }
}
