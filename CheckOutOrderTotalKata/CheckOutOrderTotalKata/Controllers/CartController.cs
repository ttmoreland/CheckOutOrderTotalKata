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
    [Produces("application/json")]
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
        /// Initializes a new instance of the <see cref="CartController" /> class.
        /// </summary>
        /// <param name="cartService">The cart service.</param>
        /// <param name="storeService">The store service.</param>
        /// <param name="markdownService">The markdown service.</param>
        /// <param name="multiplesService">The multiples service.</param>
        /// <param name="bogosService">The bogos service.</param>
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
        /// Gets list of cart items.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Gets the list of cart items.</response>
        [HttpGet]
        [ProducesResponseType(200)]
        [Produces(typeof(IEnumerable<CartItem>))]
        public ActionResult<List<CartItem>> Get()
        {
            var items = _cart.GetAllItems();
            return Ok(items);
        }

        /// <summary>
        /// Gets the cart total.
        /// </summary>
        /// <returns>The items in the cart with prices and discounts applied.</returns>
        /// <response code="200">Returns the cart total.</response>
        /// <response code="400">Empty cart.</response>   
        [HttpGet]
        [Route("GetCartTotal")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Produces(typeof(Cart))]
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
        /// Gets the specified cart item.
        /// </summary>
        /// <param name="itemName">Name of the item.</param>
        /// <returns>Gets the specified cart item.</returns>
        /// <response code="200">Returns the item.</response>
        /// <response code="404">The item was not found.</response>   
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [Produces(typeof(CartItem))]
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
        /// Adds the specified item to the cart. If the item isn't supplied with a quantity it is defaulted to 1.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     {
        ///        "name": "bread",
        ///        "quantity": 2
        ///     }
        ///
        /// </remarks>
        /// <param name="value">New Cart Item.</param>
        /// <returns>A newly created Cart Item.</returns>
        /// <response code="201">Returns the newly created item.</response>
        /// <response code="400">If the item is not valid or hasn't been set up in the store.</response>   
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
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
        /// Removes the specified cart item by name.
        /// </summary>
        /// <param name="itemName">Name of the cart item.</param>
        /// <returns></returns>
        /// <response code="200">Item successfully deleted.</response>
        /// <response code="404">The item is not found.</response>  
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
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
