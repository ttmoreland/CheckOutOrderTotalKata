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

        #region Add()
        [Fact]
        public void MultiplesPromotionController_AddItem_ItemReturnsBadRequest()
        {
            var badItem = new MultiplesPromotion("", 3, 7.50m);
            _controller.ModelState.AddModelError("Name", "Required");
            var badResponse = _controller.Post(badItem);
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        [Fact]
        public void MultiplesPromotionController_AddItem_ItemReturnsResponse()
        {
            var item = new MultiplesPromotion("Bread", 2, 3.00m);
            var createdResponse = _controller.Post(item);
            Assert.IsType<CreatedAtActionResult>(createdResponse);
        }

        [Fact]
        public void MultiplesPromotionController_AddItem_ItemReturnsResponseCreatedItem()
        {
            var item = new MultiplesPromotion("Bread", 2, 3.00m);
            var createdResponse = _controller.Post(item) as CreatedAtActionResult;
            var itemResult = createdResponse.Value as MultiplesPromotion;
            Assert.Equal(item.Name, itemResult.Name);
        }

        [Fact]
        public void MultiplesPromotionController_AddItem_DuplicateItemReturnsBadRequest()
        {
            var dupItem = new MultiplesPromotion("Apple", 2, 3.00m);
            var badResponse = _controller.Post(dupItem);
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        [Fact]
        public void MultiplesPromotionController_AddItem_ValidateAddsItem()
        {
            var item = new MultiplesPromotion("Bread", 2, 3.00m);
            var okResponse = _controller.Post(item);
            Assert.Equal(3, _multiples.GetAllItems().Count());
        }

        [Fact]
        public void MultiplesPromotionController_AddItem_ItemDoesntExistInStore()
        {
            var badItem = new MultiplesPromotion("SomeItemThatDoesntExistInStore", 2, 3.00m);
            var badResponse = _controller.Post(badItem);
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }
        #endregion

        #region Remove()
        [Fact]
        public void MarkdownPromotionController_Delete_ReturnsNotFound()
        {
            var fakeitem = "SomeFakeitem";
            var badResponse = _controller.Remove(fakeitem);
            Assert.IsType<NotFoundResult>(badResponse);
        }

        [Fact]
        public void MarkdownPromotionController_Delete_ReturnsOkResult()
        {
            var item = "Apple";
            var okResponse = _controller.Remove(item);
            Assert.IsType<OkResult>(okResponse);
        }

        [Fact]
        public void MarkdownPromotionController_Delete_ValidateRemovesItem()
        {
            var item = "Apple";
            var okResponse = _controller.Remove(item);
            Assert.Single(_multiples.GetAllItems());
        }
        #endregion
    }
}
