using System.Data;

namespace APISeed.DataLayer.Interfaces
{
    public interface IConnectionFactory
    {
        IDbConnection GetConnection();
    }
}
