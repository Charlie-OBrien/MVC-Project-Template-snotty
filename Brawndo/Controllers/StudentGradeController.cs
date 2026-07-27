using Brawndo_Components.Interfaces;
using Brawndo_Components.Models;
using Microsoft.AspNetCore.Mvc;

namespace Brawndo.Controllers
{
    public class StudentGradeController : Controller
    {
        private readonly IStudentGradeService _gradeService;
        private readonly ICourseService _courseService;
        private readonly IPersonService _personService;
        private readonly ILogger<StudentGradeController> _logger;

        public StudentGradeController(IStudentGradeService gradeService, ICourseService courseService,
            IPersonService personService, ILogger<StudentGradeController> logger)
        {
            _gradeService = gradeService;
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
                _logger.LogError(ex, "Error retrieving courses for grade listing");
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

                ViewData["Course"] = course;
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving grades for course {CourseId}", courseId);
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<IActionResult> ByStudent(int studentId)
        {
            try
            {
                var student = await _personService.GetPersonAsync(studentId);
                if (student == null)
                    return NotFound();

                var grades = await _gradeService.GetGradesAsync(studentId);
                var gpa = await _gradeService.GetGradePointAverageAsync(studentId);

                ViewData["Student"] = student;
                ViewData["GPA"] = gpa;
                return View(grades);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving grades for student {StudentId}", studentId);
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<IActionResult> Create()
        {
            try
            {
                var students = await _personService.GetPeopleByDiscriminatorAsync("Student");
                var courses = await _courseService.GetCoursesAsync();
                ViewData["Students"] = students;
                ViewData["Courses"] = courses;
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading data for grade creation");
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CourseID,StudentID,Grade")] StudentGrade grade)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return RedirectToAction(nameof(ByStudent), new { studentId = grade.StudentID });
                }

                var students = await _personService.GetPeopleByDiscriminatorAsync("Student");
                var courses = await _courseService.GetCoursesAsync();
                ViewData["Students"] = students;
                ViewData["Courses"] = courses;
                return View(grade);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating grade");
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<IActionResult> Edit(int enrollmentId)
        {
            try
            {
                var students = await _personService.GetPeopleByDiscriminatorAsync("Student");
                var courses = await _courseService.GetCoursesAsync();
                ViewData["Students"] = students;
                ViewData["Courses"] = courses;
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading grade {EnrollmentId} for editing", enrollmentId);
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int enrollmentId, [Bind("EnrollmentID,CourseID,StudentID,Grade")] StudentGrade grade)
        {
            if (enrollmentId != grade.EnrollmentID)
                return BadRequest();

            try
            {
                if (ModelState.IsValid)
                {
                    return RedirectToAction(nameof(ByStudent), new { studentId = grade.StudentID });
                }

                var students = await _personService.GetPeopleByDiscriminatorAsync("Student");
                var courses = await _courseService.GetCoursesAsync();
                ViewData["Students"] = students;
                ViewData["Courses"] = courses;
                return View(grade);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating grade {EnrollmentId}", enrollmentId);
                return RedirectToAction("Error", "Home");
            }
        }
    }
}
