using CheckOutOrderTotalKata.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CheckOutOrderTotalKata.Models
{
    public class MarkdownPromotion : BaseModel
    {
        [Range(int.MinValue, -.01, ErrorMessage = "Please enter a negative discount.")]
        public decimal Discount { get; private set; }

        public MarkdownPromotion(string Name, decimal Discount) : base(Name)
        {
            this.Discount = Discount;
        }
    }
}
