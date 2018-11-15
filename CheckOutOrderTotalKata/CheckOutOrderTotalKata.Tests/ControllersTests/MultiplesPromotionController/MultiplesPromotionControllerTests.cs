using CheckOutOrderTotalKata.Util;
using CheckOutOrderTotalKata.Models;
using CheckOutOrderTotalKata.Controllers;
using Microsoft.Extensions.Caching.Memory;
using CheckOutOrderTotalKata.ModelTests.ControllersTests;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;


namespace CheckOutOrderTotalKata.ControllersTests
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

        #region GET()
        [Fact]
        public void MultiplesPromotionController_Get_ReturnOkResponse()
        {
            var okResult = _controller.Get();
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void MultiplesPromotionController_Get_ReturnsCorrectNumberOfItems()
        {
            var okResult = _controller.Get().Result as OkObjectResult;
            var items = Assert.IsType<List<MultiplesPromotion>>(okResult.Value);
            Assert.Equal(2, items.Count);
        }
        #endregion

        #region GetItem()
        [Fact]
        public void MultiplesPromotionController_GetItem_ReturnNotFound()
        {
            var notFoundResult = _controller.Get("SomeItemThatDoesntExist");
            Assert.IsType<NotFoundResult>(notFoundResult.Result);
        }

        [Fact]
        public void MultiplesPromotionController_GetItem_ReturnsOkResult()
        {
            var okResult = _controller.Get("Apple");
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void MultiplesPromotionController_GetItem_ReturnsExpectedItem()
        {
            var okResult = _controller.Get("Apple").Result as OkObjectResult;
            var item = Assert.IsType<MultiplesPromotion>(okResult.Value);
        }
        #endregion

    }
}
