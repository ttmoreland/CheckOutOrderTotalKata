using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheckOutOrderTotalKata.Models
{
    public class Cart
    {
        public decimal Total { get; private set; }

        public Cart(decimal Total)
        {
            this.Total = Total;
        }
    }
}
