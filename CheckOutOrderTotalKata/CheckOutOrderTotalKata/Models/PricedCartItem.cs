using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheckOutOrderTotalKata.Models
{
    public class PricedCartItem : CartItem
    {
        public decimal Price { get; private set; }

        public PricedCartItem(string Name, decimal Quantity, decimal Price) : base(Name, Quantity)
        {
            this.Price = Price;
        }
    }
}
