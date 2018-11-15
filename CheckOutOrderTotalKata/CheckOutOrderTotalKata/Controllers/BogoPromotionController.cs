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
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<BogoPromotion>> Get()
        {
            var items = _bogos.GetAllItems();
            return Ok(items);
        }

        /// <summary>
        /// Gets the specified item name.
        /// </summary>
        /// <param name="itemName">Name of the item.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<BogoPromotion> Get(string itemName)
        {
            var item = _bogos.GetItem(itemName);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPost]
        public ActionResult Post([FromBody] BogoPromotion value)
        {
            //Validate item
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //Check for duplicate item
            if (_bogos.GetItem(value.ItemName) != null)
                return BadRequest($"An item already exists with the name {value.ItemName}.");

            var item = _bogos.Add(value);
            return CreatedAtAction("Get", new { id = item.ItemName }, item);
        }

    }
}
