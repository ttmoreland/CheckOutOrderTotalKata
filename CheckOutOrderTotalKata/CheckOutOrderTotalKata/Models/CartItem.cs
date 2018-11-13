﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheckOutOrderTotalKata
{
    public class CartItem
    {
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public decimal Quantity { get; private set; }
        public decimal Extension { get { return Price * Quantity; } }

        public CartItem(string Name, decimal Quantity, decimal Price)
        {
            this.Name = Name;
            this.Price = Price;
            this.Quantity = Quantity;
        }
    }
}
