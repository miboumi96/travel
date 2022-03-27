using System.Collections.Generic;
using TravelAgency.Models;

namespace TravelAgency.Data.ViewModels
{
    public class NewTravelDropdownsVM
    {
        public NewTravelDropdownsVM()
        {
                  Cities = new List<City>();
        }

             public List<City> Cities { get; set; }

    }
}
