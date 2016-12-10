using Dapper;
using DataLayer.Extensions;
using Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DataLayer.Repositories
{
    public abstract class RepositoryBase<T> : IRepository<T>, IDisposable where T : IEntity
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
        public RepositoryBase(string tableName)
        {
            _tableName = tableName;
            _connectionFactory = new ConnectionFactory();
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

        public void Add(T item)
        {
            using (var db = Connection)
            {
                var parameters = Mapping(item);
                db.Open();
                item.Id = db.Insert<int>(_tableName, parameters);
            }
        }

        public void Delete(int id)
        {
            using (var db = Connection)
            {
                var sql = string.Format(@"
DELETE FROM     {0}
WHERE           Id = @id
", _tableName);
                db.Execute(sql, new { id = id });
            }
        }

        public T Get(int id)
        {
            using (var db = Connection)
            {
                var sql = string.Format(@"
SELECT          *
FROM            {0}
WHERE           Id = @id
", _tableName);
                return db.Query<T>(sql, new { id = id }).FirstOrDefault();
            }
        }

        public IEnumerable<T> GetAll()
        {
            using (var db = Connection)
            {
                var sql = string.Format(@"
SELECT          *
FROM            {0}
", _tableName);
                var all = db.Query<T>(sql);
                return all;
            }
        }

        public IEnumerable<T> GetAll(IQueryFilter filter)
        {
            throw new NotImplementedException();
        }

        public void Update(T item)
        {
            using (var db = Connection)
            {
                var parameters = Mapping(item);
                db.Open();
                db.Update(_tableName, parameters);
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
