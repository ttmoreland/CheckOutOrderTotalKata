using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheckOutOrderTotalKata.Models
{
    /// <summary>
    /// Priced Cart Item
    /// </summary>
    /// <seealso cref="CheckOutOrderTotalKata.Models.CartItem" />
    public class PricedCartItem : CartItem
    {
        /// <summary>
        /// Gets the extension.
        /// </summary>
        /// <value>
        /// The extension.
        /// </value>
        public decimal Extension => Price * Quantity;

        /// <summary>
        /// Gets the price.
        /// </summary>
        /// <value>
        /// The price.
        /// </value>
        public decimal Price { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PricedCartItem"/> class.
        /// </summary>
        /// <param name="Name">The name.</param>
        /// <param name="Quantity">The quantity.</param>
        /// <param name="Price">The price.</param>
        public PricedCartItem(string Name, decimal Quantity, decimal Price) : base(Name, Quantity)
        {
            this.Price = Price;
        }
    }
}
