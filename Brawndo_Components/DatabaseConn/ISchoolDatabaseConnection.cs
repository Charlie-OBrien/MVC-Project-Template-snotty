using Microsoft.Data.SqlClient;

namespace Brawndo_Components.DatabaseConn
{
    public interface ISchoolDatabaseConnection
    {
        string ConnectionString { get; }

        /// <summary>
        /// Creates a new, unopened connection. The caller owns it and must dispose it.
        /// </summary>
        SqlConnection CreateConnection();
    }
}
