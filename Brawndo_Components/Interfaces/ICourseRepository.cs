using Brawndo_Components.Models;

namespace Brawndo_Components.Interfaces
{
    public interface ICourseRepository
    {
        Task<Course?> GetCourseAsync(int courseId);
        Task<List<Course>> GetCoursesAsync();
        Task<List<Course>> GetCoursesByDepartmentAsync(int departmentId);
        Task<int> InsertCourseAsync(Course course);
        Task UpdateCourseAsync(Course course);
    }
}
