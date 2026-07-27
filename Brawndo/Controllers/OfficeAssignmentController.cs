using Brawndo_Components.Interfaces;
using Brawndo_Components.Models;
using Microsoft.AspNetCore.Mvc;

namespace Brawndo.Controllers
{
    public class OfficeAssignmentController : Controller
    {
        private readonly IOfficeAssignmentService _officeAssignmentService;
        private readonly IPersonService _personService;
        private readonly ILogger<OfficeAssignmentController> _logger;

        public OfficeAssignmentController(IOfficeAssignmentService officeAssignmentService,
            IPersonService personService, ILogger<OfficeAssignmentController> logger)
        {
            _officeAssignmentService = officeAssignmentService;
            _personService = personService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var instructors = await _personService.GetPeopleByDiscriminatorAsync("Instructor");
                return View(instructors);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving instructors for office assignments");
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<IActionResult> Details(int instructorId)
        {
            try
            {
                var instructor = await _personService.GetPersonAsync(instructorId);
                if (instructor == null)
                    return NotFound();

                return View(instructor);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving office assignment for instructor {InstructorId}", instructorId);
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<IActionResult> Create()
        {
            try
            {
                var instructors = await _personService.GetPeopleByDiscriminatorAsync("Instructor");
                ViewData["Instructors"] = instructors;
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading data for office assignment creation");
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InstructorID,Location")] OfficeAssignment officeAssignment)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _officeAssignmentService.AssignOfficeAsync(officeAssignment);
                    return RedirectToAction(nameof(Details), new { instructorId = officeAssignment.InstructorID });
                }

                var instructors = await _personService.GetPeopleByDiscriminatorAsync("Instructor");
                ViewData["Instructors"] = instructors;
                return View(officeAssignment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating office assignment");
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<IActionResult> Edit(int instructorId)
        {
            try
            {
                var instructor = await _personService.GetPersonAsync(instructorId);
                if (instructor == null)
                    return NotFound();

                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading office assignment {InstructorId} for editing", instructorId);
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int instructorId, [Bind("InstructorID,Location,Timestamp")] OfficeAssignment officeAssignment)
        {
            if (instructorId != officeAssignment.InstructorID)
                return BadRequest();

            try
            {
                if (ModelState.IsValid)
                {
                    await _officeAssignmentService.RelocateAsync(officeAssignment);
                    return RedirectToAction(nameof(Details), new { instructorId = officeAssignment.InstructorID });
                }

                return View(officeAssignment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating office assignment {InstructorId}", instructorId);
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int instructorId)
        {
            try
            {
                await _officeAssignmentService.RemoveAssignmentAsync(instructorId);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error removing office assignment for instructor {InstructorId}", instructorId);
                return RedirectToAction("Error", "Home");
            }
        }
    }
}
