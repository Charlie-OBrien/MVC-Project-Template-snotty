using Brawndo_Components.DatabaseConn;
using Brawndo_Components.Interfaces;
using Brawndo_Components.Models;
using Microsoft.Data.SqlClient;

namespace Brawndo_Components.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly ISchoolDatabaseConnection _dbConnection;

        public CourseRepository(ISchoolDatabaseConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<Course?> GetCourseAsync(int courseId)
        {
            using var connection = _dbConnection.CreateConnection();
            using var command = new SqlCommand("GetCourse", connection)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@CourseID", courseId);

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new Course
                {
                    CourseID = (int)reader["CourseID"],
                    Title = (string)reader["Title"],
                    Credits = (int)reader["Credits"],
                    DepartmentID = (int)reader["DepartmentID"]
                };
            }
            return null;
        }

        public async Task<List<Course>> GetCoursesAsync()
        {
            var courses = new List<Course>();

            using var connection = _dbConnection.CreateConnection();
            using var command = new SqlCommand("GetCourses", connection)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                courses.Add(new Course
                {
                    CourseID = (int)reader["CourseID"],
                    Title = (string)reader["Title"],
                    Credits = (int)reader["Credits"],
                    DepartmentID = (int)reader["DepartmentID"]
                });
            }

            return courses;
        }

        public async Task<List<Course>> GetCoursesByDepartmentAsync(int departmentId)
        {
            var courses = new List<Course>();

            using var connection = _dbConnection.CreateConnection();
            using var command = new SqlCommand("GetCoursesByDepartment", connection)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@DepartmentID", departmentId);

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                courses.Add(new Course
                {
                    CourseID = (int)reader["CourseID"],
                    Title = (string)reader["Title"],
                    Credits = (int)reader["Credits"],
                    DepartmentID = (int)reader["DepartmentID"]
                });
            }

            return courses;
        }

        public async Task<int> InsertCourseAsync(Course course)
        {
            using var connection = _dbConnection.CreateConnection();
            using var command = new SqlCommand("InsertCourse", connection)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@Title", course.Title);
            command.Parameters.AddWithValue("@Credits", course.Credits);
            command.Parameters.AddWithValue("@DepartmentID", course.DepartmentID);

            await connection.OpenAsync();
            var result = await command.ExecuteScalarAsync();
            return result != null ? Convert.ToInt32(result) : 0;
        }

        public async Task UpdateCourseAsync(Course course)
        {
            using var connection = _dbConnection.CreateConnection();
            using var command = new SqlCommand("UpdateCourse", connection)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@CourseID", course.CourseID);
            command.Parameters.AddWithValue("@Title", course.Title);
            command.Parameters.AddWithValue("@Credits", course.Credits);
            command.Parameters.AddWithValue("@DepartmentID", course.DepartmentID);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }
    }
}
