using Brawndo_Components.Models;

namespace Brawndo_Components.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<string?> GetDepartmentNameAsync(int departmentID);
        Task<Department?> GetDepartmentAsync(int departmentID);
        Task<List<Department>> GetDepartmentsAsync();
        Task<int> InsertDepartmentAsync(Department department);
        Task UpdateDepartmentAsync(Department department);
    }
}
