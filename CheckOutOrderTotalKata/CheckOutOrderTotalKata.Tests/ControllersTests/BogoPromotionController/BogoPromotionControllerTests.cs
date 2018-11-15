﻿using CheckOutOrderTotalKata.Util;
using CheckOutOrderTotalKata.Models;
using CheckOutOrderTotalKata.Controllers;
using Microsoft.Extensions.Caching.Memory;
using CheckOutOrderTotalKata.ModelTests.ControllersTests;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CheckOutOrderTotalKata.Tests.ControllersTests
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
    }
}
