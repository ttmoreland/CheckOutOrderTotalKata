using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CheckOutOrderTotalKata.Models;
using CheckOutOrderTotalKata.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CheckOutOrderTotalKata.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IBaseService<StoreItem> _store;

        public StoreController(IBaseService<StoreItem> service)
        {
            _store = service;
        }

        [HttpGet]
        public ActionResult<List<StoreItem>> Get()
        {
            var items = _store.GetAllItems();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public ActionResult<CartItem> Get(string itemName)
        {
            var item = _store.GetItem(itemName);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }
    }
}
