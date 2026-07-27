using Brawndo_Components.DatabaseConn;
using Brawndo_Components.Interfaces;
using Brawndo_Components.Models;
using Microsoft.Data.SqlClient;

namespace Brawndo_Components.Repositories
{
    public class StudentGradeRepository : IStudentGradeRepository
    {
        private readonly ISchoolDatabaseConnection _dbConnection;

        public StudentGradeRepository(ISchoolDatabaseConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<List<StudentGrade>> GetStudentGradesAsync(int studentID)
        {
            var grades = new List<StudentGrade>();

            using var connection = _dbConnection.CreateConnection();
            using var command = new SqlCommand("GetStudentGrades", connection)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@StudentID", studentID);

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                grades.Add(new StudentGrade
                {
                    EnrollmentID = (int)reader["EnrollmentID"],
                    CourseID = (int)reader["CourseID"],
                    StudentID = (int)reader["StudentID"],
                    Grade = reader["Grade"] == DBNull.Value ? null : (decimal)reader["Grade"]
                });
            }

            return grades;
        }

        public async Task<StudentGrade?> GetStudentGradeAsync(int enrollmentID)
        {
            using var connection = _dbConnection.CreateConnection();
            using var command = new SqlCommand("GetStudentGrade", connection)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@EnrollmentID", enrollmentID);

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new StudentGrade
                {
                    EnrollmentID = (int)reader["EnrollmentID"],
                    CourseID = (int)reader["CourseID"],
                    StudentID = (int)reader["StudentID"],
                    Grade = reader["Grade"] == DBNull.Value ? null : (decimal)reader["Grade"]
                };
            }
            return null;
        }

        public async Task<List<StudentGrade>> GetCourseGradesAsync(int courseID)
        {
            var grades = new List<StudentGrade>();

            using var connection = _dbConnection.CreateConnection();
            using var command = new SqlCommand("GetCourseGrades", connection)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@CourseID", courseID);

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                grades.Add(new StudentGrade
                {
                    EnrollmentID = (int)reader["EnrollmentID"],
                    CourseID = (int)reader["CourseID"],
                    StudentID = (int)reader["StudentID"],
                    Grade = reader["Grade"] == DBNull.Value ? null : (decimal)reader["Grade"]
                });
            }

            return grades;
        }

        public async Task DeleteStudentGradeAsync(int enrollmentID)
        {
            using var connection = _dbConnection.CreateConnection();
            using var command = new SqlCommand("DeleteStudentGrade", connection)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@EnrollmentID", enrollmentID);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }

        public async Task DeleteStudentGradesByStudentAsync(int studentID)
        {
            using var connection = _dbConnection.CreateConnection();
            using var command = new SqlCommand("DeleteStudentGradesByStudent", connection)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@StudentID", studentID);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }

        public async Task<int> InsertStudentGradeAsync(StudentGrade grade)
        {
            using var connection = _dbConnection.CreateConnection();
            using var command = new SqlCommand("InsertStudentGrade", connection)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@CourseID", grade.CourseID);
            command.Parameters.AddWithValue("@StudentID", grade.StudentID);
            command.Parameters.AddWithValue("@Grade", grade.Grade ?? (object)DBNull.Value);

            await connection.OpenAsync();
            var result = await command.ExecuteScalarAsync();
            return result != null ? Convert.ToInt32(result) : 0;
        }

        public async Task UpdateStudentGradeAsync(StudentGrade grade)
        {
            using var connection = _dbConnection.CreateConnection();
            using var command = new SqlCommand("UpdateStudentGrade", connection)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@EnrollmentID", grade.EnrollmentID);
            command.Parameters.AddWithValue("@Grade", grade.Grade ?? (object)DBNull.Value);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }
    }
}
