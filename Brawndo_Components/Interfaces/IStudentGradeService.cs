using Brawndo_Components.Models;

namespace Brawndo_Components.Interfaces
{
    public interface IStudentGradeService
    {
        Task<List<StudentGrade>> GetGradesAsync(int studentId);

        /// <summary>
        /// Average of the student's graded enrollments, rounded to two places.
        /// Ungraded enrollments are excluded; returns null when nothing is graded yet.
        /// </summary>
        Task<decimal?> GetGradePointAverageAsync(int studentId);
    }
}
