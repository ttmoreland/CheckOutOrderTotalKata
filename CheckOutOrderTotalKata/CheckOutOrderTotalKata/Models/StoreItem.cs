using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CheckOutOrderTotalKata.Models
{
    public class StoreItem
    {
        [Required]
        public string Name { get; private set; }
        public decimal Price { get; private set; }

        public StoreItem (string Name, decimal Price)
        {
            this.Name = Name;
            this.Price = Price;
        }
    }
}
