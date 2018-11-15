using System.Collections.Generic;
using CheckOutOrderTotalKata.Models;
using CheckOutOrderTotalKata.Util;
using Microsoft.AspNetCore.Mvc;

namespace CheckOutOrderTotalKata.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MultiplesPromotionController : ControllerBase
    {
        private readonly BaseCacheService<MultiplesPromotion> _multiples;

        private readonly BaseCacheService<StoreItem> _store;

        public MultiplesPromotionController(BaseCacheService<MultiplesPromotion> multiplesService, BaseCacheService<StoreItem> storeService)
        {
            _multiples = multiplesService;
            _store = storeService;
        }

        [HttpGet]
        public ActionResult<List<MultiplesPromotion>> Get()
        {
            var items = _multiples.GetAllItems();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public ActionResult<MultiplesPromotion> Get(string itemName)
        {
            var item = _multiples.GetItem(itemName);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }
    }
}
