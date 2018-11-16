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
        /// <value>
        /// The total.
        /// </value>
        public decimal Total => PricedItems.Sum(x => x.Extension);

        /// <summary>
        /// Gets or sets the priced items.
        /// </summary>
        /// <value>
        /// The priced items.
        /// </value>
        public List<PricedCartItem> PricedItems { get; set; }

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

                    //only apply discount if discount will be less than or equal to the price of the product
                    if (currentItem.Price + promo.Discount >= 0)
                    {
                        discount = currentItem.Quantity * promo.Discount;
                        PricedItems.Add(new PricedCartItem($"Markdown on {promo.Name}.", 1, discount));
                    }
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
                decimal discount = 0;
                decimal mod = 0;
                decimal extensionWithDiscount = 0;
                PricedCartItem currentItem;
                foreach (MultiplesPromotion promo in multiples)
                {
                    currentItem = GetGroupedCartItems().FirstOrDefault(x => x.Name == promo.Name);
                    //Quantity needs to be larger than promo quantity
                    if (currentItem != null && currentItem.Quantity > promo.Quantity)
                    {
                        mod = currentItem.Quantity % promo.Quantity;

                        //calculating what the extension would have already been with discount
                        extensionWithDiscount = ((currentItem.Quantity - (mod * promo.Quantity)) * currentItem.Price) + (mod * promo.Price);

                        //taking extension - discounted extension to derive discount amount
                        discount = (currentItem.Extension - extensionWithDiscount) * -1;
                        PricedItems.Add(new PricedCartItem($"{promo.Quantity} {currentItem.Name} for {promo.Price} promotion.", 1, discount));
                    } else if(currentItem != null && currentItem.Quantity == promo.Quantity)
                    {
                        //taking extension - discounted extension to derive discount amount
                        discount = (currentItem.Extension - promo.Price) * -1;
                        PricedItems.Add(new PricedCartItem($"{promo.Quantity} {currentItem.Name} for {promo.Price} promotion.", 1, discount));
                    }
                }
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
                decimal discount = 0;
                int counter = 0;
                decimal quantity = 0;
                PricedCartItem currentItem;
                foreach (BogoPromotion promo in bogos)
                {
                    currentItem = GetGroupedCartItems().FirstOrDefault(x => x.Name == promo.Name);
                    if (currentItem != null && currentItem.Quantity > promo.QuantityThreshold)
                    {
                        quantity = currentItem.Quantity;
                        while (quantity > 0 && (promo.QuantityLimit == 0 || counter < promo.QuantityLimit))
                        {
                            if (quantity > promo.QuantityThreshold)
                            {
                                discount = promo.QuantityImpacted * promo.PercentOff * .01m * currentItem.Price;
                                quantity -= promo.QuantityImpacted;
                                counter++;
                                PricedItems.Add(new PricedCartItem($"Buy {promo.QuantityThreshold} {currentItem.Name} get {promo.QuantityImpacted} {currentItem.Name} {promo.PercentOff}% off promotion.", 1, discount * -1));
                            }
                            quantity -= promo.QuantityThreshold;
                        }
                    }
                }
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
