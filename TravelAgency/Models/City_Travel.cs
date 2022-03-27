
namespace TravelAgency.Models
{
    public class City_Travel
    {
        public int TravelId { get; set; }
        public Travel Travel { get; set; }

        public int CityId { get; set; }
        public City City { get; set; }

    }
}
