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
            CartItem groceryItem = new CartItem("EachItem", 1m);
            Assert.Equal("EachItem", groceryItem.Name);
            Assert.Equal(1, groceryItem.Quantity);
        }

        [Fact]
        public void CartItem_TestWeightedItemProperties()
        {
            CartItem groceryItem = new CartItem("WeightedItem", 4.50m);
            Assert.Equal("WeightedItem", groceryItem.Name);
            Assert.Equal(4.50m, groceryItem.Quantity);
        }
    }
}
