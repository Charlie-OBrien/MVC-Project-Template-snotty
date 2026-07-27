using Brawndo_Components.Interfaces;

namespace Brawndo_Components.Services
{
    public class CourseInstructorService : ICourseInstructorService
    {
        private readonly ICourseInstructorRepository _courseInstructors;
        private readonly IPersonService _personService;

        public CourseInstructorService(ICourseInstructorRepository courseInstructors, IPersonService personService)
        {
            _courseInstructors = courseInstructors;
            _personService = personService;
        }

        public async Task<List<Models.Person>> GetInstructorsForCourseAsync(int courseId)
        {
            if (courseId <= 0)
                throw new ArgumentException("A valid CourseID is required.", nameof(courseId));

            var assignments = await _courseInstructors.GetCourseInstructorsAsync(courseId);
            var instructors = new List<Models.Person>();

            foreach (var assignment in assignments)
            {
                var instructor = await _personService.GetPersonAsync(assignment.PersonID);
                if (instructor != null)
                    instructors.Add(instructor);
            }

            return instructors;
        }

        public Task RemoveInstructorFromCourseAsync(int courseId, int personId)
        {
            if (courseId <= 0)
                throw new ArgumentException("A valid CourseID is required.", nameof(courseId));

            if (personId <= 0)
                throw new ArgumentException("A valid PersonID is required.", nameof(personId));

            return _courseInstructors.DeleteCourseInstructorAsync(courseId, personId);
        }

        public Task RemoveInstructorFromAllCoursesAsync(int personId)
        {
            if (personId <= 0)
                throw new ArgumentException("A valid PersonID is required.", nameof(personId));

            return _courseInstructors.DeleteCourseInstructorsByPersonAsync(personId);
        }
    }
}
