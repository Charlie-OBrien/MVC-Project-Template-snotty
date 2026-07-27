using Brawndo_Components.Exceptions;
using Brawndo_Components.Interfaces;
using Brawndo_Components.Models;

namespace Brawndo_Components.Services
{
    public class OfficeAssignmentService : IOfficeAssignmentService
    {
        private const int LocationMaxLength = 50;

        private readonly IOfficeAssignmentRepository _officeAssignments;

        public OfficeAssignmentService(IOfficeAssignmentRepository officeAssignments)
        {
            _officeAssignments = officeAssignments;
        }

        public async Task<byte[]> AssignOfficeAsync(OfficeAssignment assignment)
        {
            Validate(assignment);

            var rowVersion = await _officeAssignments.InsertOfficeAssignmentAsync(assignment);

            // The proc only returns a timestamp when a row was actually inserted.
            if (rowVersion is null)
                throw new InvalidOperationException(
                    $"Office assignment for instructor {assignment.InstructorID} could not be created.");

            return rowVersion;
        }

        public async Task<byte[]> RelocateAsync(OfficeAssignment assignment)
        {
            Validate(assignment);

            if (assignment.Timestamp.Length == 0)
                throw new ArgumentException(
                    "The original row version is required to detect concurrent edits.", nameof(assignment));

            var rowVersion = await _officeAssignments.UpdateOfficeAssignmentAsync(assignment);

            // UpdateOfficeAssignment matches on the original timestamp, so a null result
            // means someone else changed or removed the row after we read it.
            if (rowVersion is null)
                throw new ConcurrencyException(
                    $"The office assignment for instructor {assignment.InstructorID} was changed by someone else. Reload and try again.");

            return rowVersion;
        }

        public Task RemoveAssignmentAsync(int instructorId)
        {
            if (instructorId <= 0)
                throw new ArgumentException("A valid InstructorID is required.", nameof(instructorId));

            return _officeAssignments.DeleteOfficeAssignmentAsync(instructorId);
        }

        private static void Validate(OfficeAssignment assignment)
        {
            if (assignment is null)
                throw new ArgumentNullException(nameof(assignment));

            if (assignment.InstructorID <= 0)
                throw new ArgumentException("A valid InstructorID is required.", nameof(assignment));

            if (string.IsNullOrWhiteSpace(assignment.Location))
                throw new ArgumentException("Location is required.", nameof(assignment));

            if (assignment.Location.Length > LocationMaxLength)
                throw new ArgumentException(
                    $"Location cannot exceed {LocationMaxLength} characters.", nameof(assignment));
        }
    }
}
