using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheckOutOrderTotalKata.Models
{
    public class GroceryStore
    {
        public readonly List<GroceryItem> GroceryItems;
        public readonly List<CartItem> CartItems;

        public GroceryStore()
        {
            GroceryItems = new List<GroceryItem>();
            CartItems = new List<CartItem>();
        }
    }
}
