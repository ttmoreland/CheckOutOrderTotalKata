using System.Collections.Generic;

namespace CheckOutOrderTotalKata.Models
{
    /// <summary>
    /// Cart 
    /// </summary>
    public class Cart
    {
        /// <summary>
        /// Gets the total.
        /// </summary>
        /// <value>
        /// The total.
        /// </value>
        public decimal Total { get; private set; }

        public List<PricedCartItem> PricedItems { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Cart"/> class.
        /// </summary>
        /// <param name="Total">The total.</param>
        public Cart(decimal Total)
        {
            this.Total = Total;
        }
    }
}
