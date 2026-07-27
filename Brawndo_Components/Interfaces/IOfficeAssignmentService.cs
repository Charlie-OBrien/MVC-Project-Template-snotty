using Brawndo_Components.Models;

namespace Brawndo_Components.Interfaces
{
    public interface IOfficeAssignmentService
    {
        /// <summary>Assigns an office and returns the new row version.</summary>
        Task<byte[]> AssignOfficeAsync(OfficeAssignment assignment);

        /// <summary>
        /// Moves an instructor to a new office. Throws
        /// <see cref="Exceptions.ConcurrencyException"/> if the row changed since it was read.
        /// </summary>
        Task<byte[]> RelocateAsync(OfficeAssignment assignment);

        Task RemoveAssignmentAsync(int instructorId);
    }
}
