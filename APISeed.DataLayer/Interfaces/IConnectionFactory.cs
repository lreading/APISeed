using System.Data;

namespace APISeed.DataLayer.Interfaces
{
    /// <summary>
    /// Minimal interface to describe our connection factories.
    /// </summary>
    public interface IConnectionFactory
    {
        IDbConnection GetConnection();
    }
}
