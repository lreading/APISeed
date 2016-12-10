using Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    /// <summary>
    /// Generic repository implementation
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IAsyncRepository<T> where T : IEntity
    {
        /// <summary>
        /// Gets a specific item by id from the repository
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetAsync(int id);

        /// <summary>
        /// Gets all items from the repository
        /// </summary>
        /// <remarks>
        /// Use with care, could be a lot of data.
        /// </remarks>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Gets all items that match the filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAllAsync(IQueryFilter filter);

        /// <summary>
        /// Adds an item to the repository
        /// </summary>
        /// <param name="item"></param>
        Task AddAsync(T item);

        /// <summary>
        /// Updates an existing item in the repository
        /// </summary>
        /// <param name="item"></param>
        Task UpdateAsync(T item);

        /// <summary>
        /// Deletes an existing item from the repository
        /// </summary>
        /// <param name="id"></param>
        Task DeleteAsync(int id);
    }
}
