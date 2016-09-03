using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace $safeprojectname$
{
    /// <summary>
    /// Handles connection related tasks for database work
    /// </summary>
    internal class ConnectionFactory : Interfaces.IConnectionFactory
    {
        /// <summary>
        /// Gets a SqlConnection using the DefaultConnection connectionString in the web.config
        /// </summary>
        /// <returns></returns>
        public IDbConnection GetConnection()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        }
    }
}
