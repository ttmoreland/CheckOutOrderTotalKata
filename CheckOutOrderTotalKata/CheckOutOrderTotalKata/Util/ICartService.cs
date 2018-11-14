using CheckOutOrderTotalKata.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheckOutOrderTotalKata.Util
{
    public interface ICartService
    {
        IEnumerable<CartItem> GetAllItems();
        CartItem Add(CartItem newItem);
        CartItem GetItem(string itemName);
        void Remove(string itemName);
        decimal GetCartTotal();
    }
}
