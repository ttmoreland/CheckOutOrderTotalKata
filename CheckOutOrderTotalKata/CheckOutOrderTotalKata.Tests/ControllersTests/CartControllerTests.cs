using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using CheckOutOrderTotalKata;
using CheckOutOrderTotalKata.Controllers;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using CheckOutOrderTotalKata.Util;
using Microsoft.AspNetCore.Mvc;

namespace CheckOutOrderTotalKata.Tests.ControllersTests
{
    public class CartControllerTests
    {
        CartController _controller;
        ICartService _cart;

        public CartControllerTests()
        {
            _cart = new CartServiceMock();
            _controller = new CartController(_cart);
        }

        [Fact]
        public void CartController_Get_ReturnOkResponse()
        {
            var okResult = _controller.Get();
            Assert.IsType<OkObjectResult>(okResult.Result);
        }
    }
}
