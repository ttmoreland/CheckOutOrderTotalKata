﻿using System;
using Xunit;
using CheckOutOrderTotalKata.Models;

namespace CheckOutOrderTotalKata.Tests
{
    public class MultiplesPromotionTests
    {
        [Fact]
        public void MultiplePromotionTests_TestProperties()
        {
            MultiplesPromotion promotion = new MultiplesPromotion("Soup", 3, 5.00m);
            Assert.Equal("Soup", promotion.ItemName);
            Assert.Equal(3, promotion.Quantity);
            Assert.Equal(5.00m, promotion.Price);
        }
    }
}
