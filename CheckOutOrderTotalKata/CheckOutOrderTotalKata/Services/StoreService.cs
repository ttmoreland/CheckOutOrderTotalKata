using System.Linq;
using CheckOutOrderTotalKata.Models;
using CheckOutOrderTotalKata.Util;
using Microsoft.Extensions.Caching.Memory;

namespace CheckOutOrderTotalKata.Services
{
    public class StoreService : BaseCacheService<StoreItem>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="StoreService" /> class.
        /// </summary>
        /// <param name="cache">The cache.</param>
        public StoreService(IMemoryCache cache) : base(cache)
        {
        }

        /// <summary>
        /// Gets the temperament.
        /// </summary>
        /// <value>
        /// The temperament.
        /// </value>
        public override string CacheKey => CacheKeys.Store;

        /// <summary>
        /// Gets the item.
        /// </summary>
        /// <param name="itemName">Name of the item.</param>
        /// <returns></returns>
        public override StoreItem GetItem(string itemName)
        {
            return GetAllItems().Where(a => a.Name == itemName).FirstOrDefault();
        }
    }
}
