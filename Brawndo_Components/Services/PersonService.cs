using Brawndo_Components.Interfaces;
using Brawndo_Components.Models;

namespace Brawndo_Components.Services
{
    public class PersonService : IPersonService
    {
        private const int NameMaxLength = 50;

        public const string StudentDiscriminator = "Student";
        public const string InstructorDiscriminator = "Instructor";

        private readonly IPersonRepository _people;
        private readonly IOfficeAssignmentRepository _officeAssignments;

        public PersonService(IPersonRepository people, IOfficeAssignmentRepository officeAssignments)
        {
            _people = people;
            _officeAssignments = officeAssignments;
        }

        public Task<int> EnrollStudentAsync(Person student)
        {
            ValidateName(student);

            if (student.EnrollmentDate is null)
                throw new ArgumentException("A student requires an enrollment date.", nameof(student));

            student.Discriminator = StudentDiscriminator;
            student.HireDate = null;

            return _people.InsertPersonAsync(student);
        }

        public Task<int> HireInstructorAsync(Person instructor)
        {
            ValidateName(instructor);

            if (instructor.HireDate is null)
                throw new ArgumentException("An instructor requires a hire date.", nameof(instructor));

            instructor.Discriminator = InstructorDiscriminator;
            instructor.EnrollmentDate = null;

            return _people.InsertPersonAsync(instructor);
        }

        public Task UpdatePersonAsync(Person person)
        {
            if (person.PersonID <= 0)
                throw new ArgumentException("A valid PersonID is required to update.", nameof(person));

            ValidateName(person);

            if (string.IsNullOrWhiteSpace(person.Discriminator))
                throw new ArgumentException("Discriminator is required.", nameof(person));

            return _people.UpdatePersonAsync(person);
        }

        public async Task RemovePersonAsync(int personId)
        {
            if (personId <= 0)
                throw new ArgumentException("A valid PersonID is required to delete.", nameof(personId));

            await _people.DeletePersonAndDependentsAsync(personId);
        }

        public Task<Person?> GetPersonAsync(int personId)
        {
            if (personId <= 0)
                throw new ArgumentException("A valid PersonID is required.", nameof(personId));

            return _people.GetPersonAsync(personId);
        }

        public Task<List<Person>> GetPeopleAsync()
        {
            return _people.GetPeopleAsync();
        }

        public Task<List<Person>> GetPeopleByDiscriminatorAsync(string discriminator)
        {
            if (string.IsNullOrWhiteSpace(discriminator))
                throw new ArgumentException("Discriminator is required.", nameof(discriminator));

            return _people.GetPeopleByDiscriminatorAsync(discriminator);
        }

        private static void ValidateName(Person person)
        {
            if (person is null)
                throw new ArgumentNullException(nameof(person));

            if (string.IsNullOrWhiteSpace(person.LastName))
                throw new ArgumentException("Last name is required.", nameof(person));

            if (string.IsNullOrWhiteSpace(person.FirstName))
                throw new ArgumentException("First name is required.", nameof(person));

            if (person.LastName.Length > NameMaxLength)
                throw new ArgumentException($"Last name cannot exceed {NameMaxLength} characters.", nameof(person));

            if (person.FirstName.Length > NameMaxLength)
                throw new ArgumentException($"First name cannot exceed {NameMaxLength} characters.", nameof(person));
        }
    }
}
