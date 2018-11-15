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
        public string Name { get; private set; }

        [Range(int.MinValue, -.01, ErrorMessage = "Please enter a negative discount.")]
        public decimal Discount { get; private set; }

        public MarkdownPromotion(string Name, decimal Discount)
        {
            this.Name = Name;
            this.Discount = Discount;
        }
    }
}
