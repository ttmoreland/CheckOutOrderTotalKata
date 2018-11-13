using Xunit;
using CheckOutOrderTotalKata.Models;
using System.Collections.Generic;

namespace CheckOutOrderTotalKata.Tests
{
    public class GroceryStoreTests
    {
        [Fact]
        public void TestGroceryStoreCollections()
        {
            GroceryStore groceryStore = new GroceryStore();
            Assert.Equal(new List<GroceryItem>(), groceryStore.GroceryItems);
            Assert.Equal(new List<CartItem>(), groceryStore.CartItems);
        }
    }
}
