using System;
using Xunit;
using CheckOutOrderTotalKata.Models;

namespace CheckOutOrderTotalKata.ModelTests
{
    public class PricedCartItemTests
    {
        [Fact]
        public void PricedCartItem_TestProperties()
        {
            PricedCartItem cartItem = new PricedCartItem("Soup", 12.00m, 12.00m);
            Assert.Equal(144m, cartItem.Extension);
        }
    }
}
