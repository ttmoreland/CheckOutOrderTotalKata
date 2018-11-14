using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CheckOutOrderTotalKata.Models
{
    public class CartItem
    {     
        [Required]
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public decimal Quantity { get; private set; }
        public decimal Extension { get { return Price * Quantity; } }

        public CartItem(string Name, decimal Quantity, decimal Price)
        {
            this.Name = Name;
            this.Price = Price;       
            if (Quantity == 0)
                //Each
                this.Quantity = Quantity;
            else
                //Weight
                this.Quantity = Quantity;
        }
    }
}
