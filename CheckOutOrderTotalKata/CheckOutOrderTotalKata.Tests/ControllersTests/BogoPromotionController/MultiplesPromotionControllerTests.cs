using CheckOutOrderTotalKata.Util;
using CheckOutOrderTotalKata.Models;
using CheckOutOrderTotalKata.Controllers;
using Microsoft.Extensions.Caching.Memory;
using CheckOutOrderTotalKata.ModelTests.ControllersTests;

namespace CheckOutOrderTotalKata.Tests.ControllersTests
{
    public class BogoPromotionControllerTests
    {
        BogoPromotionController _controller;
        BaseCacheService<StoreItem> _store;
        BaseCacheService<BogoPromotion> _bogos;

        public BogoPromotionControllerTests()
        {
            var cache = new MemoryCache(new MemoryCacheOptions());

            _store = new StoreServiceMock(cache);
            _bogos = new BogoPromotionServiceMock(cache);
            _controller = new BogoPromotionController(_bogos, _store);
        }
    }
}
