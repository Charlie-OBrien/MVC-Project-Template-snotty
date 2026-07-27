namespace Brawndo_Components.Interfaces
{
    public interface ICourseInstructorService
    {
        Task<List<Models.Person>> GetInstructorsForCourseAsync(int courseId);
        Task RemoveInstructorFromCourseAsync(int courseId, int personId);
        Task RemoveInstructorFromAllCoursesAsync(int personId);
    }
}
