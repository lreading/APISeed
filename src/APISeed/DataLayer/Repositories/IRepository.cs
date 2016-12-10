using Domain;
using System.Collections.Generic;

namespace DataLayer.Repositories
{
    /// <summary>
    /// Generic repository implementation
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T : IEntity
    {
        /// <summary>
        /// Gets a specific item by id from the repository
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T Get(int id);

        /// <summary>
        /// Gets all items from the repository
        /// </summary>
        /// <remarks>
        /// Use with care, could be a lot of data.
        /// </remarks>
        /// <returns></returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Gets all items that match the filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        IEnumerable<T> GetAll(IQueryFilter filter);

        /// <summary>
        /// Adds an item to the repository
        /// </summary>
        /// <param name="item"></param>
        void Add(T item);

        /// <summary>
        /// Updates an existing item in the repository
        /// </summary>
        /// <param name="item"></param>
        void Update(T item);

        /// <summary>
        /// Deletes an existing item from the repository
        /// </summary>
        /// <param name="id"></param>
        void Delete(int id);
    }
}
