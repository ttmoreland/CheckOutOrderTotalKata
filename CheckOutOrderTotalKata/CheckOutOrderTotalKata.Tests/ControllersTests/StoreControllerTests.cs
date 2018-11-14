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
    }
}
