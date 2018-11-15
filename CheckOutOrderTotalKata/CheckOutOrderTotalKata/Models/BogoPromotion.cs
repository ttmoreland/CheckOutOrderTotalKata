using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CheckOutOrderTotalKata.Models
{
    public class BogoPromotion
    {
        [Required]
        public string ItemName { get; private set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}.")]
        public int QuantityThreshold { get; private set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}.")]
        public int QuantityImpacted { get; private set; }

        [Range(0, 100, ErrorMessage = "Please enter a value between 1 and 100.")]
        public int PercentOff { get; private set; }

        [Range(0, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}.")]
        public int QuantityLimit { get; private set; }

        public BogoPromotion(string ItemName, int QuantityThreshold, int QuantityImpacted, int PercentOff, int QuantityLimit)
        {
            this.ItemName = ItemName;
            this.QuantityThreshold = QuantityThreshold;
            this.QuantityImpacted = QuantityImpacted;
            this.PercentOff = PercentOff;
            this.QuantityLimit = QuantityLimit;
        }
    }
}
