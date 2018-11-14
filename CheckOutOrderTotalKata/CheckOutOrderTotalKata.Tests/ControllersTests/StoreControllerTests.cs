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
        public void CartController_Get_ReturnOkResponse()
        {
            var okResult = _controller.Get();
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        #endregion
    }
}
