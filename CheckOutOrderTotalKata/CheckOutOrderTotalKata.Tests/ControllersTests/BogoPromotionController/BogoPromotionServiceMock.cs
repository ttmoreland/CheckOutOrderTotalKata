using CheckOutOrderTotalKata.Models;
using CheckOutOrderTotalKata.Util;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckOutOrderTotalKata.ModelTests.ControllersTests
{
    public class BogoPromotionServiceMock : BaseCacheService<BogoPromotion>
    {
        private readonly List<BogoPromotion> _bogo;

        public BogoPromotionServiceMock(IMemoryCache cache) : base(cache)
        {
            _bogo = new List<BogoPromotion>()
            {
                new BogoPromotion("Steak", 2, 1, 50, 3)
            };

            cache.SetCachedItem(CacheKey, _bogo);
        }

        public override string CacheKey => CacheKeys.BogoPromotion;
    }
}
