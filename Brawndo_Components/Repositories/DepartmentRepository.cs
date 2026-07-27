using Brawndo_Components.DatabaseConn;
using Brawndo_Components.Interfaces;
using Brawndo_Components.Models;
using Microsoft.Data.SqlClient;

namespace Brawndo_Components.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ISchoolDatabaseConnection _dbConnection;

        public DepartmentRepository(ISchoolDatabaseConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<string?> GetDepartmentNameAsync(int departmentID)
        {
            using var connection = _dbConnection.CreateConnection();
            using var command = new SqlCommand("GetDepartmentName", connection)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@ID", departmentID);
            var nameParam = command.Parameters.Add("@Name", System.Data.SqlDbType.NVarChar, 50);
            nameParam.Direction = System.Data.ParameterDirection.Output;

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();

            return nameParam.Value != DBNull.Value ? (string)nameParam.Value : null;
        }

        public async Task<Department?> GetDepartmentAsync(int departmentID)
        {
            using var connection = _dbConnection.CreateConnection();
            using var command = new SqlCommand("GetDepartment", connection)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@DepartmentID", departmentID);

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new Department
                {
                    DepartmentID = (int)reader["DepartmentID"],
                    Name = (string)reader["Name"],
                    Budget = (decimal)reader["Budget"],
                    StartDate = (DateTime)reader["StartDate"],
                    Administrator = reader["Administrator"] == DBNull.Value ? null : (int)reader["Administrator"]
                };
            }
            return null;
        }

        public async Task<List<Department>> GetDepartmentsAsync()
        {
            var departments = new List<Department>();

            using var connection = _dbConnection.CreateConnection();
            using var command = new SqlCommand("GetDepartments", connection)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                departments.Add(new Department
                {
                    DepartmentID = (int)reader["DepartmentID"],
                    Name = (string)reader["Name"],
                    Budget = (decimal)reader["Budget"],
                    StartDate = (DateTime)reader["StartDate"],
                    Administrator = reader["Administrator"] == DBNull.Value ? null : (int)reader["Administrator"]
                });
            }

            return departments;
        }

        public async Task<int> InsertDepartmentAsync(Department department)
        {
            using var connection = _dbConnection.CreateConnection();
            using var command = new SqlCommand("InsertDepartment", connection)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@Name", department.Name);
            command.Parameters.AddWithValue("@Budget", department.Budget);
            command.Parameters.AddWithValue("@StartDate", department.StartDate);
            command.Parameters.AddWithValue("@Administrator", department.Administrator ?? (object)DBNull.Value);

            await connection.OpenAsync();
            var result = await command.ExecuteScalarAsync();
            return result != null ? Convert.ToInt32(result) : 0;
        }

        public async Task UpdateDepartmentAsync(Department department)
        {
            using var connection = _dbConnection.CreateConnection();
            using var command = new SqlCommand("UpdateDepartment", connection)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@DepartmentID", department.DepartmentID);
            command.Parameters.AddWithValue("@Name", department.Name);
            command.Parameters.AddWithValue("@Budget", department.Budget);
            command.Parameters.AddWithValue("@StartDate", department.StartDate);
            command.Parameters.AddWithValue("@Administrator", department.Administrator ?? (object)DBNull.Value);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }
    }
}
