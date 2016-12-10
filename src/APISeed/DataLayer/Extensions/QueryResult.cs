using System;

namespace DataLayer.Extensions
{
    /// <summary>
    /// A result object with the generated sql and dynamic params.
    /// </summary>
    public class QueryResult
    {
        /// <summary>
        /// The _result
        /// </summary>
        private readonly Tuple<string, object> _result;

        /// <summary>
        /// Gets the SQL.
        /// </summary>
        /// <value>
        /// The SQL.
        /// </value>
        public string Sql
        {
            get
            {
                return _result.Item1;
            }
        }

        /// <summary>
        /// Gets the param.
        /// </summary>
        /// <value>
        /// The param.
        /// </value>
        public object Param
        {
            get
            {
                return _result.Item2;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryResult" /> class.
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <param name="param">The param.</param>
        public QueryResult(string sql, object param)
        {
            _result = new Tuple<string, object>(sql, param);
        }
    }
}
