using CheckOutOrderTotalKata.Util;
using CheckOutOrderTotalKata.Models;
using CheckOutOrderTotalKata.Controllers;
using Microsoft.Extensions.Caching.Memory;
using CheckOutOrderTotalKata.ModelTests.ControllersTests;

namespace CheckOutOrderTotalKata.Tests.ControllersTests
{
    public class MultiplesPromotionControllerTests
    {
        MultiplesPromotionController _controller;
        BaseCacheService<StoreItem> _store;
        BaseCacheService<MultiplesPromotion> _multiples;

        public MultiplesPromotionControllerTests()
        {
            var cache = new MemoryCache(new MemoryCacheOptions());

            _store = new StoreServiceMock(cache);
            _multiples = new MultiplesPromotionServiceMock(cache);
            _controller = new MultiplesPromotionController(_multiples, _store);
        }
    }
}
