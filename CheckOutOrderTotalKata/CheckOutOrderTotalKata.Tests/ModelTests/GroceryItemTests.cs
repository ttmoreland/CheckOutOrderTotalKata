using System;
using Xunit;
using CheckOutOrderTotalKata.Models;

namespace CheckOutOrderTotalKata.Tests
{
    public class GroceryItemTests
    {
        [Fact]
        public void GroceryItem_TestEachItemProperties()
        {
            GroceryItem groceryItem = new GroceryItem("EachItem", 12.00m);
            Assert.Equal("EachItem", groceryItem.Name);
            Assert.Equal(12.00m, groceryItem.Price);
        }

        [Fact]
        public void GroceryItem_TestWeightedItemProperties()
        {
            GroceryItem groceryItem = new GroceryItem("WeightedItem", 2.99m);
            Assert.Equal("WeightedItem", groceryItem.Name);
            Assert.Equal(2.99m, groceryItem.Price);
        }
    }
}
