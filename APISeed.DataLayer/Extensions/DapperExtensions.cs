using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace APISeed.DataLayer.Extensions
{
    // Borrowed from: https://github.com/bbraithwaite/RepoWrapper/blob/master/QueryResult.cs
    /// <summary>
    /// Extension methods for the Dapper dot Net library
    /// </summary>
    public static class DapperExtensions
    {
        /// <summary>
        /// Inserts an object into the database.  Will update the id parameter.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cnn"></param>
        /// <param name="tableName"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static T Insert<T>(this IDbConnection cnn, string tableName, dynamic param)
        {
            IEnumerable<T> result = SqlMapper.Query<T>(cnn, DynamicQuery.GetInsertQuery(tableName, param), param);
            return result.First();
        }

        /// <summary>
        /// Updates an object.
        /// </summary>
        /// <param name="cnn"></param>
        /// <param name="tableName"></param>
        /// <param name="param"></param>
        public static void Update(this IDbConnection cnn, string tableName, dynamic param)
        {
            SqlMapper.Execute(cnn, DynamicQuery.GetUpdateQuery(tableName, param), param);
        }
    }
}
