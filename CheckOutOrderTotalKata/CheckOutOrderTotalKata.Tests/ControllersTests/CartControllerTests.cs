using System.Collections.Generic;
using Xunit;
using CheckOutOrderTotalKata.Controllers;
using CheckOutOrderTotalKata.Util;
using Microsoft.AspNetCore.Mvc;
using CheckOutOrderTotalKata.Models;
using System.Linq;

namespace CheckOutOrderTotalKata.Tests.ControllersTests
{
    public class CartControllerTests
    {
        CartController _controller;
        IBaseService<CartItem> _cart;

        public CartControllerTests()
        {
            _cart = new CartServiceMock();
            _controller = new CartController(_cart);
        }

        #region Get()
        [Fact]
        public void CartController_Get_ReturnOkResponse()
        {
            var okResult = _controller.Get();
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void CartController_Get_ReturnsCorrectNumberOfItems()
        {
            var okResult = _controller.Get().Result as OkObjectResult;
            var cartItems = Assert.IsType<List<CartItem>>(okResult.Value);
            Assert.Equal(3, cartItems.Count);
        }
        #endregion

        #region Getitem()
        [Fact]
        public void CartController_GetItem_ReturnNotFound()
        {
            var notFoundResult = _controller.Get("SomeItemThatDoesntExist");
            Assert.IsType<NotFoundResult>(notFoundResult.Result);
        }

        [Fact]
        public void CartController_GetItem_ReturnsOkResult()
        {
            var okResult = _controller.Get("Soup");
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void CartController_GetItem_ReturnsExpectedItem()
        {
            var okResult = _controller.Get("Steak").Result as OkObjectResult;
            var item = Assert.IsType<CartItem>(okResult.Value);
        }
        #endregion

        #region Add()
        [Fact]
        public void CartController_AddItem_WeightedItemReturnsBadRequest()
        {
            var badItem = new CartItem("", 12);
            _controller.ModelState.AddModelError("Name", "Required");
            var badResponse = _controller.Post(badItem);
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        [Fact]
        public void CartController_AddItem_EachItemReturnsBadRequest()
        {
            var badItem = new CartItem("", 0);
            _controller.ModelState.AddModelError("Name", "Required");
            var badResponse = _controller.Post(badItem);
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        [Fact]
        public void CartController_AddItem_WeightedItemReturnsResponse()
        {
            var item = new CartItem("Chorizo", 1.25m);
            var createdResponse = _controller.Post(item);
            Assert.IsType<CreatedAtActionResult>(createdResponse);
        }

        [Fact]
        public void CartController_AddItem_EachItemReturnsResponse()
        {
            var item = new CartItem("Beer 12 Pack", 0m);
            var createdResponse = _controller.Post(item);
            Assert.IsType<CreatedAtActionResult>(createdResponse);
        }

        [Fact]
        public void CartController_AddItem_WeightedItemReturnsResponseCreatedItem()
        {
            var item = new CartItem("Chorizo", 1.25m);
            var createdResponse = _controller.Post(item) as CreatedAtActionResult;
            var itemResult = createdResponse.Value as CartItem;
            Assert.Equal(item.Name, itemResult.Name);
        }

        [Fact]
        public void CartController_AddItem_EachItemReturnsResponseCreatedItem()
        {
            var item = new CartItem("Beer 12 Pack", 0m);
            var createdResponse = _controller.Post(item) as CreatedAtActionResult;
            var itemResult = createdResponse.Value as CartItem;
            Assert.Equal(item.Name, itemResult.Name);
        }


        #endregion

        #region Remove()
        [Fact]
        public void CartController_Delete_ReturnsNotFound()
        {
            var fakeitem = "SomeFakeitem";
            var badResponse = _controller.Remove(fakeitem);
            Assert.IsType<NotFoundResult>(badResponse);
        }

        [Fact]
        public void CartController_Delete_ReturnsOkResult()
        {
            var item = "Soup";
            var okResponse = _controller.Remove(item);
            Assert.IsType<OkResult>(okResponse);
        }

        [Fact]
        public void CartController_Delete_ValidateRemovesItem()
        {
            var item = "Soup";
            var okResponse = _controller.Remove(item);
            Assert.Equal(2, _cart.GetAllItems().Count());
        }
        #endregion
    }
}
