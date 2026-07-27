using Brawndo_Components.Models;

namespace Brawndo_Components.Interfaces
{
    public interface IStudentGradeRepository
    {
        Task<List<StudentGrade>> GetStudentGradesAsync(int studentID);
        Task<StudentGrade?> GetStudentGradeAsync(int enrollmentID);
        Task<List<StudentGrade>> GetCourseGradesAsync(int courseID);
        Task<int> InsertStudentGradeAsync(StudentGrade grade);
        Task UpdateStudentGradeAsync(StudentGrade grade);
        Task DeleteStudentGradeAsync(int enrollmentID);
        Task DeleteStudentGradesByStudentAsync(int studentID);
    }
}
