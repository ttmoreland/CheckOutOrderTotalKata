using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CheckOutOrderTotalKata.Models
{
    public class GroceryItem
    {
        [Required]
        public string Name { get; private set; }
        public decimal Price { get; private set; }

        public GroceryItem (string Name, decimal Price)
        {
            this.Name = Name;
            this.Price = Price;
        }
    }
}
