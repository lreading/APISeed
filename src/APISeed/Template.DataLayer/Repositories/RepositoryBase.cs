using Dapper;
using Template.DataLayer.Extensions;
using Template.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Template.DataLayer.Repositories
{
    public abstract class RepositoryBase<T> : IAsyncRepository<T>, IDisposable where T : IEntity
    {

        #region Fields, Properties and Constructors

        /// <summary>
        /// Helper for the IDisposable implementation
        /// </summary>
        private bool _disposed;

        /// <summary>
        /// The name of the table this object relates to
        /// </summary>
        private readonly string _tableName;

        /// <summary>
        /// The connection factory
        /// </summary>
        private readonly IConnectionFactory _connectionFactory;

        /// <summary>
        /// The actual connection
        /// </summary>
        private IDbConnection _connection;

        /// <summary>
        /// The database connection
        /// </summary>
        internal IDbConnection Connection
        {
            get
            {
                if (_connection == null) _connection = _connectionFactory.GetConnection();
                return _connection;
            }
        }

        /// <summary>
        /// Custom mapping for the item.  
        /// </summary>
        /// <remarks>
        /// This should only need to be overridden in the case of a complex object
        /// </remarks>
        /// <param name="item"></param>
        /// <returns></returns>
        internal virtual object Mapping(T item)
        {
            return item;
        }

        /// <summary>
        /// Creates a new instance of the repository base
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="connectionFactory"></param>
        public RepositoryBase(string tableName, IConnectionFactory connectionFactory)
        {
            _tableName = tableName;
            _connectionFactory = connectionFactory;
        }

        #endregion

        #region IRepository Implementation

        public async Task AddAsync(T item)
        {
            using (var db = Connection as SqlConnection)
            {
                var parameters = Mapping(item);
                await db.OpenAsync();
                item.Id = await db.InsertAsync<int>(_tableName, parameters);
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var db = Connection)
            {
                var sql = string.Format(@"
DELETE FROM     {0}
WHERE           Id = @id
", _tableName);
                await db.ExecuteAsync(sql, new { id = id });
            }
        }

        public async Task<T> GetAsync(int id)
        {
            using (var db = Connection)
            {
                var sql = string.Format(@"
SELECT          *
FROM            {0}
WHERE           Id = @id
", _tableName);
                var items = await db.QueryAsync<T>(sql, new { id = id });
                return items.FirstOrDefault();
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            using (var db = Connection)
            {
                var sql = string.Format(@"
SELECT          *
FROM            {0}
", _tableName);
                var all = await db.QueryAsync<T>(sql);
                return all;
            }
        }

        public Task<IEnumerable<T>> GetAllAsync(IQueryFilter filter)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(T item)
        {
            using (var db = Connection as SqlConnection)
            {
                var parameters = Mapping(item);
                await db.OpenAsync();
                await db.UpdateAsync(_tableName, parameters);
            }
        }

        #endregion

        #region IDisposable Implementation

        /// <summary>
        /// Disposes the resources
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Helper for disposing this object
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                _disposed = true;
                if (disposing)
                {
                    if (_connection != null)
                    {
                        _connection.Dispose();
                    }
                }
            }
        }

        #endregion

    }
}
