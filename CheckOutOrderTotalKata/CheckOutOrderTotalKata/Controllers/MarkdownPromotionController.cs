﻿using System.Collections.Generic;
using CheckOutOrderTotalKata.Models;
using CheckOutOrderTotalKata.Util;
using Microsoft.AspNetCore.Mvc;

namespace CheckOutOrderTotalKata.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarkdownPromotionController : ControllerBase
    {
        private readonly BaseCacheService<MarkdownPromotion> _markdowns;

        private readonly BaseCacheService<StoreItem> _store;

        public MarkdownPromotionController(BaseCacheService<MarkdownPromotion> markdownService, BaseCacheService<StoreItem> storeService)
        {
            _markdowns = markdownService;
            _store = storeService;
        }

        [HttpGet]
        public ActionResult<List<StoreItem>> Get()
        {
            var items = _markdowns.GetAllItems();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public ActionResult<MarkdownPromotion> Get(string itemName)
        {
            var item = _markdowns.GetItem(itemName);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }
    }
}
