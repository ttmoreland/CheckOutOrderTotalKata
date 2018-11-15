using System;
using Xunit;
using CheckOutOrderTotalKata.Models;

namespace CheckOutOrderTotalKata.ModelTests
{
    public class MarkdownPromotionTests
    {
        [Fact]
        public void MarkdownPromotionTests_TestProperties()
        {
            MarkdownPromotion promotion = new MarkdownPromotion("Soup", -.20m);
            Assert.Equal("Soup", promotion.ItemName);
            Assert.Equal(-.20m, promotion.Discount);
        }
    }
}
