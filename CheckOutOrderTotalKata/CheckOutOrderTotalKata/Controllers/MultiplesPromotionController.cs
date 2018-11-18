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
    [Produces("application/json")]
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
        /// Gets the list of multiples promotions.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Gets the list of multiples promotions.</response>
        [HttpGet]
        [ProducesResponseType(200)]
        [Produces(typeof(IEnumerable<MultiplesPromotion>))]
        public ActionResult<List<MultiplesPromotion>> Get()
        {
            List<MultiplesPromotion> items = _multiples.GetAllItems();
            return Ok(items);
        }

        /// <summary>
        /// Gets the specified multiples promotion item.
        /// </summary>
        /// <param name="itemName">Name of the item.</param>
        /// <returns>Gets the specified multiples promotion item.</returns>
        /// <response code="200">Returns the item.</response>
        /// <response code="404">The item was not found.</response>   
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [Produces(typeof(MultiplesPromotion))]
        public ActionResult<MultiplesPromotion> Get(string itemName)
        {
            MultiplesPromotion item = _multiples.GetItem(itemName);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        /// <summary>
        /// Adds the specified multiples promotion.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// Buy 2 Guiness 6-Packs for 10.99
        /// 
        ///     {
        ///        name: "Guiness 6-Pack",
        ///        quantity: 2,
        ///        price: 10.99
        ///     }
        ///
        /// </remarks>
        /// <param name="value">New multiples promotion item.</param>
        /// <returns>A newly created multiples promotion item.</returns>
        /// <response code="201">Returns the newly created item.</response>
        /// <response code="400">If the item is not valid, it's a duplicate, or item hasn't been set up in the store.</response>   
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public ActionResult Post([FromBody] MultiplesPromotion value)
        {
            //Validate item
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Check for duplicate item
            if (_multiples.GetItem(value.Name) != null)
            {
                return BadRequest($"An item already exists with the name {value.Name}.");
            }

            //item needs set up in store to be valid
            if (_store.GetItem(value.Name) == null)
            {
                return BadRequest($"The item ({value.Name}) has not been set up.");
            }

            MultiplesPromotion item = _multiples.Add(value);
            return CreatedAtAction("Get", new { id = item.Name }, item);
        }

        /// <summary>
        /// Removes the specified multiples promotion.
        /// </summary>
        /// <param name="itemName">Name of the multiples promotion item.</param>
        /// <returns></returns>
        /// <response code="200">Item successfully deleted.</response>
        /// <response code="404">The item is not found.</response>  
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult Remove(string itemName)
        {
            MultiplesPromotion existingItem = _multiples.GetItem(itemName);

            if (existingItem == null)
            {
                return NotFound();
            }

            _multiples.Remove(existingItem);
            return Ok();
        }
    }
}
