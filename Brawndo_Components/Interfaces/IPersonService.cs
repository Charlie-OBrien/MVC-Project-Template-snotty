using Brawndo_Components.Models;

namespace Brawndo_Components.Interfaces
{
    public interface IPersonService
    {
        /// <summary>Creates a student. Requires an enrollment date; hire date is cleared.</summary>
        Task<int> EnrollStudentAsync(Person student);

        /// <summary>Creates an instructor. Requires a hire date; enrollment date is cleared.</summary>
        Task<int> HireInstructorAsync(Person instructor);

        Task UpdatePersonAsync(Person person);

        /// <summary>Removes a person along with any office assignment that would block the delete.</summary>
        Task RemovePersonAsync(int personId);

        Task<Person?> GetPersonAsync(int personId);
        Task<List<Person>> GetPeopleAsync();
        Task<List<Person>> GetPeopleByDiscriminatorAsync(string discriminator);
    }
}
