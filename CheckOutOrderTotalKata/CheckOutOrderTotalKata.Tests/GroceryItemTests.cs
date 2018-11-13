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
            Assert.Equal(1, groceryItem.Quantity);
            Assert.Equal(12.00m, groceryItem.Price);
            Assert.Equal(12.00m, groceryItem.Extension);
        }

        [Fact]
        public void TestGroceryWeightedItemProperties()
        {
            GroceryItem groceryItem = new GroceryItem("WeightedItem", 4.50m, 2.99m);
            Assert.Equal("WeightedItem", groceryItem.Name);
            Assert.Equal(4.50m, groceryItem.Quantity);
            Assert.Equal(2.99m, groceryItem.Price);
            Assert.Equal(13.455m, groceryItem.Extension);
        }
    }
}
