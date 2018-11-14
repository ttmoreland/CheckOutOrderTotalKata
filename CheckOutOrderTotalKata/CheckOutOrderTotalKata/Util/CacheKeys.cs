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
        public static string GroceryItem { get { return "_GroceryItems"; } }
    }
}
