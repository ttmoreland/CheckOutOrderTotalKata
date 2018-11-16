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
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        /// <summary>
        /// The store
        /// </summary>
        private readonly BaseCacheService<StoreItem> _store;

        /// <summary>
        /// Initializes a new instance of the <see cref="StoreController"/> class.
        /// </summary>
        /// <param name="service">The service.</param>
        public StoreController(BaseCacheService<StoreItem> service)
        {
            _store = service;
        }

        /// <summary>
        /// Gets a list of items in the store.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Gets the list of items in the store.</response>
        [HttpGet]
        [ProducesResponseType(200)]
        [Produces(typeof(IEnumerable<StoreItem>))]
        public ActionResult<List<StoreItem>> Get()
        {
            var items = _store.GetAllItems();
            return Ok(items);
        }

        /// <summary>
        /// Gets the specified store item by name.
        /// </summary>
        /// <param name="itemName">Name of the item.</param>
        /// <returns>Returns a specific store item.</returns>
        /// <response code="200">Returns the item.</response>
        /// <response code="404">The item was not found.</response>   
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [Produces(typeof(StoreItem))]
        public ActionResult<StoreItem> Get(string itemName)
        {
            var item = _store.GetItem(itemName);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        /// <summary>
        /// Adds the specified item to the store.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     {
        ///        "name": "bread",
        ///        "price": 1.39
        ///     }
        ///
        /// </remarks>
        /// <param name="value">New Store Item.</param>
        /// <returns>A newly created store item.</returns>
        /// <response code="201">Returns the newly created item.</response>
        /// <response code="400">If the item is not valid or is a duplicate.</response>   
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
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
        /// Removes the specified store item.
        /// </summary>
        /// <param name="itemName">Name of the item.</param>
        /// <returns></returns>
        /// <response code="200">Item successfully deleted.</response>
        /// <response code="404">The item is not found.</response>  
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
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
