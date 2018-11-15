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

        [HttpPost]
        public ActionResult Post([FromBody] MultiplesPromotion value)
        {
            //Validate item
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //Check for duplicate item
            if (_multiples.GetItem(value.ItemName) != null)
                return BadRequest($"An item already exists with the name {value.ItemName}.");

            //item needs set up in store to be valid
            if (_store.GetItem(value.ItemName) == null)
                return BadRequest($"The item ({value.ItemName}) has not been set up.");

            var item = _multiples.Add(value);
            return CreatedAtAction("Get", new { id = item.ItemName }, item);
        }

    }
}
