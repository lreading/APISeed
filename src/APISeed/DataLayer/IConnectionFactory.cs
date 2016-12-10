using System.Data;

namespace DataLayer
{
    /// <summary>
    /// Minimal interface to describe our connection factories.
    /// </summary>
    public interface IConnectionFactory
    {
        IDbConnection GetConnection();
    }
}
