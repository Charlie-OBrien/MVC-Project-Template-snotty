using Brawndo_Components.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Brawndo.Controllers
{
    public class PersonController : Controller
    {
        private readonly IPersonService _personService;
        private readonly ILogger<PersonController> _logger;

        public PersonController(IPersonService personService, ILogger<PersonController> logger)
        {
            _personService = personService;
            _logger = logger;
        }

        public async Task<IActionResult> Students()
        {
            try
            {
                var students = await _personService.GetPeopleByDiscriminatorAsync("Student");
                return View(students);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving students");
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<IActionResult> Instructors()
        {
            try
            {
                var instructors = await _personService.GetPeopleByDiscriminatorAsync("Instructor");
                return View(instructors);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving instructors");
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var person = await _personService.GetPersonAsync(id);
                if (person == null)
                    return NotFound();

                return View(person);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving person {PersonId}", id);
                return RedirectToAction("Error", "Home");
            }
        }
    }
}
