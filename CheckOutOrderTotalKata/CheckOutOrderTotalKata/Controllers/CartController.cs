using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CheckOutOrderTotalKata.Models;
using CheckOutOrderTotalKata.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CheckOutOrderTotalKata.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _service;

        public CartController(ICartService service)
        {
            _service = service;
        }

        // GET: api/Cart
        [HttpGet]
        public ActionResult<List<CartItem>> Get()
        {
            var items = _service.GetAllItems();
            return Ok(items);
        }

        // GET api/Cart/SomeItem
        [HttpGet("{id}")]
        public ActionResult<CartItem> Get(string itemName)
        {
            var item = _service.GetItem(itemName);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        // POST: api/Cart
        [HttpPost]
        public ActionResult Post([FromBody] CartItem value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = _service.Add(value);
            return CreatedAtAction("Get", new { id = item.Name }, item);
        }

        // DELETE: api/Cart/SomeItem
        [HttpDelete("{id}")]
        public ActionResult Remove(string itemName)
        {
            var existingItem = _service.GetItem(itemName);

            if (existingItem == null)
            {
                return NotFound();
            }

            _service.Remove(itemName);
            return Ok();
        }
    }
}
