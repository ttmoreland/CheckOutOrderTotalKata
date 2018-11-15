using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheckOutOrderTotalKata.Util
{
    /// <summary>
    /// Cache Key class to store cache strings
    /// </summary>
    public static class CacheKeys
    {
        /// <summary>
        /// Gets the cart.
        /// </summary>
        /// <value>
        /// The cart.
        /// </value>
        public static string Cart { get { return "_Cart"; } }

        /// <summary>
        /// Gets the grocery item.
        /// </summary>
        /// <value>
        /// The grocery item.
        /// </value>
        public static string Store { get { return "_Store"; } }

        /// <summary>
        /// Gets the bogo promotion.
        /// </summary>
        /// <value>
        /// The bogo promotion.
        /// </value>
        public static string BogoPromotion { get { return "_BogoPromotion"; } }

        /// <summary>
        /// Gets the bogo promotion.
        /// </summary>
        /// <value>
        /// The bogo promotion.
        /// </value>
        public static string MarkdownPromotion { get { return "_MarkdownPromotion"; } }

        /// <summary>
        /// Gets the bogo promotion.
        /// </summary>
        /// <value>
        /// The bogo promotion.
        /// </value>
        public static string MultiplesPromotion { get { return "_MultiplesPromotion"; } }
    }
}
