using Brawndo_Components.DatabaseConn;
using Brawndo_Components.Interfaces;
using Brawndo_Components.Models;
using Microsoft.Data.SqlClient;

namespace Brawndo_Components.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly ISchoolDatabaseConnection _dbConnection;

        public PersonRepository(ISchoolDatabaseConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<int> InsertPersonAsync(Person person)
        {
            using var connection = _dbConnection.CreateConnection();
            using var command = new SqlCommand("InsertPerson", connection)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@LastName", person.LastName);
            command.Parameters.AddWithValue("@FirstName", person.FirstName);
            command.Parameters.AddWithValue("@HireDate", person.HireDate ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@EnrollmentDate", person.EnrollmentDate ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@Discriminator", person.Discriminator);

            await connection.OpenAsync();
            var result = await command.ExecuteScalarAsync();
            return result != null ? Convert.ToInt32(result) : 0;
        }

        public async Task UpdatePersonAsync(Person person)
        {
            using var connection = _dbConnection.CreateConnection();
            using var command = new SqlCommand("UpdatePerson", connection)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@PersonID", person.PersonID);
            command.Parameters.AddWithValue("@LastName", person.LastName);
            command.Parameters.AddWithValue("@FirstName", person.FirstName);
            command.Parameters.AddWithValue("@HireDate", person.HireDate ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@EnrollmentDate", person.EnrollmentDate ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@Discriminator", person.Discriminator);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }

        public async Task DeletePersonAsync(int personID)
        {
            using var connection = _dbConnection.CreateConnection();
            using var command = new SqlCommand("DeletePerson", connection)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@PersonID", personID);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }

        public async Task DeletePersonAndDependentsAsync(int personID)
        {
            using var connection = _dbConnection.CreateConnection();
            using var command = new SqlCommand("DeletePersonAndDependents", connection)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@PersonID", personID);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }

        public async Task<Person?> GetPersonAsync(int personID)
        {
            using var connection = _dbConnection.CreateConnection();
            using var command = new SqlCommand("GetPerson", connection)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@PersonID", personID);

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new Person
                {
                    PersonID = (int)reader["PersonID"],
                    LastName = (string)reader["LastName"],
                    FirstName = (string)reader["FirstName"],
                    HireDate = reader["HireDate"] == DBNull.Value ? null : (DateTime)reader["HireDate"],
                    EnrollmentDate = reader["EnrollmentDate"] == DBNull.Value ? null : (DateTime)reader["EnrollmentDate"],
                    Discriminator = (string)reader["Discriminator"]
                };
            }
            return null;
        }

        public async Task<List<Person>> GetPeopleAsync()
        {
            var people = new List<Person>();

            using var connection = _dbConnection.CreateConnection();
            using var command = new SqlCommand("GetPeople", connection)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                people.Add(new Person
                {
                    PersonID = (int)reader["PersonID"],
                    LastName = (string)reader["LastName"],
                    FirstName = (string)reader["FirstName"],
                    HireDate = reader["HireDate"] == DBNull.Value ? null : (DateTime)reader["HireDate"],
                    EnrollmentDate = reader["EnrollmentDate"] == DBNull.Value ? null : (DateTime)reader["EnrollmentDate"],
                    Discriminator = (string)reader["Discriminator"]
                });
            }

            return people;
        }

        public async Task<List<Person>> GetPeopleByDiscriminatorAsync(string discriminator)
        {
            var people = new List<Person>();

            using var connection = _dbConnection.CreateConnection();
            using var command = new SqlCommand("GetPeopleByDiscriminator", connection)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@Discriminator", discriminator);

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                people.Add(new Person
                {
                    PersonID = (int)reader["PersonID"],
                    LastName = (string)reader["LastName"],
                    FirstName = (string)reader["FirstName"],
                    HireDate = reader["HireDate"] == DBNull.Value ? null : (DateTime)reader["HireDate"],
                    EnrollmentDate = reader["EnrollmentDate"] == DBNull.Value ? null : (DateTime)reader["EnrollmentDate"],
                    Discriminator = (string)reader["Discriminator"]
                });
            }

            return people;
        }
    }
}
