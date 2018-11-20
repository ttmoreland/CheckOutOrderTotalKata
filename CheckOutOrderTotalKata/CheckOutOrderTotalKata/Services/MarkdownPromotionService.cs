using CheckOutOrderTotalKata.Models;
using CheckOutOrderTotalKata.Util;
using Microsoft.Extensions.Caching.Memory;
using System.Linq;

namespace CheckOutOrderTotalKata.Services
{
    /// <summary>
    /// Markdown Promotion Service
    /// </summary>
    public class MarkdownPromotionService : BaseCacheService<MarkdownPromotion>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="MarkdownPromotionService"/> class.
        /// </summary>
        /// <param name="cache">The cache.</param>
        public MarkdownPromotionService(IMemoryCache cache) : base(cache)
        {
        }

        /// <summary>
        /// Gets the temperament.
        /// </summary>
        /// <value>
        /// The temperament.
        /// </value>
        public override string CacheKey => CacheKeys.MarkdownPromotion;
    }
}
