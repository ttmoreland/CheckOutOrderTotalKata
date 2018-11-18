using System.Collections.Generic;
using CheckOutOrderTotalKata.Models;
using CheckOutOrderTotalKata.Util;
using Microsoft.AspNetCore.Mvc;

namespace CheckOutOrderTotalKata.Controllers
{
    /// <summary>
    /// Bogo Promotion Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class BogoPromotionController : ControllerBase
    {
        /// <summary>
        /// The bogos
        /// </summary>
        private readonly BaseCacheService<BogoPromotion> _bogos;

        /// <summary>
        /// The store
        /// </summary>
        private readonly BaseCacheService<StoreItem> _store;

        /// <summary>
        /// Initializes a new instance of the <see cref="BogoPromotionController"/> class.
        /// </summary>
        /// <param name="bogoService">The bogo service.</param>
        /// <param name="storeService">The store service.</param>
        public BogoPromotionController(BaseCacheService<BogoPromotion> bogoService, BaseCacheService<StoreItem> storeService)
        {
            _bogos = bogoService;
            _store = storeService;
        }

        /// <summary>
        /// Gets the list of buy one get one promotions.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Gets the list of buy one get one promotions.</response>
        [HttpGet]
        [ProducesResponseType(200)]
        [Produces(typeof(IEnumerable<CartItem>))]
        [HttpGet]
        public ActionResult<List<BogoPromotion>> Get()
        {
            List<BogoPromotion> items = _bogos.GetAllItems();
            return Ok(items);
        }

        /// <summary>
        /// Gets the specified buy one get one promotion item.
        /// </summary>
        /// <param name="itemName">Name of the item.</param>
        /// <returns>Gets the specified buy one get one promotion item.</returns>
        /// <response code="200">Returns the item.</response>
        /// <response code="404">The item was not found.</response>   
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [Produces(typeof(BogoPromotion))]
        public ActionResult<BogoPromotion> Get(string itemName)
        {
            BogoPromotion item = _bogos.GetItem(itemName);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        /// <summary>
        /// Adds the specified buy one get one promotion.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// Buy 1 Bread Get 1 @100% (Free), Limit 2
        /// 
        ///     {
        ///        name: "Bread",
        ///        quantityThreshold: 1,
        ///        quantityImpacted: 1,
        ///        percentOff: 100,
        ///        quantityLimit: 2
        ///     }
        ///
        /// </remarks>
        /// <param name="value">The value.</param>
        /// <returns>A newly created buy one get one promotion item.</returns>
        /// <response code="201">Returns the newly created item.</response>
        /// <response code="400">If the item is not valid, it's a duplicate, or item hasn't been set up in the store.</response>   
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public ActionResult Post([FromBody] BogoPromotion value)
        {
            //Validate item
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //Check for duplicate item
            if (_bogos.GetItem(value.Name) != null)
                return BadRequest($"An item already exists with the name {value.Name}.");

            //item needs set up in store to be valid
            if (_store.GetItem(value.Name) == null)
                return BadRequest($"The item ({value.Name}) has not been set up.");

            BogoPromotion item = _bogos.Add(value);
            return CreatedAtAction("Get", new { id = item.Name }, item);
        }

        /// <summary>
        /// Removes the specified buy one get one promotion.
        /// </summary>
        /// <param name="itemName">Name of the buy one get one promotion item.</param>
        /// <returns></returns>
        /// <response code="200">Item successfully deleted.</response>
        /// <response code="404">The item is not found.</response>  
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult Remove(string itemName)
        {
            BogoPromotion existingItem = _bogos.GetItem(itemName);

            if (existingItem == null)
            {
                return NotFound();
            }

            _bogos.Remove(existingItem);
            return Ok();
        }
    }
}
