using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Template.DataLayer
{
    /// <summary>
    /// Handles connection related tasks for database work
    /// </summary>
    internal class ConnectionFactory : IConnectionFactory
    {
        /// <summary>
        /// Gets a SqlConnection using the DefaultConnection connectionString in the web.config
        /// </summary>
        /// <returns></returns>
        public IDbConnection GetConnection()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        }

        /// <summary>
        /// Gets a SqlConnection using the provided connection string
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public IDbConnection GetConnection(string connectionString)
        {
            return new SqlConnection(connectionString);
        }
    }
}