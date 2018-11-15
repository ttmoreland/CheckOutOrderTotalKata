using System.Collections.Generic;
using CheckOutOrderTotalKata.Models;
using CheckOutOrderTotalKata.Util;
using Microsoft.AspNetCore.Mvc;

namespace CheckOutOrderTotalKata.Controllers
{
    /// <summary>
    /// Multiples Promotion Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class MultiplesPromotionController : ControllerBase
    {
        /// <summary>
        /// The multiples
        /// </summary>
        private readonly BaseCacheService<MultiplesPromotion> _multiples;

        /// <summary>
        /// The store
        /// </summary>
        private readonly BaseCacheService<StoreItem> _store;

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiplesPromotionController"/> class.
        /// </summary>
        /// <param name="multiplesService">The multiples service.</param>
        /// <param name="storeService">The store service.</param>
        public MultiplesPromotionController(BaseCacheService<MultiplesPromotion> multiplesService, BaseCacheService<StoreItem> storeService)
        {
            _multiples = multiplesService;
            _store = storeService;
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<MultiplesPromotion>> Get()
        {
            var items = _multiples.GetAllItems();
            return Ok(items);
        }

        /// <summary>
        /// Gets the specified item name.
        /// </summary>
        /// <param name="itemName">Name of the item.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Posts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Post([FromBody] MultiplesPromotion value)
        {
            //Validate item
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //Check for duplicate item
            if (_multiples.GetItem(value.Name) != null)
                return BadRequest($"An item already exists with the name {value.Name}.");

            //item needs set up in store to be valid
            if (_store.GetItem(value.Name) == null)
                return BadRequest($"The item ({value.Name}) has not been set up.");

            var item = _multiples.Add(value);
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
            var existingItem = _multiples.GetItem(itemName);

            if (existingItem == null)
            {
                return NotFound();
            }

            _multiples.Remove(existingItem);
            return Ok();
        }
    }
}
