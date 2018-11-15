using System.Collections.Generic;
using CheckOutOrderTotalKata.Models;
using CheckOutOrderTotalKata.Util;
using Microsoft.AspNetCore.Mvc;

namespace CheckOutOrderTotalKata.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MultiplesPromotionController : ControllerBase
    {
        private readonly BaseCacheService<MultiplesPromotion> _multiples;

        private readonly BaseCacheService<StoreItem> _store;

        public MultiplesPromotionController(BaseCacheService<MultiplesPromotion> multiplesService, BaseCacheService<StoreItem> storeService)
        {
            _multiples = multiplesService;
            _store = storeService;
        }
    }
}
