using System.Data;

namespace Template.DataLayer
{
    /// <summary>
    /// Minimal interface to describe our connection factories.
    /// </summary>
    public interface IConnectionFactory
    {
        IDbConnection GetConnection();
    }
}
