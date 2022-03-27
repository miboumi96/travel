
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelAgency.Data.Services;
using TravelAgency.Data.Static;
using TravelAgency.Data.ViewModels;

namespace TravelAgency.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class TravelsController : Controller
    {
        private readonly ITravelsService _service;

        public TravelsController(ITravelsService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var allTravels = await _service.GetAllAsync(n => n.Cities);
            return View(allTravels);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Filter(string searchString)
        {
            var allTravels = await _service.GetAllAsync(n => n.Cities);

            if (!string.IsNullOrEmpty(searchString))
            {
                var filteredResultNew = allTravels.Where(n => string.Equals(n.Name, searchString, StringComparison.CurrentCultureIgnoreCase) || string.Equals(n.Description, searchString, StringComparison.CurrentCultureIgnoreCase)).ToList();

                return View("Index", filteredResultNew);
            }

            return View("Index", allTravels);
        }

        //GET: Travels/Details/1
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var TravelDetail = await _service.GetTravelByIdAsync(id);
            return View(TravelDetail);
        }

        //GET: Travels/Create
        public async Task<IActionResult> Create()
        {
            var TravelDropdownsData = await _service.GetNewTravelDropdownsValues();

 
            ViewBag.Actors = new SelectList(TravelDropdownsData.Cities, "Id", "FullName");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewTravelVM travel)
        {
            if (!ModelState.IsValid)
            {
                var travelDropdownsData = await _service.GetNewTravelDropdownsValues();

                ViewBag.Actors = new SelectList(travelDropdownsData.Cities, "Id", "FullName");

                return View(travel);
            }

            await _service.AddNewTravelAsync(travel);
            return RedirectToAction(nameof(Index));
        }


        //GET: Travels/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var travelDetails = await _service.GetTravelByIdAsync(id);
            if (travelDetails == null) return View("NotFound");

            var response = new NewTravelVM()
            {
                Id = travelDetails.Id,
                Name = travelDetails.Name,
                Description = travelDetails.Description,
                Price = travelDetails.Price,
                StartDate = travelDetails.StartDate,
                EndDate = travelDetails.EndDate,
                ImageURL = travelDetails.ImageURL,
               TravelCategory = travelDetails.TravelCategory,
                CityIds = travelDetails.Cities.Select(n => n.CityId).ToList(),
            };

            var travelDropdownsData = await _service.GetNewTravelDropdownsValues();

            ViewBag.Actors = new SelectList(travelDropdownsData.Cities, "Id", "FullName");

            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewTravelVM travel)
        {
            if (id != travel.Id) return View("NotFound");

            if (!ModelState.IsValid)
            {
                var travelDropdownsData = await _service.GetNewTravelDropdownsValues();

                ViewBag.Actors = new SelectList(travelDropdownsData.Cities, "Id", "FullName");

                return View(travel);
            }

            await _service.UpdateTravelAsync(travel);
            return RedirectToAction(nameof(Index));
        }
    }
}