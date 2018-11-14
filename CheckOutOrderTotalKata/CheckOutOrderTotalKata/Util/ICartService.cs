using CheckOutOrderTotalKata.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheckOutOrderTotalKata.Util
{
    public interface ICartService : IBaseService<CartItem>
    {
        decimal GetCartTotal();
        IEnumerable<StoreItem> GetStoreItems();
    }
}
