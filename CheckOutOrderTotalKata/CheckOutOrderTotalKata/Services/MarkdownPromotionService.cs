using CheckOutOrderTotalKata.Models;
using CheckOutOrderTotalKata.Util;
using Microsoft.Extensions.Caching.Memory;
using System.Linq;

namespace CheckOutOrderTotalKata.Services
{
    /// <summary>
    /// Markdown Promotion Service
    /// </summary>
    /// <seealso cref="CheckOutOrderTotalKata.Util.BaseCacheService{CheckOutOrderTotalKata.Models.MarkdownPromotion}" />
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

        /// <summary>
        /// Gets the item.
        /// </summary>
        /// <param name="itemName">Name of the item.</param>
        /// <returns></returns>
        public override MarkdownPromotion GetItem(string Name)
        {
            return GetAllItems().Where(a => a.Name == Name).FirstOrDefault();
        }
    }
}
