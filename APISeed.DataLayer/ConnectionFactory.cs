using APISeed.DataLayer.Interfaces;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace APISeed.DataLayer
{
    internal class ConnectionFactory : IConnectionFactory
    {
        public IDbConnection GetConnection()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        }
    }
}
