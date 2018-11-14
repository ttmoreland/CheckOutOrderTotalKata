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
        private readonly IBaseService<StoreItem> _service;

        public StoreController(IBaseService<StoreItem> service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<StoreItem>> Get()
        {
            var items = _service.GetAllItems();
            return Ok(items);
        }
    }
}
