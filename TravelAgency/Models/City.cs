
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TravelAgency.Data.Base;

namespace TravelAgency.Models
{
    public class City : IEntityBase
    {
        public Travel Travel { get; set; }

        [Key]
        public int Id { get; set; }
        [Display(Name = "City Picture")]
        [Required(ErrorMessage = "City Picture is required")]
        public string ProfilePictureURL { get; set; }

        [Display(Name = " Name")]
        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Full Name must be between 3 and 50 chars")]
        public string FullName { get; set; }
        //Relationships
        public List<City_Travel> Cities { get; set; }


    }
}
