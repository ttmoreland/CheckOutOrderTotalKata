using System;
using Xunit;
using CheckOutOrderTotalKata.Models;

namespace CheckOutOrderTotalKata.Tests
{
    public class CartItemTests
    {
        [Fact]
        public void CartItem_TestEachItemProperties()
        {
            CartItem groceryItem = new CartItem("EachItem", 1, 12.00m);
            Assert.Equal("EachItem", groceryItem.Name);
            Assert.Equal(1, groceryItem.Quantity);
            Assert.Equal(12.00m, groceryItem.Price);
            Assert.Equal(12.00m, groceryItem.Extension);
        }

        [Fact]
        public void CartItem_TestWeightedItemProperties()
        {
            CartItem groceryItem = new CartItem("WeightedItem", 4.50m, 2.99m);
            Assert.Equal("WeightedItem", groceryItem.Name);
            Assert.Equal(4.50m, groceryItem.Quantity);
            Assert.Equal(2.99m, groceryItem.Price);
            Assert.Equal(13.455m, groceryItem.Extension);
        }
    }
}
