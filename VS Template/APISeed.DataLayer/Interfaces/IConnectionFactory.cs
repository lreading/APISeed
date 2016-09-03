using System.Data;

namespace $safeprojectname$.Interfaces
{
    /// <summary>
    /// Minimal interface to describe our connection factories.
    /// </summary>
    public interface IConnectionFactory
    {
        IDbConnection GetConnection();
    }
}
