using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TravelAgency.Data.Enums;

namespace TravelAgency.Data.ViewModels
{
    public class NewTravelVM
    {
        public int Id { get; set; }

        [Display(Name = "Travel name")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Display(Name = "Travel description")]
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Display(Name = "Price in DH")]
        [Required(ErrorMessage = "Price is required")]
        public double Price { get; set; }

        [Display(Name = "Travel poster URL")]
        [Required(ErrorMessage = "Travel poster URL is required")]
        public string ImageURL { get; set; }

        [Display(Name = "Travel start date")]
        [Required(ErrorMessage = "Start date is required")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Travel end date")]
        [Required(ErrorMessage = "End date is required")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Select a category")]
        [Required(ErrorMessage = "Travel category is required")]
        public TravelCategory TravelCategory { get; set; }

        //Relationships
        [Display(Name = "Select City (s)")]
        [Required(ErrorMessage = "Travel city(s) is required")]
        public List<int> CityIds { get; set; }

    
    }
}
