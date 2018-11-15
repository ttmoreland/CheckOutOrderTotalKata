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

        #region GET()
        [Fact]
        public void BogoPromotionController_Get_ReturnOkResponse()
        {
            var okResult = _controller.Get();
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void BogoPromotionController_Get_ReturnsCorrectNumberOfItems()
        {
            var okResult = _controller.Get().Result as OkObjectResult;
            var items = Assert.IsType<List<BogoPromotion>>(okResult.Value);
            Assert.Single(items);
        }
        #endregion

        #region GetItem()
        [Fact]
        public void BogoPromotionController_GetItem_ReturnNotFound()
        {
            var notFoundResult = _controller.Get("SomeItemThatDoesntExist");
            Assert.IsType<NotFoundResult>(notFoundResult.Result);
        }

        [Fact]
        public void BogoPromotionController_GetItem_ReturnsOkResult()
        {
            var okResult = _controller.Get("Steak");
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void BogoPromotionController_GetItem_ReturnsExpectedItem()
        {
            var okResult = _controller.Get("Steak").Result as OkObjectResult;
            var item = Assert.IsType<BogoPromotion>(okResult.Value);
        }
        #endregion

        #region Add()
        [Fact]
        public void BogoPromotionController_AddItem_ItemReturnsBadRequest()
        {
            var badItem = new BogoPromotion("", 1, 1, 100, 1);
            _controller.ModelState.AddModelError("Name", "Required");
            var badResponse = _controller.Post(badItem);
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        [Fact]
        public void BogoPromotionController_AddItem_ItemReturnsResponse()
        {
            var item = new BogoPromotion("Chorizo", 2, 1, 50, 3);
            var createdResponse = _controller.Post(item);
            Assert.IsType<CreatedAtActionResult>(createdResponse);
        }

        [Fact]
        public void BogoPromotionController_AddItem_ItemReturnsResponseCreatedItem()
        {
            var item = new BogoPromotion("Chorizo", 2, 1, 50, 3);
            var createdResponse = _controller.Post(item) as CreatedAtActionResult;
            var itemResult = createdResponse.Value as BogoPromotion;
            Assert.Equal(item.ItemName, itemResult.ItemName);
        }

        [Fact]
        public void BogoPromotionController_AddItem_DuplicateItemReturnsBadRequest()
        {
            var dupItem = new BogoPromotion("Steak", 2, 1, 50, 3);
            var badResponse = _controller.Post(dupItem);
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        [Fact]
        public void BogoPromotionController_AddItem_ValidateAddsItem()
        {
            var item = new BogoPromotion("Chorizo", 2, 1, 50, 3);
            var okResponse = _controller.Post(item);
            Assert.Equal(2, _bogos.GetAllItems().Count());
        }
        #endregion

    }
}
