using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TravelAgency.Data.Base;
using TravelAgency.Data.Enums;

namespace TravelAgency.Models
{
    public class Travel : IEntityBase
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string ImageURL { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public TravelCategory TravelCategory { get; set; }
       
        //Relationships
        public List<City_Travel> Cities { get; set; }

    }
}
