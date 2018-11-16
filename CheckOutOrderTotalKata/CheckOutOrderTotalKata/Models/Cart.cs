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


        /// <summary>
        /// Adds the priced items.
        /// </summary>
        /// <param name="cartItems">The cart items.</param>
        /// <param name="storeItems">The store items.</param>
        public void AddPricedItems(List<CartItem> cartItems, List<StoreItem> storeItems)
        {
            StoreItem currentStoreItem;
            foreach (var item in cartItems)
            {
                currentStoreItem = storeItems.FirstOrDefault(x => x.Name == item.Name);
                PricedItems.Add(new PricedCartItem(item.Name, item.Quantity, currentStoreItem.Price));
            }
        }

        /// <summary>
        /// Applies the promotions.
        /// </summary>
        /// <param name="markdowns">The markdowns.</param>
        /// <param name="multiples">The multiples.</param>
        /// <param name="bogos">The bogos.</param>
        public void ApplyPromotions(List<MarkdownPromotion> markdowns, List<MultiplesPromotion> multiples, List<BogoPromotion> bogos)
        {
            ApplyMarkdowns(markdowns);
            ApplyMultiples(multiples);
            ApplyBogos(bogos);
        }

        /// <summary>
        /// Applies the markdowns.
        /// </summary>
        /// <param name="markdowns">The markdowns.</param>
        private void ApplyMarkdowns(List<MarkdownPromotion> markdowns)
        {
            if(markdowns != null && markdowns.Count != 0)
            {
                decimal discount = 0;
                PricedCartItem currentItem;
                foreach  (MarkdownPromotion promo in markdowns)
                {
                    currentItem = GetGroupedCartItems().FirstOrDefault(x => x.Name == promo.Name);
                    discount = currentItem.Quantity * promo.Discount;
                    PricedItems.Add(new PricedCartItem($"Markdown on {promo.Name}.", 1, discount));
                }
            }
        }

        /// <summary>
        /// Applies the multiples.
        /// </summary>
        /// <param name="multiples">The multiples.</param>
        private void ApplyMultiples(List<MultiplesPromotion> multiples)
        {
            if (multiples != null && multiples.Count != 0)
            {

            }
        }

        /// <summary>
        /// Applies the bogos.
        /// </summary>
        /// <param name="bogos">The bogos.</param>
        private void ApplyBogos(List<BogoPromotion> bogos)
        {
            if (bogos != null && bogos.Count != 0)
            {

            }
        }


        private List<PricedCartItem> GetGroupedCartItems()
        {
            var query = PricedItems.GroupBy(g => new { g.Name })
                                 .Select(group => new PricedCartItem
                                     {
                                         Name = group.Key.Name,
                                         Quantity = group.Sum(x => x.Quantity),
                                         Price = group.First().Price
                                     }).ToList();

            return query;
        }
    }
}
