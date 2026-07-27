using Brawndo_Components.Interfaces;
using Brawndo_Components.Models;
using Microsoft.AspNetCore.Mvc;

namespace Brawndo.Controllers
{
    public class CourseInstructorController : Controller
    {
        private readonly ICourseInstructorService _courseInstructorService;
        private readonly ICourseService _courseService;
        private readonly IPersonService _personService;
        private readonly ILogger<CourseInstructorController> _logger;

        public CourseInstructorController(ICourseInstructorService courseInstructorService, ICourseService courseService,
            IPersonService personService, ILogger<CourseInstructorController> logger)
        {
            _courseInstructorService = courseInstructorService;
            _courseService = courseService;
            _personService = personService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var courses = await _courseService.GetCoursesAsync();
                return View(courses);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving courses for instructor assignments");
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<IActionResult> ByCourse(int courseId)
        {
            try
            {
                var course = await _courseService.GetCourseAsync(courseId);
                if (course == null)
                    return NotFound();

                var instructors = await _courseInstructorService.GetInstructorsForCourseAsync(courseId);
                ViewData["Course"] = course;
                ViewData["Instructors"] = instructors;
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving instructors for course {CourseId}", courseId);
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<IActionResult> ByInstructor(int instructorId)
        {
            try
            {
                var instructor = await _personService.GetPersonAsync(instructorId);
                if (instructor == null)
                    return NotFound();

                ViewData["Instructor"] = instructor;
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving courses for instructor {InstructorId}", instructorId);
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<IActionResult> Create()
        {
            try
            {
                var instructors = await _personService.GetPeopleByDiscriminatorAsync("Instructor");
                var courses = await _courseService.GetCoursesAsync();
                ViewData["Instructors"] = instructors;
                ViewData["Courses"] = courses;
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading data for course instructor assignment creation");
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CourseID,PersonID")] CourseInstructor courseInstructor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return RedirectToAction(nameof(ByCourse), new { courseId = courseInstructor.CourseID });
                }

                var instructors = await _personService.GetPeopleByDiscriminatorAsync("Instructor");
                var courses = await _courseService.GetCoursesAsync();
                ViewData["Instructors"] = instructors;
                ViewData["Courses"] = courses;
                return View(courseInstructor);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating course instructor assignment");
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int courseId, int personId)
        {
            try
            {
                await _courseInstructorService.RemoveInstructorFromCourseAsync(courseId, personId);
                return RedirectToAction(nameof(ByCourse), new { courseId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error removing instructor {PersonId} from course {CourseId}", personId, courseId);
                return RedirectToAction("Error", "Home");
            }
        }
    }
}
