using Brawndo_Components.Interfaces;
using Brawndo_Components.Models;

namespace Brawndo_Components.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courses;

        public CourseService(ICourseRepository courses)
        {
            _courses = courses;
        }

        public Task<Course?> GetCourseAsync(int courseId)
        {
            if (courseId <= 0)
                throw new ArgumentException("A valid CourseID is required.", nameof(courseId));

            return _courses.GetCourseAsync(courseId);
        }

        public Task<List<Course>> GetCoursesAsync()
        {
            return _courses.GetCoursesAsync();
        }

        public Task<List<Course>> GetCoursesByDepartmentAsync(int departmentId)
        {
            if (departmentId <= 0)
                throw new ArgumentException("A valid DepartmentID is required.", nameof(departmentId));

            return _courses.GetCoursesByDepartmentAsync(departmentId);
        }

        public Task<int> AddCourseAsync(Course course)
        {
            if (course == null)
                throw new ArgumentNullException(nameof(course));

            if (string.IsNullOrWhiteSpace(course.Title))
                throw new ArgumentException("Course title is required.", nameof(course.Title));

            if (course.Credits <= 0)
                throw new ArgumentException("Course credits must be greater than 0.", nameof(course.Credits));

            if (course.DepartmentID <= 0)
                throw new ArgumentException("A valid DepartmentID is required.", nameof(course.DepartmentID));

            return _courses.InsertCourseAsync(course);
        }

        public Task UpdateCourseAsync(Course course)
        {
            if (course == null)
                throw new ArgumentNullException(nameof(course));

            if (course.CourseID <= 0)
                throw new ArgumentException("A valid CourseID is required.", nameof(course.CourseID));

            if (string.IsNullOrWhiteSpace(course.Title))
                throw new ArgumentException("Course title is required.", nameof(course.Title));

            if (course.Credits <= 0)
                throw new ArgumentException("Course credits must be greater than 0.", nameof(course.Credits));

            if (course.DepartmentID <= 0)
                throw new ArgumentException("A valid DepartmentID is required.", nameof(course.DepartmentID));

            return _courses.UpdateCourseAsync(course);
        }
    }
}
