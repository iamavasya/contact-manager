using contact_manager.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace contact_manager.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPersonService _personService;
        public HomeController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadCsv(IFormFile file)
        {
            try
            {
                await _personService.UploadCsvAsync(file);
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }

            return RedirectToAction("ViewDB");
        }

        public async Task<IActionResult> ViewDB()
        {
            var people = await _personService.GetPeopleAsync();
            return View(people);
        }
    }
}
