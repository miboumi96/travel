using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;


namespace TravelAgency.Models
{
    public class TravelUser : IdentityUser
    {
        [Required(ErrorMessage = "Entrer votre nom !!")]
        [Display(Name = "Nom")]
        public string FName { get; set; }

        [Required(ErrorMessage = "Entrer votre prénom !")]
        [Display(Name = "Prenom")]
        public string LName { get; set; }

    }
}
