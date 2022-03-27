using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TravelAgency.Data.Services;
using TravelAgency.Data.Static;
using TravelAgency.Models;

namespace TravelAgency.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class CitiesController : Controller
    {
        private readonly ICitiesService _service;

        public CitiesController(ICitiesService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync();
            return View(data);
        }

        //Get: Cities/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("FullName,ProfilePictureURL,Bio")]City city)
        {
            if (!ModelState.IsValid)
            {
                return View(city);
            }
            await _service.AddAsync(city);
            return RedirectToAction(nameof(Index));
        }

        //Get: Cities/Details/1
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var cityDetails = await _service.GetByIdAsync(id);

            if (cityDetails == null) return View("NotFound");
            return View(cityDetails);
        }

        //Get: Cities/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var cityDetails = await _service.GetByIdAsync(id);
            if (cityDetails == null) return View("NotFound");
            return View(cityDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,ProfilePictureURL,Bio")] City city)
        {
            if (!ModelState.IsValid)
            {
                return View(city);
            }
            await _service.UpdateAsync(id, city);
            return RedirectToAction(nameof(Index));
        }

        //Get: Cities/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var cityDetails = await _service.GetByIdAsync(id);
            if (cityDetails == null) return View("NotFound");
            return View(cityDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cityDetails = await _service.GetByIdAsync(id);
            if (cityDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
