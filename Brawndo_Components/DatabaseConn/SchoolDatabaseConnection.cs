using Microsoft.Data.SqlClient;

namespace Brawndo_Components.DatabaseConn
{
    public class SchoolDatabaseConnection : ISchoolDatabaseConnection
    {
        public SchoolDatabaseConnection(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentException("A School database connection string is required.", nameof(connectionString));

            ConnectionString = connectionString;
        }

        public string ConnectionString { get; }

        public SqlConnection CreateConnection() => new SqlConnection(ConnectionString);
    }
}
