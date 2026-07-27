using Brawndo_Components.Models;

namespace Brawndo_Components.Interfaces
{
    public interface IDepartmentService
    {
        /// <summary>Returns the department's name, or null when no such department exists.</summary>
        Task<string?> GetDepartmentNameAsync(int departmentId);

        Task<Department?> GetDepartmentAsync(int departmentId);
        Task<List<Department>> GetDepartmentsAsync();
        Task<int> AddDepartmentAsync(Department department);
        Task UpdateDepartmentAsync(Department department);
    }
}
