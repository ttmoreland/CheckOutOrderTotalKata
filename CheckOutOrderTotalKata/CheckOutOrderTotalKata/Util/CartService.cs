using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CheckOutOrderTotalKata.Models;
using Microsoft.Extensions.Caching.Memory;

namespace CheckOutOrderTotalKata.Util
{
    public class CartService : ICartService
    {
        private readonly List<CartItem> _cart;
        private readonly IMemoryCache _cache;
        public CartService(IMemoryCache cache)
        {
            _cart = cache.GetCachedCart();
            _cache = cache;
        }

        public IEnumerable<CartItem> GetAllItems()
        {
            return _cart;
        }

        public CartItem Add(CartItem newItem)
        {
            throw new NotImplementedException();
        }

        public decimal GetCartTotal()
        {
            throw new NotImplementedException();
        }

        public CartItem GetItem(string itemName)
        {
            return _cart.Where(a => a.Name == itemName).FirstOrDefault();
        }

        public void Remove(string itemName)
        {
            throw new NotImplementedException();
        }
    }
}
