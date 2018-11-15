using CheckOutOrderTotalKata.Util;
using CheckOutOrderTotalKata.Models;
using CheckOutOrderTotalKata.Controllers;
using Microsoft.Extensions.Caching.Memory;
using CheckOutOrderTotalKata.ModelTests.ControllersTests;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

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

        #region Add()
        [Fact]
        public void MarkdownPromotionController_AddItem_ItemReturnsBadRequest()
        {
            var badItem = new MarkdownPromotion("", 12.00m);
            _controller.ModelState.AddModelError("Name", "Required");
            var badResponse = _controller.Post(badItem);
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        [Fact]
        public void MarkdownPromotionController_AddItem_ItemReturnsResponse()
        {
            var item = new MarkdownPromotion("Steak", -.50m);
            var createdResponse = _controller.Post(item);
            Assert.IsType<CreatedAtActionResult>(createdResponse);
        }

        [Fact]
        public void MarkdownPromotionController_AddItem_ItemReturnsResponseCreatedItem()
        {
            var item = new MarkdownPromotion("Steak", -.50m);
            var createdResponse = _controller.Post(item) as CreatedAtActionResult;
            var itemResult = createdResponse.Value as MarkdownPromotion;
            Assert.Equal(item.ItemName, itemResult.ItemName);
        }

        [Fact]
        public void MarkdownPromotionController_AddItem_DuplicateItemReturnsBadRequest()
        {
            var dupItem = new MarkdownPromotion("Soup", -.40m);
            var badResponse = _controller.Post(dupItem);
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        [Fact]
        public void MarkdownPromotionController_AddItem_ValidateAddsItem()
        {
            var item = new MarkdownPromotion("Steak", -.50m);
            var okResponse = _controller.Post(item);
            Assert.Equal(2, _markdowns.GetAllItems().Count());
        }
        #endregion
    }
}
