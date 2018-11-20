using CheckOutOrderTotalKata.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CheckOutOrderTotalKata.Models
{
    /// <summary>
    /// Cart Item
    /// </summary>
    public class CartItem : BaseModel
    {
        /// <summary>
        /// Gets the quantity.
        /// </summary>
        /// <value>
        /// The quantity.
        /// </value>
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}.")]
        public decimal Quantity { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CartItem"/> class.
        /// </summary>
        /// <param name="Name">The name.</param>
        /// <param name="Quantity">The quantity.</param>
        public CartItem(string Name, decimal Quantity) : base(Name)
        {
            if (Quantity == 0)
            {
                //Each
                this.Quantity = 1;
            }
            else
            {
                //Weight
                this.Quantity = Quantity;
            }
        }
    }
}
