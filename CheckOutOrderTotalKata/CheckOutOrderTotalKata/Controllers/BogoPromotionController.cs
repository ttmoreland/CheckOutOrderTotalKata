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
    }
}
