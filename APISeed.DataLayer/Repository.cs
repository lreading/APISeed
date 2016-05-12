using APISeed.DataLayer.Extensions;
using APISeed.DataLayer.Interfaces;
using APISeed.DataLayer.Models;
using APISeed.Domain;
using Dapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace APISeed.DataLayer
{
    /// <summary>
    /// Base repsitory class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Repository<T> : IRepository<T> where T : IEntity
    {
        #region Fields, Properties and Constructors
        /// <summary>
        /// The name of the table this object relates to
        /// </summary>
        private readonly string _tableName;

        /// <summary>
        /// The connection factory
        /// </summary>
        private readonly IConnectionFactory _connectionFactory;

        /// <summary>
        /// Persistenance mechanism / backing field
        /// </summary>
        internal static IList<T> _collection;

        /// <summary>
        /// Used for persistence to avoid collecting the same data from the database repeatedly
        /// </summary>
        public virtual IList<T> Collection
        {
            get
            {
                if (_collection == null || _collection.Count == 0)
                {
                    _collection = GetAll().ToList();
                }
                return _collection;
            }
            set
            {
                _collection = value;
            }
        }

        /// <summary>
        /// The database connection
        /// </summary>
        internal IDbConnection Connection
        {
            get
            {
                return _connectionFactory.GetConnection();
            }
        }

        /// <summary>
        /// Any custom mapping required for the item
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        internal virtual dynamic Mapping(T item)
        {
            return item;
        }

        /// <summary>
        /// Creates the repository object
        /// </summary>
        /// <param name="tableName"></param>
        public Repository(string tableName)
        {
            _tableName = tableName;
            _connectionFactory = new ConnectionFactory();
            _collection = new List<T>();
        }

        /// <summary>
        /// Creates the repository object with a custom connection factory.  
        /// Useful for testing.  
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="connectionFactory"></param>
        public Repository(string tableName, IConnectionFactory connectionFactory)
        {
            _tableName = tableName;
            _connectionFactory = connectionFactory;
            _collection = new List<T>();
        }

        #endregion

        #region IRepository Implementation

        /// <summary>
        /// Gets the base type by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T Get(int id)
        {
            using (var db = Connection)
            {
                var sql = string.Format(@"
SELECT          *
FROM            {0}
WHERE           Id = @id
", _tableName);
                return db.Query<T>(sql, new { id = id })
                    .FirstOrDefault();
            }
        }

        /// <summary>
        /// Gets all of the items in the database for this type
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<T> GetAll()
        {
            using (var db = Connection)
            {
                var sql = string.Format(@"
SELECT          *
FROM            {0}
", _tableName);
                var all = db.Query<T>(sql);
                _collection = all.ToList();
                return all;
            }
        }

        /// <summary>
        /// Adds a new item of this type.  Updates the ID
        /// </summary>
        /// <param name="item"></param>
        public virtual void Add(T item)
        {
            using (var db = Connection)
            {
                var parameters = (object)Mapping(item);
                db.Open();
                item.Id = db.Insert<int>(_tableName, parameters);
                _collection.Add(item);
            }
        }

        /// <summary>
        /// Updates the item in the database
        /// </summary>
        /// <param name="item"></param>
        public virtual void Update(T item)
        {
            using (var db = Connection)
            {
                var parameters = (object)Mapping(item);
                db.Open();
                db.Update(_tableName, parameters);
                _collection.Remove(_collection.FirstOrDefault(x => x.Id == item.Id));
                _collection.Add(item);
            }
        }

        /// <summary>
        /// Deletes an item from the database.
        /// </summary>
        /// <remarks>
        /// This is NOT a soft-delete!
        /// </remarks>
        /// <param name="id"></param>
        public virtual void Delete(int id)
        {
            using (var db = Connection)
            {
                var sql = string.Format(@"
DELETE FROM     {0}
WHERE           Id = @id
", _tableName);
                db.Execute(sql, new { id = id });
                _collection.Remove(_collection.FirstOrDefault(x => x.Id == id));
            }
        }

        #endregion

        #region Helper Methods
        /// <summary>
        /// Gets the ApplicationUser from their id.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public ApplicationUser GetUserFromId(string userId)
        {
            var context = new ApplicationDbContext();
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            return UserManager.FindById(userId);
        }

        /// <summary>
        /// Gets the application user from their id Async
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Task<ApplicationUser> GetUserByIdAsync(string userId)
        {
            var context = new ApplicationDbContext();
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            return UserManager.FindByIdAsync(userId);
        }

        #endregion
    }
}
