﻿using System.Collections.Generic;
using Xunit;
using CheckOutOrderTotalKata.Controllers;
using CheckOutOrderTotalKata.Util;
using Microsoft.AspNetCore.Mvc;
using CheckOutOrderTotalKata.Models;
using System.Linq;
using Microsoft.Extensions.Caching.Memory;
using CheckOutOrderTotalKata.ModelTests.ControllersTests;
namespace CheckOutOrderTotalKata.ModelTests
{
    public class CartTests
    {
        BaseCacheService<CartItem> _cart;
        BaseCacheService<StoreItem> _store;
        readonly BaseCacheService<MarkdownPromotion> _markdowns;
        readonly BaseCacheService<BogoPromotion> _bogos;
        readonly BaseCacheService<MultiplesPromotion> _multiples;

        public CartTests()
        {
            var cache = new MemoryCache(new MemoryCacheOptions());
            _cart = new CartServiceMock(cache);
            _store = new StoreServiceMock(cache);
            _markdowns = new MarkdownPromotionServiceMock(cache);
            _bogos = new BogoPromotionServiceMock(cache);
            _multiples = new MultiplesPromotionServiceMock(cache);
        }

        [Fact]
        public void Cart_TestProperties()
        {
            Cart cart = new Cart();
            cart.AddPricedItems(_cart.GetAllItems(), _store.GetAllItems());
            Assert.Equal(32.5625m, cart.Total);
        }


        [Fact]
        public void Cart_TestPromotion_NoPromotions()
        {
            Cart cart = new Cart();
            cart.AddPricedItems(_cart.GetAllItems(), _store.GetAllItems());
            cart.ApplyPromotions(null, null, null);
            Assert.Equal(32.5625m, cart.Total);
        }

        [Fact]
        public void Cart_TestMarkdownPromotion_ValidPromotion()
        {
            Cart cart = new Cart();
            cart.AddPricedItems(_cart.GetAllItems(), _store.GetAllItems());
            cart.ApplyPromotions(_markdowns.GetAllItems(), null, null);
            Assert.Equal(32.3625m, cart.Total);
        }

        [Fact]
        public void Cart_TestMarkdownPromotion_HigherThanItemPrice()
        {
            Cart cart = new Cart();
            cart.AddPricedItems(_cart.GetAllItems(), _store.GetAllItems());
            List<MarkdownPromotion> markDowns = new List<MarkdownPromotion> { new MarkdownPromotion("Soup", -1.01m) };
            
            cart.ApplyPromotions(markDowns, null, null);
            Assert.Equal(32.5625m, cart.Total);
        }

        [Fact]
        public void Cart_TestMultiplesPromotion_Remainder()
        {
            Cart cart = new Cart();
            cart.AddPricedItems(_cart.GetAllItems(), _store.GetAllItems());
            cart.ApplyPromotions(null, _multiples.GetAllItems(), null);
            Assert.Equal(31.5625m, cart.Total);
        }

        [Fact]
        public void Cart_TestMultiplesPromotion_NoRemainder()
        {
            Cart cart = new Cart();
            cart.AddPricedItems(_cart.GetAllItems(), _store.GetAllItems());
            List<MultiplesPromotion> multiples = new List<MultiplesPromotion> { new MultiplesPromotion("Apple", 3, 3) };
            cart.ApplyPromotions(null, multiples, null);
            Assert.Equal(26.5625m, cart.Total);
        }

        [Fact]
        public void Cart_TestMultiplesPromotion_DidntMeetQuantityThreshold()
        {
            Cart cart = new Cart();
            cart.AddPricedItems(_cart.GetAllItems(), _store.GetAllItems());
            List<MultiplesPromotion> multiples = new List<MultiplesPromotion> { new MultiplesPromotion("Apple", 5, 3) };
            cart.ApplyPromotions(null, multiples, null);
            Assert.Equal(32.5625m, cart.Total);

        }
    }
}
