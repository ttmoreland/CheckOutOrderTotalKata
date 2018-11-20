using CheckOutOrderTotalKata.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CheckOutOrderTotalKata.Models
{
    public class StoreItem : BaseModel
    {
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public decimal Price { get; private set; }

        public StoreItem (string Name, decimal Price) : base(Name)
        {
            this.Price = Price;
        }
    }
}
