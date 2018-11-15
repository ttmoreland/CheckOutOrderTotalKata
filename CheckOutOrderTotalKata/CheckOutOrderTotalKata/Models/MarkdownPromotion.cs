using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CheckOutOrderTotalKata.Models
{
    public class MarkdownPromotion
    {
        [Required]
        public string ItemName { get; private set; }

        [Range(-.01, int.MinValue, ErrorMessage = "Please enter a negative discount.")]
        public decimal Discount { get; private set; }

        public MarkdownPromotion(string ItemName, decimal Discount)
        {
            this.ItemName = ItemName;
            this.Discount = Discount;
        }
    }
}
