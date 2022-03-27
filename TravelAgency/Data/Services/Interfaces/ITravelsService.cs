using System.Threading.Tasks;
using TravelAgency.Data.Base;
using TravelAgency.Data.ViewModels;
using TravelAgency.Models;

namespace TravelAgency.Data.Services
{
    public interface ITravelsService:IEntityBaseRepository<Travel>
    {
        Task<Travel> GetTravelByIdAsync(int id);
        Task<NewTravelDropdownsVM> GetNewTravelDropdownsValues();
        Task AddNewTravelAsync(NewTravelVM data);
        Task UpdateTravelAsync(NewTravelVM data);
    }
}
