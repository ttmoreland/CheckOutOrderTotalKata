using System.Collections.Generic;
using CheckOutOrderTotalKata.Models;
using CheckOutOrderTotalKata.Util;
using Microsoft.AspNetCore.Mvc;

namespace CheckOutOrderTotalKata.Controllers
{
    /// <summary>
    /// Markdown Promotion Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class MarkdownPromotionController : ControllerBase
    {
        /// <summary>
        /// The markdowns
        /// </summary>
        private readonly BaseCacheService<MarkdownPromotion> _markdowns;

        /// <summary>
        /// The store
        /// </summary>
        private readonly BaseCacheService<StoreItem> _store;

        /// <summary>
        /// Initializes a new instance of the <see cref="MarkdownPromotionController"/> class.
        /// </summary>
        /// <param name="markdownService">The markdown service.</param>
        /// <param name="storeService">The store service.</param>
        public MarkdownPromotionController(BaseCacheService<MarkdownPromotion> markdownService, BaseCacheService<StoreItem> storeService)
        {
            _markdowns = markdownService;
            _store = storeService;
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<StoreItem>> Get()
        {
            var items = _markdowns.GetAllItems();
            return Ok(items);
        }

        /// <summary>
        /// Gets the specified item name.
        /// </summary>
        /// <param name="itemName">Name of the item.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Posts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Post([FromBody] MarkdownPromotion value)
        {
            //Validate item
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //Check for duplicate item
            if (_markdowns.GetItem(value.Name) != null)
                return BadRequest($"An item already exists with the name {value.Name}.");

            //item needs set up in store to be valid
            if (_store.GetItem(value.Name) == null)
                return BadRequest($"The item ({value.Name}) has not been set up.");

            var item = _markdowns.Add(value);
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
            var existingItem = _markdowns.GetItem(itemName);

            if (existingItem == null)
            {
                return NotFound();
            }

            _markdowns.Remove(existingItem);
            return Ok();
        }

    }
}
