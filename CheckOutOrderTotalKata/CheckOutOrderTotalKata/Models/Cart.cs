using System.Collections.Generic;
using System.Linq;

namespace CheckOutOrderTotalKata.Models
{
    /// <summary>
    /// Cart
    /// </summary>
    public class Cart
    {
        /// <summary>
        /// Gets the total.
        /// </summary>
        /// <value
        /// The total.
        /// </value>
        public decimal Total => PricedItems.Sum(x => x.Extension);


        /// <summary>
        /// Gets or sets the priced items.
        /// </summary>
        /// <value>
        /// The priced items.
        /// </value>
        private List<PricedCartItem> PricedItems { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Cart" /> class.
        /// </summary>
        public Cart()
        {
            PricedItems = new List<PricedCartItem>();
        }


        public void AddPricedItems(List<CartItem> cartItems, List<StoreItem> storeItems)
        {
            StoreItem currentStoreItem;
            foreach (var item in cartItems)
            {
                currentStoreItem = storeItems.FirstOrDefault(x => x.Name == item.Name);
                PricedItems.Add(new PricedCartItem(item.Name, item.Quantity, currentStoreItem.Price));
            }
        }
    }
}
