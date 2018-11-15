using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheckOutOrderTotalKata.Util
{
    public abstract class BaseListService<T> : IBaseService<T>
    {
        /// <summary>
        /// The cart
        /// </summary>
        private readonly List<T> _items;


        /// <summary>
        /// Adds the specified new item.
        /// </summary>
        /// <param name="newItem">The new item.</param>
        /// <returns></returns>
        public T Add(T newItem)
        {
            _items.Add(newItem);
            return newItem;
        }

        /// <summary>
        /// Gets all items.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetAllItems()
        {
            return _items;
        }

        /// <summary>
        /// Removes the specified item name.
        /// </summary>
        /// <param name="item">The item.</param>
        public void Remove(T item)
        {
            _items.Remove(item);
        }

        /// <summary>
        /// Gets the item.
        /// </summary>
        /// <param name="itemName">Name of the item.</param>
        /// <returns></returns>
        abstract public T GetItem(string itemName);
    }
}
