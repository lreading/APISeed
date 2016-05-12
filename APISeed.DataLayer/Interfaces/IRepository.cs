using System.Collections.Generic;

namespace APISeed.DataLayer.Interfaces
{
    /// <summary>
    /// Interface to describe our repositories
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T>
    {
        T Get(int id);
        IEnumerable<T> GetAll();
        void Add(T item);
        void Update(T item);
        void Delete(int id);
    }
}
