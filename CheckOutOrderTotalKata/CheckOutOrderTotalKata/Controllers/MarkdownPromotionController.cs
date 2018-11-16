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
    [Produces("application/json")]
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
        /// Gets the list of mark down promotions.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Gets the list of mark down promotions.</response>
        [HttpGet]
        [ProducesResponseType(200)]
        [Produces(typeof(IEnumerable<MarkdownPromotion>))]
        public ActionResult<List<MarkdownPromotion>> Get()
        {
            var items = _markdowns.GetAllItems();
            return Ok(items);
        }

        /// <summary>
        /// Gets the specified mark down promotion item.
        /// </summary>
        /// <param name="itemName">Name of the item.</param>
        /// <returns>Gets the specified mark down promotion item.</returns>
        /// <response code="200">Returns the item.</response>
        /// <response code="404">The item was not found.</response>   
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [Produces(typeof(MarkdownPromotion))]
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
        /// Adds the specified mark down promotion.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// Soup is $.20 off
        /// 
        ///     {
        ///        name: "Soup",
        ///        discount: -.20
        ///     }
        ///
        /// </remarks>
        /// <param name="value">New mark down promotion item.</param>
        /// <returns>A newly created mark down promotion item.</returns>
        /// <response code="201">Returns the newly created item.</response>
        /// <response code="400">If the item is not valid, it's a duplicate, or item hasn't been set up in the store.</response>   
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
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
        /// Removes the specified mark down promotion.
        /// </summary>
        /// <param name="itemName">Name of markdown item.</param>
        /// <returns></returns>
        /// <response code="200">Item successfully deleted.</response>
        /// <response code="404">The item is not found.</response>  
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
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
