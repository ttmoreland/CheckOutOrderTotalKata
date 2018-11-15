﻿using System.Linq;
using CheckOutOrderTotalKata.Models;
using CheckOutOrderTotalKata.Util;
using Microsoft.Extensions.Caching.Memory;

namespace CheckOutOrderTotalKata.Services
{
    /// <summary>
    /// Cart Service
    /// </summary>
    /// <seealso cref="CheckOutOrderTotalKata.Util.ICartService" />
    public class CartService : BaseCacheService<CartItem>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CartService"/> class.
        /// </summary>
        /// <param name="cache">The cache.</param>
        /// <param name="cacheKey">The cache key.</param>
        public CartService(IMemoryCache cache) : base(cache)
        {
        }

        /// <summary>
        /// Gets the temperament.
        /// </summary>
        /// <value>
        /// The temperament.
        /// </value>
        public override string CacheKey => CacheKeys.Cart;

        /// <summary>
        /// Gets the item.
        /// </summary>
        /// <param name="itemName">Name of the item.</param>
        /// <returns></returns>
        public override CartItem GetItem(string itemName)
        {
            return GetAllItems().Where(a => a.Name == itemName).FirstOrDefault();
        }
    }
}
