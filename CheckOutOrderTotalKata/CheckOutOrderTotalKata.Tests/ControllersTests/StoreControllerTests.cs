using System.Collections.Generic;
using Xunit;
using CheckOutOrderTotalKata.Controllers;
using CheckOutOrderTotalKata.Util;
using Microsoft.AspNetCore.Mvc;
using CheckOutOrderTotalKata.Models;
using System.Linq;


namespace CheckOutOrderTotalKata.Tests.ControllersTests
{
    public class StoreControllerTests
    {
        StoreController _controller;
        IBaseService<StoreItem> _store;

        public StoreControllerTests()
        {
            _store = new StoreServiceMock();
            _controller = new StoreController(_store);
        }

        #region GET()
        [Fact]
        public void StoreController_Get_ReturnOkResponse()
        {
            var okResult = _controller.Get();
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void StoreController_Get_ReturnsCorrectNumberOfItems()
        {
            var okResult = _controller.Get().Result as OkObjectResult;
            var cartItems = Assert.IsType<List<StoreItem>>(okResult.Value);
            Assert.Equal(4, cartItems.Count);
        }
        #endregion

        #region GetItem()
        [Fact]
        public void StoreController_GetItem_ReturnNotFound()
        {
            var notFoundResult = _controller.Get("SomeItemThatDoesntExist");
            Assert.IsType<NotFoundResult>(notFoundResult.Result);
        }

        [Fact]
        public void StoreController_GetItem_ReturnsOkResult()
        {
            var okResult = _controller.Get("Soup");
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void StoreController_GetItem_ReturnsExpectedItem()
        {
            var okResult = _controller.Get("Steak").Result as OkObjectResult;
            var item = Assert.IsType<StoreItem>(okResult.Value);
        }
        #endregion

        #region Add()
        [Fact]
        public void StoreController_AddItem_WeightedItemReturnsBadRequest()
        {
            var badItem = new StoreItem("", 12.00m);
            _controller.ModelState.AddModelError("Name", "Required");
            var badResponse = _controller.Post(badItem);
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        [Fact]
        public void StoreController_AddItem_WeightedItemReturnsResponse()
        {
            var item = new StoreItem("Chorizo", 3.99m);
            var createdResponse = _controller.Post(item);
            Assert.IsType<CreatedAtActionResult>(createdResponse);
        }

        [Fact]
        public void StoreController_AddItem_WeightedItemReturnsResponseCreatedItem()
        {
            var item = new StoreItem("Ground Beef", 2.99m);
            var createdResponse = _controller.Post(item) as CreatedAtActionResult;
            var itemResult = createdResponse.Value as StoreItem;
            Assert.Equal(item.Name, itemResult.Name);
        }
        #endregion
    }
}
