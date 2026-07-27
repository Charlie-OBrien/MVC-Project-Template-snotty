using Brawndo_Components.Interfaces;
using Brawndo_Components.Models;
using Microsoft.AspNetCore.Mvc;

namespace Brawndo.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;
        private readonly ICourseService _courseService;
        private readonly ILogger<DepartmentController> _logger;

        public DepartmentController(IDepartmentService departmentService, ICourseService courseService, ILogger<DepartmentController> logger)
        {
            _departmentService = departmentService;
            _courseService = courseService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var departments = await _departmentService.GetDepartmentsAsync();
                return View(departments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving departments");
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var department = await _departmentService.GetDepartmentAsync(id);
                if (department == null)
                    return NotFound();

                var courses = await _courseService.GetCoursesByDepartmentAsync(id);
                ViewData["Courses"] = courses;
                return View(department);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving department {DepartmentId}", id);
                return RedirectToAction("Error", "Home");
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Budget,StartDate,Administrator")] Department department)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var departmentId = await _departmentService.AddDepartmentAsync(department);
                    return RedirectToAction(nameof(Details), new { id = departmentId });
                }

                return View(department);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating department");
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var department = await _departmentService.GetDepartmentAsync(id);
                if (department == null)
                    return NotFound();

                return View(department);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading department {DepartmentId} for editing", id);
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DepartmentID,Name,Budget,StartDate,Administrator")] Department department)
        {
            if (id != department.DepartmentID)
                return BadRequest();

            try
            {
                if (ModelState.IsValid)
                {
                    await _departmentService.UpdateDepartmentAsync(department);
                    return RedirectToAction(nameof(Details), new { id = department.DepartmentID });
                }

                return View(department);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating department {DepartmentId}", id);
                return RedirectToAction("Error", "Home");
            }
        }
    }
}
