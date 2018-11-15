using CheckOutOrderTotalKata.Util;
using CheckOutOrderTotalKata.Models;
using CheckOutOrderTotalKata.Controllers;
using Microsoft.Extensions.Caching.Memory;
using CheckOutOrderTotalKata.ModelTests.ControllersTests;

namespace CheckOutOrderTotalKata.Tests.ControllersTests
{
    public class MarkdownPromotionControllerTests
    {
        MarkdownPromotionController _controller;
        BaseCacheService<StoreItem> _store;
        BaseCacheService<MarkdownPromotion> _markdowns;

        public MarkdownPromotionControllerTests()
        {
            var cache = new MemoryCache(new MemoryCacheOptions());

            _store = new StoreServiceMock(cache);
            _markdowns = new MarkdownPromotionServiceMock(cache);
            _controller = new MarkdownPromotionController(_markdowns, _store);
        }
    }
}
