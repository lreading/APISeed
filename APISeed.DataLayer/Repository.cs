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
        private readonly string _tableName;
        private readonly IConnectionFactory _connectionFactory;
        internal static IList<T> _collection;
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

        internal IDbConnection Connection
        {
            get
            {
                return _connectionFactory.GetConnection();
            }
        }

        internal virtual dynamic Mapping(T item)
        {
            return item;
        }

        public Repository(string tableName)
        {
            _tableName = tableName;
            _connectionFactory = new ConnectionFactory();
            _collection = new List<T>();
        }

        public Repository(string tableName, IConnectionFactory connectionFactory)
        {
            _tableName = tableName;
            _connectionFactory = connectionFactory;
            _collection = new List<T>();
        }

        #endregion

        #region IRepository Implementation

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

        public ApplicationUser GetUserFromId(string userId)
        {
            var context = new ApplicationDbContext();
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            return UserManager.FindById(userId);
        }

        public Task<ApplicationUser> GetUserByIdAsync(string userId)
        {
            var context = new ApplicationDbContext();
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            return UserManager.FindByIdAsync(userId);
        }

        #endregion
    }
}
