using System.Collections.Generic;
using CheckOutOrderTotalKata.Models;
using CheckOutOrderTotalKata.Util;
using Microsoft.AspNetCore.Mvc;

namespace CheckOutOrderTotalKata.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BogoPromotionController : ControllerBase
    {
        private readonly BaseCacheService<BogoPromotion> _bogos;

        private readonly BaseCacheService<StoreItem> _store;

        public BogoPromotionController(BaseCacheService<BogoPromotion> bogoService, BaseCacheService<StoreItem> storeService)
        {
            _bogos = bogoService;
            _store = storeService;
        }

        [HttpGet]
        public ActionResult<List<BogoPromotion>> Get()
        {
            var items = _bogos.GetAllItems();
            return Ok(items);
        }

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
    }
}
