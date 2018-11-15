using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CheckOutOrderTotalKata.Models
{
    public class MultiplesPromotion
    {
        [Required]
        public string ItemName { get; private set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}.")]
        public int Quantity { get; private set; }

        [Range(.01, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}.")]
        public decimal Price { get; private set; }

        public MultiplesPromotion(string ItemName, int Quantity, decimal Price)
        {
            this.ItemName = ItemName;
            this.Price = Price;
            this.Quantity = Quantity;
        }
    }
}
