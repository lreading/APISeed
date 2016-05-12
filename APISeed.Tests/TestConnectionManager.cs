using APISeed.DataLayer.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace APISeed.Tests
{
    /// <summary>
    /// Connection Manager specifically for testing
    /// </summary>
    public class TestConnectionManager : IConnectionFactory
    {
        /// <summary>
        /// Gets a IDbConnection that will not affect a real server
        /// </summary>
        /// <returns></returns>
        public IDbConnection GetConnection()
        {
            // Change this to your local sql instance name
            const string localDbName = @"SO_DESK_OFFICE1\SOFTWAREOUTSIDE";
            return new SqlConnection(string.Format("Data Source={0};Initial Catalog=tempdb; Integrated Security=true;User Instance=True;", localDbName));
        }
    }
}
