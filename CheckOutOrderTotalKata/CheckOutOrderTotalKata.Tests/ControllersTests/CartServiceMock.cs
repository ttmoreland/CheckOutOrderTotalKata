using CheckOutOrderTotalKata.Models;
using CheckOutOrderTotalKata.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckOutOrderTotalKata.Tests.ControllersTests
{
    public class CartServiceMock : IBaseService<CartItem>
    {
        private readonly List<CartItem> _cart;

        public CartServiceMock()
        {
            _cart = new List<CartItem>()
            {
                new CartItem("Soup", 1.00m),
                new CartItem("Steak", 4.75m),
                new CartItem("Apple", 3.00m)
            };
        }

        public IEnumerable<CartItem> GetAllItems()
        {
            return _cart;
        }

        public CartItem Add(CartItem newItem)
        {
            _cart.Add(newItem);
            return newItem;
        }

        public CartItem GetItem(string itemName)
        {
            return _cart.Where(a => a.Name == itemName).FirstOrDefault();
        }

        public void Remove(string itemName)
        {
            var existing = this.GetItem(itemName);
            _cart.Remove(existing);
        }
    }
}
