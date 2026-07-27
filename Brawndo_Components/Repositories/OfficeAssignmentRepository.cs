using Brawndo_Components.DatabaseConn;
using Brawndo_Components.Interfaces;
using Brawndo_Components.Models;
using Microsoft.Data.SqlClient;

namespace Brawndo_Components.Repositories
{
    public class OfficeAssignmentRepository : IOfficeAssignmentRepository
    {
        private readonly ISchoolDatabaseConnection _dbConnection;

        public OfficeAssignmentRepository(ISchoolDatabaseConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<byte[]?> InsertOfficeAssignmentAsync(OfficeAssignment officeAssignment)
        {
            using var connection = _dbConnection.CreateConnection();
            using var command = new SqlCommand("InsertOfficeAssignment", connection)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@InstructorID", officeAssignment.InstructorID);
            command.Parameters.AddWithValue("@Location", officeAssignment.Location);

            await connection.OpenAsync();
            var result = await command.ExecuteScalarAsync();
            return result != null ? (byte[])result : null;
        }

        public async Task<byte[]?> UpdateOfficeAssignmentAsync(OfficeAssignment officeAssignment)
        {
            using var connection = _dbConnection.CreateConnection();
            using var command = new SqlCommand("UpdateOfficeAssignment", connection)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@InstructorID", officeAssignment.InstructorID);
            command.Parameters.AddWithValue("@Location", officeAssignment.Location);
            command.Parameters.AddWithValue("@OrigTimestamp", officeAssignment.Timestamp);

            await connection.OpenAsync();
            var result = await command.ExecuteScalarAsync();
            return result != null ? (byte[])result : null;
        }

        public async Task DeleteOfficeAssignmentAsync(int instructorID)
        {
            using var connection = _dbConnection.CreateConnection();
            using var command = new SqlCommand("DeleteOfficeAssignment", connection)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@InstructorID", instructorID);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }

        public async Task<OfficeAssignment?> GetOfficeAssignmentAsync(int instructorID)
        {
            using var connection = _dbConnection.CreateConnection();
            using var command = new SqlCommand("GetOfficeAssignment", connection)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@InstructorID", instructorID);

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new OfficeAssignment
                {
                    InstructorID = (int)reader["InstructorID"],
                    Location = (string)reader["Location"],
                    Timestamp = (byte[])reader["Timestamp"]
                };
            }
            return null;
        }

        public async Task<List<OfficeAssignment>> GetOfficeAssignmentsAsync()
        {
            var assignments = new List<OfficeAssignment>();

            using var connection = _dbConnection.CreateConnection();
            using var command = new SqlCommand("GetOfficeAssignments", connection)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                assignments.Add(new OfficeAssignment
                {
                    InstructorID = (int)reader["InstructorID"],
                    Location = (string)reader["Location"],
                    Timestamp = (byte[])reader["Timestamp"]
                });
            }

            return assignments;
        }
    }
}
