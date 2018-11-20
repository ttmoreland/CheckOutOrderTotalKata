﻿using CheckOutOrderTotalKata.Models;
using CheckOutOrderTotalKata.Util;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckOutOrderTotalKata.ModelTests.ControllersTests
{
    public class MultiplesPromotionServiceMock : BaseCacheService<MultiplesPromotion>
    {
        private readonly List<MultiplesPromotion> _multiples;

        public MultiplesPromotionServiceMock(IMemoryCache cache) : base(cache)
        {
            _multiples = new List<MultiplesPromotion>()
            {
                new MultiplesPromotion("Apple", 2, 5),
                new MultiplesPromotion("Chorizo", 2, 7.00m)
            };

            cache.SetCachedItem(CacheKey, _multiples);
        }

        public override string CacheKey => CacheKeys.MultiplesPromotion;
    }
}
