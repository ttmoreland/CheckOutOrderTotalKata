using System.Collections.Generic;
using System.Linq;
using CheckOutOrderTotalKata.Models;
using CheckOutOrderTotalKata.Util;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace CheckOutOrderTotalKata.Controllers
{
    /// <summary>
    /// Cart Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        /// <summary>
        /// The cart
        /// </summary>
        private readonly BaseCacheService<CartItem> _cart;

        /// <summary>
        /// The store
        /// </summary>
        private readonly BaseCacheService<StoreItem> _store;

        /// <summary>
        /// The markdowns
        /// </summary>
        private readonly BaseCacheService<MarkdownPromotion> _markdowns;

        /// <summary>
        /// The multiples
        /// </summary>
        private readonly BaseCacheService<MultiplesPromotion> _multiples;

        /// <summary>
        /// The bogos
        /// </summary>
        private readonly BaseCacheService<BogoPromotion> _bogos;

        /// <summary>
        /// Initializes a new instance of the <see cref="CartController"/> class.
        /// </summary>
        /// <param name="service">The service.</param>
        public CartController(BaseCacheService<CartItem> cartService, 
                              BaseCacheService<StoreItem> storeService,
                              BaseCacheService<MarkdownPromotion> markdownService,
                              BaseCacheService<MultiplesPromotion> multiplesService,
                              BaseCacheService<BogoPromotion> bogosService)
        {
            _cart = cartService;
            _store = storeService;
            _bogos = bogosService;
            _markdowns = markdownService;
            _multiples = multiplesService;
        }

        /// <summary>
        /// Gets list of cart items. GET: api/Cart
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<CartItem>> Get()
        {
            var items = _cart.GetAllItems();
            return Ok(items);
        }

        [HttpGet]
        [Route("GetCartTotal")]
        public ActionResult<Cart> GetCartTotal()
        {
            Cart cart = new Cart();

            //add items to cart with price
            cart.AddPricedItems(_cart.GetAllItems(), _store.GetAllItems());

            //if cart is empty bad request
            if (cart.Total == 0 || _cart.GetAllItems().Count() == 0)
                return BadRequest("Cart is empty.");

            //add promotional line items after adding items to cart with price
            cart.ApplyPromotions(_markdowns?.GetAllItems(), _multiples?.GetAllItems(), _bogos?.GetAllItems());

            return Ok(cart);
        }

        /// <summary>
        /// Gets the specified item by name. GET: api/Cart/SomeItem
        /// </summary>
        /// <param name="itemName">Name of the item.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<CartItem> Get(string itemName)
        {
            var item = _cart.GetItem(itemName);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        /// <summary>
        /// Adds the specified value. POST: api/Cart
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Post([FromBody] CartItem value)
        {
            //validate item
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //item needs set up in store to be valid
            if (_store.GetItem(value.Name) == null)
                return BadRequest($"The item ({value.Name}) has not been set up.");

            var item = _cart.Add(value);
            return CreatedAtAction("Get", new { id = item.Name }, item);
        }

        /// <summary>
        /// Removes the specified item by name. DELETE: api/Cart/SomeItem
        /// </summary>
        /// <param name="itemName">Name of the item.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult Remove(string itemName)
        {
            var existingItem = _cart.GetItem(itemName);

            if (existingItem == null)
            {
                return NotFound();
            }

            _cart.Remove(existingItem);
            return Ok();
        }
    }
}
