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
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public decimal Quantity { get; private set; }

        public CartItem(string Name, decimal Quantity)
        {
            this.Name = Name;      
            if (Quantity == 0)
                //Each
                this.Quantity = 1;
            else
                //Weight
                this.Quantity = Quantity;
        }
    }
}
