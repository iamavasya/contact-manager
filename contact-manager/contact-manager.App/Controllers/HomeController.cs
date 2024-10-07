using contact_manager.BusinessLogic.Interfaces;
using contact_manager.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Person updatedPerson)
        {
            if (ModelState.IsValid)
            {
                var response = await _personService.UpdatePersonAsync(updatedPerson);
                if (response) return Ok();
                else return NotFound();
            }
            return BadRequest(ModelState);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _personService.DeletePersonAsync(id);
            if (response) return Ok();
            else return NotFound();
        }


        [HttpGet]
        public async Task<IActionResult> ViewDB()
        {
            var people = await _personService.GetPeopleAsync();
            return View(people);
        }
    }
}
