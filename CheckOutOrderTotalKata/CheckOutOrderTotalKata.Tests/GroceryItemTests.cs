using System;
using Xunit;
using CheckOutOrderTotalKata;

namespace CheckOutOrderTotalKata.Tests
{
    public class GroceryItemTests
    {
        [Fact]
        public void TestGroceryEachItemProperties()
        {
            GroceryItem groceryItem = new GroceryItem("EachItem", 1, 12.00m);
            Assert.Equal("EachItem", groceryItem.Name);
            Assert.Equal(12.00m, groceryItem.Price);
        }

        [Fact]
        public void TestGroceryWeightedItemProperties()
        {
            GroceryItem groceryItem = new GroceryItem("WeightedItem", 4.50m, 2.99m);
            Assert.Equal("WeightedItem", groceryItem.Name);
            Assert.Equal(2.99m, groceryItem.Price);
        }
    }
}
