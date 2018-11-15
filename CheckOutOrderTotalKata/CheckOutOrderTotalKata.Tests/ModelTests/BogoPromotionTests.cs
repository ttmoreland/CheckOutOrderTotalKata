using System;
using Xunit;
using CheckOutOrderTotalKata.Models;

namespace CheckOutOrderTotalKata.ModelTests
{
    public class BogoPromotionTests
    {
        [Fact]
        public void BogoPromotionTests_TestProperties()
        {
            BogoPromotion promotion = new BogoPromotion("Soup", 2, 1, 100, 0);
            Assert.Equal("Soup", promotion.Name);
            Assert.Equal(2, promotion.QuantityThreshold);
            Assert.Equal(1, promotion.QuantityImpacted);
            Assert.Equal(100, promotion.PercentOff);
            Assert.Equal(0, promotion.QuantityLimit);
        }
    }
}
