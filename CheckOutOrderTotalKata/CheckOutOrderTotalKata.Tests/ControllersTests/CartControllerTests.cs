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
using CheckOutOrderTotalKata.Models;

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

        [Fact]
        public void CartController_Get_ReturnsCorrectNumberOfItems()
        {
            var okResult = _controller.Get().Result as OkObjectResult;
            var cartItems = Assert.IsType<List<CartItem>>(okResult.Value);
            Assert.Equal(3, cartItems.Count);
        }

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
        
    }
}
