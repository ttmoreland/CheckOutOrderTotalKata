using CheckOutOrderTotalKata.Models;
using CheckOutOrderTotalKata.Util;
using Microsoft.Extensions.Caching.Memory;
using System.Linq;

namespace CheckOutOrderTotalKata.Services
{
    /// <summary>
    /// BOGO Promotion
    /// </summary>
    /// <seealso cref="CheckOutOrderTotalKata.Util.BaseCacheService{CheckOutOrderTotalKata.Models.BogoPromotion}" />
    public class BogoPromotionService : BaseCacheService<BogoPromotion>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BogoPromotionService"/> class.
        /// </summary>
        /// <param name="cache">The cache.</param>
        /// <param name="cacheKey">The cache key.</param>
        public BogoPromotionService(IMemoryCache cache) : base(cache)
        {
        }

        /// <summary>
        /// Gets the temperament.
        /// </summary>
        /// <value>
        /// The temperament.
        /// </value>
        public override string CacheKey => CacheKeys.BogoPromotion;

        /// <summary>
        /// Gets the item.
        /// </summary>
        /// <param name="itemName">Name of the item.</param>
        /// <returns></returns>
        public override BogoPromotion GetItem(string Name)
        {
            return GetAllItems().Where(a => a.Name == Name).FirstOrDefault();
        }
    }
}
