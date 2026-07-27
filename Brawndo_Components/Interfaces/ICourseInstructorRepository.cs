using Brawndo_Components.Models;

namespace Brawndo_Components.Interfaces
{
    public interface ICourseInstructorRepository
    {
        Task<int> InsertCourseInstructorAsync(CourseInstructor instructor);
        Task UpdateCourseInstructorAsync(CourseInstructor instructor);
        Task<CourseInstructor?> GetCourseInstructorAsync(int courseId, int personId);
        Task<List<CourseInstructor>> GetCourseInstructorsAsync(int courseId);
        Task DeleteCourseInstructorAsync(int courseId, int personId);
        Task DeleteCourseInstructorsByPersonAsync(int personId);
    }
}
