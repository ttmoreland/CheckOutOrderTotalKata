using System.Collections.Generic;
using Xunit;
using CheckOutOrderTotalKata.Controllers;
using CheckOutOrderTotalKata.Util;
using Microsoft.AspNetCore.Mvc;
using CheckOutOrderTotalKata.Models;
using System.Linq;
using Microsoft.Extensions.Caching.Memory;

namespace CheckOutOrderTotalKata.ModelTests.ControllersTests
{
    public class CartControllerTests
    {
        CartController _controller;
        BaseCacheService<CartItem> _cart;
        BaseCacheService<StoreItem> _store;

        public CartControllerTests()
        {
            var cache = new MemoryCache(new MemoryCacheOptions());

            _cart = new CartServiceMock(cache);
            _store = new StoreServiceMock(cache);
            _controller = new CartController(_cart, _store);
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

        #region GetcartTotal
        [Fact]
        public void CartController_GetCartTotal_ReturnsCorrectType()
        {
            var createdResponse = _controller.GetCartTotal();
            Assert.IsType<OkObjectResult>(createdResponse.Result);
        }

        [Fact]
        public void CartController_GetCartTotal_ReturnsCorrectTotal()
        {
            var response = _controller.GetCartTotal().Result as OkObjectResult;
            var cartResult = response.Value as Cart;
            Assert.Equal(32.5625m, cartResult.Total);
        }

        [Fact]
        public void CartController_GetCartTotal_ReturnsBadResponse()
        {
            //clear out cart
            _cart.GetAllItems().ToList().ForEach(x => _cart.Remove(x));

            var badResponse = _controller.GetCartTotal();
            Assert.IsType<BadRequestObjectResult>(badResponse.Result);
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
            var item = new CartItem("Apple", 0m);
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
            var item = new CartItem("Apple", 0m);
            var createdResponse = _controller.Post(item) as CreatedAtActionResult;
            var itemResult = createdResponse.Value as CartItem;
            Assert.Equal(item.Name, itemResult.Name);
        }

        [Fact]
        public void CartController_AddItem_ItemDoesntExistInStore()
        {
            var badItem = new CartItem("SomeItemThatDoesntExistInStore", 0);
            var badResponse = _controller.Post(badItem);
            Assert.IsType<BadRequestObjectResult>(badResponse);
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
