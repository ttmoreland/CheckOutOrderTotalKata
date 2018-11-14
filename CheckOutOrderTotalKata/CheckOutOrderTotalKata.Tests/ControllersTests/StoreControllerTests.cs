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
    }
}
