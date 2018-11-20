using System.Linq;
using CheckOutOrderTotalKata.Models;
using CheckOutOrderTotalKata.Util;
using Microsoft.Extensions.Caching.Memory;

namespace CheckOutOrderTotalKata.Services
{
    /// <summary>
    /// Cart Service
    /// </summary>
    public class CartService : BaseCacheService<CartItem>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CartService" /> class.
        /// </summary>
        /// <param name="cache">The cache.</param>
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
    }
}
