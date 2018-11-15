using CheckOutOrderTotalKata.Models;
using CheckOutOrderTotalKata.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckOutOrderTotalKata.ModelTests.ControllersTests
{
    public class StoreServiceMock : IBaseService<StoreItem>
    {
        private readonly List<StoreItem> _store;

        public StoreServiceMock()
        {
            _store = new List<StoreItem>()
            {
                new StoreItem("Soup", 1.00m),
                new StoreItem("Steak", 4.75m),
                new StoreItem("Apple", 3.00m),
                new StoreItem("Bread", 1.59m),
                new StoreItem("Chorizo", 3.99m)
            };
        }

        public StoreItem Add(StoreItem newItem)
        {
            _store.Add(newItem);
            return newItem;
        }

        public IEnumerable<StoreItem> GetAllItems()
        {
            return _store;
        }

        public StoreItem GetItem(string itemName)
        {
            return _store.Where(a => a.Name == itemName).FirstOrDefault();
        }

        public void Remove(string itemName)
        {
            var existing = this.GetItem(itemName);
            _store.Remove(existing);
        }
    }
}
