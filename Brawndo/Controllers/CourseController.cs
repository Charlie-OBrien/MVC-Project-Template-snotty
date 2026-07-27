using Brawndo_Components.Interfaces;
using Brawndo_Components.Models;
using Microsoft.AspNetCore.Mvc;

namespace Brawndo.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly IDepartmentService _departmentService;
        private readonly ILogger<CourseController> _logger;

        public CourseController(ICourseService courseService, IDepartmentService departmentService, ILogger<CourseController> logger)
        {
            _courseService = courseService;
            _departmentService = departmentService;
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
                _logger.LogError(ex, "Error retrieving courses");
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<IActionResult> ByDepartment(int departmentId)
        {
            try
            {
                var department = await _departmentService.GetDepartmentAsync(departmentId);
                if (department == null)
                    return NotFound();

                var courses = await _courseService.GetCoursesByDepartmentAsync(departmentId);
                ViewData["Department"] = department;
                return View(courses);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving courses for department {DepartmentId}", departmentId);
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var course = await _courseService.GetCourseAsync(id);
                if (course == null)
                    return NotFound();

                var department = await _departmentService.GetDepartmentAsync(course.DepartmentID);
                ViewData["Department"] = department;
                return View(course);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving course {CourseId}", id);
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<IActionResult> Create()
        {
            try
            {
                var departments = await _departmentService.GetDepartmentsAsync();
                ViewData["Departments"] = departments;
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading departments for course creation");
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Credits,DepartmentID")] Course course)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var courseId = await _courseService.AddCourseAsync(course);
                    return RedirectToAction(nameof(Details), new { id = courseId });
                }

                var departments = await _departmentService.GetDepartmentsAsync();
                ViewData["Departments"] = departments;
                return View(course);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating course");
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var course = await _courseService.GetCourseAsync(id);
                if (course == null)
                    return NotFound();

                var departments = await _departmentService.GetDepartmentsAsync();
                ViewData["Departments"] = departments;
                return View(course);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading course {CourseId} for editing", id);
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CourseID,Title,Credits,DepartmentID")] Course course)
        {
            if (id != course.CourseID)
                return BadRequest();

            try
            {
                if (ModelState.IsValid)
                {
                    await _courseService.UpdateCourseAsync(course);
                    return RedirectToAction(nameof(Details), new { id = course.CourseID });
                }

                var departments = await _departmentService.GetDepartmentsAsync();
                ViewData["Departments"] = departments;
                return View(course);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating course {CourseId}", id);
                return RedirectToAction("Error", "Home");
            }
        }
    }
}
