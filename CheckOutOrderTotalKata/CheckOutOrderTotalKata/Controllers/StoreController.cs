using System.Collections.Generic;
using CheckOutOrderTotalKata.Models;
using CheckOutOrderTotalKata.Util;
using Microsoft.AspNetCore.Mvc;

namespace CheckOutOrderTotalKata.Controllers
{
    /// <summary>
    /// Store Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly BaseCacheService<StoreItem> _store;

        public StoreController(BaseCacheService<StoreItem> service)
        {
            _store = service;
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<StoreItem>> Get()
        {
            var items = _store.GetAllItems();
            return Ok(items);
        }

        /// <summary>
        /// Gets the specified item name.
        /// </summary>
        /// <param name="itemName">Name of the item.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Posts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Post([FromBody] StoreItem value)
        {
            //Validate item
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            //Check for duplicate item
            if (_store.GetItem(value.Name) != null)
                return BadRequest($"An item already exists with the name {value.Name}.");
            
            var item = _store.Add(value);
            return CreatedAtAction("Get", new { id = item.Name }, item);
        }

        /// <summary>
        /// Removes the specified item name.
        /// </summary>
        /// <param name="itemName">Name of the item.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult Remove(string itemName)
        {
            var existingItem = _store.GetItem(itemName);

            if (existingItem == null)
            {
                return NotFound();
            }

            _store.Remove(existingItem);
            return Ok();
        }
    }
}
