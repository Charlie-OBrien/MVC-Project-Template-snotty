using Brawndo_Components.Interfaces;
using Brawndo_Components.Models;

namespace Brawndo_Components.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departments;

        public DepartmentService(IDepartmentRepository departments)
        {
            _departments = departments;
        }

        public Task<string?> GetDepartmentNameAsync(int departmentId)
        {
            if (departmentId <= 0)
                throw new ArgumentException("A valid DepartmentID is required.", nameof(departmentId));

            return _departments.GetDepartmentNameAsync(departmentId);
        }

        public Task<Department?> GetDepartmentAsync(int departmentId)
        {
            if (departmentId <= 0)
                throw new ArgumentException("A valid DepartmentID is required.", nameof(departmentId));

            return _departments.GetDepartmentAsync(departmentId);
        }

        public Task<List<Department>> GetDepartmentsAsync()
        {
            return _departments.GetDepartmentsAsync();
        }

        public Task<int> AddDepartmentAsync(Department department)
        {
            if (department == null)
                throw new ArgumentNullException(nameof(department));

            if (string.IsNullOrWhiteSpace(department.Name))
                throw new ArgumentException("Department name is required.", nameof(department.Name));

            if (department.Budget < 0)
                throw new ArgumentException("Department budget cannot be negative.", nameof(department.Budget));

            if (department.StartDate == default)
                throw new ArgumentException("A valid start date is required.", nameof(department.StartDate));

            return _departments.InsertDepartmentAsync(department);
        }

        public Task UpdateDepartmentAsync(Department department)
        {
            if (department == null)
                throw new ArgumentNullException(nameof(department));

            if (department.DepartmentID <= 0)
                throw new ArgumentException("A valid DepartmentID is required.", nameof(department.DepartmentID));

            if (string.IsNullOrWhiteSpace(department.Name))
                throw new ArgumentException("Department name is required.", nameof(department.Name));

            if (department.Budget < 0)
                throw new ArgumentException("Department budget cannot be negative.", nameof(department.Budget));

            if (department.StartDate == default)
                throw new ArgumentException("A valid start date is required.", nameof(department.StartDate));

            return _departments.UpdateDepartmentAsync(department);
        }
    }
}
