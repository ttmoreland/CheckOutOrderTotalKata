using System;
using Xunit;
using CheckOutOrderTotalKata.Models;

namespace CheckOutOrderTotalKata.Tests
{
    public class CartTests
    {
        [Fact]
        public void Cart_TestProperties()
        {
            Cart cart = new Cart(100m);
            Assert.Equal(100m, cart.Total);
        }
    }
}
