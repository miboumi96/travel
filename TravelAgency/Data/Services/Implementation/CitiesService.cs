using TravelAgency.Data.Base;
using TravelAgency.Models;

namespace TravelAgency.Data.Services
{
    public class CitiesService : EntityBaseRepository<City>, ICitiesService
    {
        public CitiesService(ApplicationDbContext context) : base(context) { }
    }
}
