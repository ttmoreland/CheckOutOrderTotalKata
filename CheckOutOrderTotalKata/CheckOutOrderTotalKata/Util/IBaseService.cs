using CheckOutOrderTotalKata.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheckOutOrderTotalKata.Util
{
    /// <summary>
    /// IBaseService Interface
    /// </summary>
    public interface IBaseService<T> where T :BaseModel
    {

        /// <summary>
        /// Gets all items.
        /// </summary>
        /// <returns></returns>
        List<T> GetAllItems();

        /// <summary>
        /// Adds the specified new item.
        /// </summary>
        /// <param name="newItem">The new item.</param>
        /// <returns></returns>
        T Add(T newItem);

        /// <summary>
        /// Removes the specified item name.
        /// </summary>
        /// <param name="item">The item.</param>
        void Remove(T item);

        /// <summary>
        /// Gets the item.
        /// </summary>
        /// <param name="itemName">Name of the item.</param>
        /// <returns></returns>
        T GetItem(string itemName);
    }
}
