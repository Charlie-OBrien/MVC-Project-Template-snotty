using Brawndo_Components.Models;

namespace Brawndo_Components.Interfaces
{
    public interface IOfficeAssignmentRepository
    {
        Task<byte[]?> InsertOfficeAssignmentAsync(OfficeAssignment officeAssignment);
        Task<byte[]?> UpdateOfficeAssignmentAsync(OfficeAssignment officeAssignment);
        Task DeleteOfficeAssignmentAsync(int instructorID);
        Task<OfficeAssignment?> GetOfficeAssignmentAsync(int instructorID);
        Task<List<OfficeAssignment>> GetOfficeAssignmentsAsync();
    }
}
