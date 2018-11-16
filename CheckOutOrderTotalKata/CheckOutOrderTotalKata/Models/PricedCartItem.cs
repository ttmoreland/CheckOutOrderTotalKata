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
    public class PricedCartItem
    {
        /// <summary>
        /// Gets the extension.
        /// </summary>
        /// <value>
        /// The extension.
        /// </value>
        public decimal Extension => Price * Quantity;

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        /// <value>
        /// The quantity.
        /// </value>
        public decimal Quantity { get; set; }

        /// <summary>
        /// Gets the price.
        /// </summary>
        /// <value>
        /// The price.
        /// </value>
        public decimal Price { get; set; }

        public PricedCartItem(string Name, decimal Quantity, decimal Price)
        {
            this.Name = Name;
            this.Quantity = Quantity;
            this.Price = Price;
        }

        public PricedCartItem()
        {
        }
    }
}
