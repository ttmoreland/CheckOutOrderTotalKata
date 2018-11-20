using CheckOutOrderTotalKata.Models;
using CheckOutOrderTotalKata.Util;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckOutOrderTotalKata.ModelTests.ControllersTests
{
    public class MarkdownPromotionServiceMock : BaseCacheService<MarkdownPromotion>
    {
        private readonly List<MarkdownPromotion> _markdowns;

        public MarkdownPromotionServiceMock(IMemoryCache cache) : base(cache)
        {
            _markdowns = new List<MarkdownPromotion>()
            {
                new MarkdownPromotion("Soup", -.20m)
            };

            cache.SetCachedItem(CacheKey, _markdowns);
        }

        public override string CacheKey => CacheKeys.MarkdownPromotion;
    }
}
