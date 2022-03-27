
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelAgency.Data.Base;
using TravelAgency.Data.ViewModels;
using TravelAgency.Models;

namespace TravelAgency.Data.Services
{
    public class TravelsService : EntityBaseRepository<Travel>, ITravelsService
    {
        private readonly ApplicationDbContext _context;
        public TravelsService(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddNewTravelAsync(NewTravelVM data)
        {
            var newTravel = new Travel()
            {
                Name = data.Name,
                Description = data.Description,
                Price = data.Price,
                ImageURL = data.ImageURL,
                StartDate = data.StartDate,
                EndDate = data.EndDate,
                TravelCategory = data.TravelCategory,
            };
            await _context.Travels.AddAsync(newTravel);
            await _context.SaveChangesAsync();

            //Add Movie Actors
            foreach (var cityId in data.CityIds)
            {
                var newCityTravel = new City_Travel()
                {
                    TravelId = newTravel.Id,
                    CityId = cityId
                };
                await _context.Cities_Travel.AddAsync(newCityTravel);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<Travel> GetTravelByIdAsync(int id)
        {
            var travelDetails = await _context.Travels
                .Include(am => am.Cities).ThenInclude(a => a.City)
                .FirstOrDefaultAsync(n => n.Id == id);

            return travelDetails;
        }

        public async Task<NewTravelDropdownsVM> GetNewTravelDropdownsValues()
        {
            var response = new NewTravelDropdownsVM()
            {
                Cities = await _context.Cities.OrderBy(n => n.FullName).ToListAsync(),
            };

            return response;
        }

        public async Task UpdateTravelAsync(NewTravelVM data)
        {
            var dbTravel = await _context.Travels.FirstOrDefaultAsync(n => n.Id == data.Id);

            if(dbTravel != null)
            {
                dbTravel.Name = data.Name;
                dbTravel.Description = data.Description;
                dbTravel.Price = data.Price;
                dbTravel.ImageURL = data.ImageURL;
                dbTravel.StartDate = data.StartDate;
                dbTravel.EndDate = data.EndDate;
                dbTravel.TravelCategory = data.TravelCategory;
                await _context.SaveChangesAsync();
            }

            //Remove existing cities
            var existingCitiesDb = _context.Cities_Travel.Where(n => n.TravelId == data.Id).ToList();
            _context.Cities_Travel.RemoveRange(existingCitiesDb);
            await _context.SaveChangesAsync();

            //Add Travel Cities
            foreach (var cityId in data.CityIds)
            {
                var newCityTravel = new City_Travel()
                {
                    TravelId = data.Id,
                    CityId = cityId
                };
                await _context.Cities_Travel.AddAsync(newCityTravel);
            }
            await _context.SaveChangesAsync();
        }
    }
}
