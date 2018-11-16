using CheckOutOrderTotalKata.Models;
using CheckOutOrderTotalKata.Util;
using Microsoft.Extensions.Caching.Memory;
using System.Linq;

namespace CheckOutOrderTotalKata.Services
{
    /// <summary>
    /// BOGO Promotion
    /// </summary>
    public class BogoPromotionService : BaseCacheService<BogoPromotion>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BogoPromotionService" /> class.
        /// </summary>
        /// <param name="cache">The cache.</param>
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
        /// <param name="Name">The name.</param>
        /// <returns></returns>
        public override BogoPromotion GetItem(string Name)
        {
            return GetAllItems().Where(a => a.Name == Name).FirstOrDefault();
        }
    }
}
