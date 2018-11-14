using System;
using Xunit;
using CheckOutOrderTotalKata.Models;

namespace CheckOutOrderTotalKata.Tests
{
    public class StoreItemTests
    {
        [Fact]
        public void StoreItem_TestEachItemProperties()
        {
            StoreItem storeItem = new StoreItem("EachItem", 12.00m);
            Assert.Equal("EachItem", storeItem.Name);
            Assert.Equal(12.00m, storeItem.Price);
        }

        [Fact]
        public void StoreItem_TestWeightedItemProperties()
        {
            StoreItem storeItem = new StoreItem("WeightedItem", 2.99m);
            Assert.Equal("WeightedItem", storeItem.Name);
            Assert.Equal(2.99m, storeItem.Price);
        }
    }
}
