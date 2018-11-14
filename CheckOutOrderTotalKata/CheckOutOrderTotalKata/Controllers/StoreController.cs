using System.Collections.Generic;
using CheckOutOrderTotalKata.Models;
using CheckOutOrderTotalKata.Util;
using Microsoft.AspNetCore.Mvc;

namespace CheckOutOrderTotalKata.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IBaseService<StoreItem> _store;

        public StoreController(IBaseService<StoreItem> service)
        {
            _store = service;
        }

        [HttpGet]
        public ActionResult<List<StoreItem>> Get()
        {
            var items = _store.GetAllItems();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public ActionResult<CartItem> Get(string itemName)
        {
            var item = _store.GetItem(itemName);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPost]
        public ActionResult Post([FromBody] StoreItem value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = _store.Add(value);
            return CreatedAtAction("Get", new { id = item.Name }, item);
        }

        [HttpDelete("{id}")]
        public ActionResult Remove(string itemName)
        {
            var existingItem = _store.GetItem(itemName);

            if (existingItem == null)
            {
                return NotFound();
            }

            _store.Remove(itemName);
            return Ok();
        }
    }
}
