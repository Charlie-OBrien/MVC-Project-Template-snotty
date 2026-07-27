using Brawndo_Components.DatabaseConn;
using Brawndo_Components.Interfaces;
using Brawndo_Components.Models;
using Microsoft.Data.SqlClient;

namespace Brawndo_Components.Repositories
{
    public class CourseInstructorRepository : ICourseInstructorRepository
    {
        private readonly ISchoolDatabaseConnection _dbConnection;

        public CourseInstructorRepository(ISchoolDatabaseConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task DeleteCourseInstructorAsync(int courseId, int personId)
        {
            using var connection = _dbConnection.CreateConnection();
            using var command = new SqlCommand("DeleteCourseInstructor", connection)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@CourseID", courseId);
            command.Parameters.AddWithValue("@PersonID", personId);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }

        public async Task DeleteCourseInstructorsByPersonAsync(int personId)
        {
            using var connection = _dbConnection.CreateConnection();
            using var command = new SqlCommand("DeleteCourseInstructorsByPerson", connection)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@PersonID", personId);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }

        public async Task<int> InsertCourseInstructorAsync(CourseInstructor instructor)
        {
            using var connection = _dbConnection.CreateConnection();
            using var command = new SqlCommand("InsertCourseInstructor", connection)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@CourseID", instructor.CourseID);
            command.Parameters.AddWithValue("@PersonID", instructor.PersonID);

            await connection.OpenAsync();
            var result = await command.ExecuteScalarAsync();
            return result != null ? Convert.ToInt32(result) : 0;
        }

        public async Task UpdateCourseInstructorAsync(CourseInstructor instructor)
        {
            using var connection = _dbConnection.CreateConnection();
            using var command = new SqlCommand("UpdateCourseInstructor", connection)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@CourseID", instructor.CourseID);
            command.Parameters.AddWithValue("@PersonID", instructor.PersonID);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }

        public async Task<CourseInstructor?> GetCourseInstructorAsync(int courseId, int personId)
        {
            using var connection = _dbConnection.CreateConnection();
            using var command = new SqlCommand("GetCourseInstructor", connection)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@CourseID", courseId);
            command.Parameters.AddWithValue("@PersonID", personId);

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new CourseInstructor
                {
                    CourseID = (int)reader["CourseID"],
                    PersonID = (int)reader["PersonID"]
                };
            }
            return null;
        }

        public async Task<List<CourseInstructor>> GetCourseInstructorsAsync(int courseId)
        {
            var instructors = new List<CourseInstructor>();

            using var connection = _dbConnection.CreateConnection();
            using var command = new SqlCommand("GetCourseInstructors", connection)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@CourseID", courseId);

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                instructors.Add(new CourseInstructor
                {
                    CourseID = (int)reader["CourseID"],
                    PersonID = (int)reader["PersonID"]
                });
            }

            return instructors;
        }
    }
}
