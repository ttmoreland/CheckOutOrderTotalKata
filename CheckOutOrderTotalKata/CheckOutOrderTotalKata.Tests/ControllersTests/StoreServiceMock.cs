using CheckOutOrderTotalKata.Models;
using CheckOutOrderTotalKata.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckOutOrderTotalKata.Tests.ControllersTests
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
                new StoreItem("Bread", 3.00m)
            };
        }

        public StoreItem Add(StoreItem newItem)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<StoreItem> GetAllItems()
        {
            throw new NotImplementedException();
        }

        public StoreItem GetItem(string itemName)
        {
            throw new NotImplementedException();
        }

        public void Remove(string itemName)
        {
            throw new NotImplementedException();
        }
    }
}
