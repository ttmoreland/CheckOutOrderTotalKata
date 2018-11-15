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

        #region GET()
        [Fact]
        public void MarkdownPromotionController_Get_ReturnOkResponse()
        {
            var okResult = _controller.Get();
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void MarkdownPromotionController_Get_ReturnsCorrectNumberOfItems()
        {
            var okResult = _controller.Get().Result as OkObjectResult;
            var items = Assert.IsType<List<MarkdownPromotion>>(okResult.Value);
            Assert.Single(items);
        }
        #endregion

        #region GetItem()
        [Fact]
        public void MarkdownPromotionController_GetItem_ReturnNotFound()
        {
            var notFoundResult = _controller.Get("SomeItemThatDoesntExist");
            Assert.IsType<NotFoundResult>(notFoundResult.Result);
        }

        [Fact]
        public void MarkdownPromotionController_GetItem_ReturnsOkResult()
        {
            var okResult = _controller.Get("Soup");
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void MarkdownPromotionController_GetItem_ReturnsExpectedItem()
        {
            var okResult = _controller.Get("Soup").Result as OkObjectResult;
            var item = Assert.IsType<MarkdownPromotion>(okResult.Value);
        }
        #endregion

    }
}
