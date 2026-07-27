using Brawndo_Components.Models;

namespace Brawndo_Components.Interfaces
{
    public interface IPersonRepository
    {
        Task<int> InsertPersonAsync(Person person);
        Task UpdatePersonAsync(Person person);
        Task DeletePersonAsync(int personID);
        Task DeletePersonAndDependentsAsync(int personID);
        Task<Person?> GetPersonAsync(int personID);
        Task<List<Person>> GetPeopleAsync();
        Task<List<Person>> GetPeopleByDiscriminatorAsync(string discriminator);
    }
}
