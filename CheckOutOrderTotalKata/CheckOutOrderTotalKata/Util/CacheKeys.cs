using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheckOutOrderTotalKata.Util
{
    public static class CacheKeys
    {
        public static string Cart { get { return "_Cart"; } }
        public static string GroceryItem { get { return "_GroceryItems"; } }
    }
}
