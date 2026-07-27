using Brawndo_Components.Interfaces;
using Brawndo_Components.Models;

namespace Brawndo_Components.Services
{
    public class StudentGradeService : IStudentGradeService
    {
        private readonly IStudentGradeRepository _grades;

        public StudentGradeService(IStudentGradeRepository grades)
        {
            _grades = grades;
        }

        public Task<List<StudentGrade>> GetGradesAsync(int studentId)
        {
            if (studentId <= 0)
                throw new ArgumentException("A valid StudentID is required.", nameof(studentId));

            return _grades.GetStudentGradesAsync(studentId);
        }

        public async Task<decimal?> GetGradePointAverageAsync(int studentId)
        {
            var grades = await GetGradesAsync(studentId);

            // Enrollments with no grade yet are in progress and must not drag the average down.
            var graded = grades
                .Where(g => g.Grade.HasValue)
                .Select(g => g.Grade!.Value)
                .ToList();

            if (graded.Count == 0)
                return null;

            return Math.Round(graded.Average(), 2, MidpointRounding.AwayFromZero);
        }
    }
}
