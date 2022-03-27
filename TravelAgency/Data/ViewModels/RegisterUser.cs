using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TravelAgency.Data.ViewModels
{
    public class RegisterUser
    {
        [Display(Name = "First name")]
        [Required(ErrorMessage = "le prenom est obligatoire")]
        public string FName { get; set; }

        [Display(Name = "Last name")]
        [Required(ErrorMessage = "le nom est obligatoire")]
        public string LName { get; set; }


        [Display(Name = "Email address")]
        [Required(ErrorMessage = "l'email est obligatoire")]
        public string EmailAddress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirm password")]
        [Required(ErrorMessage = "Confirmer le mot de passe obligatoire")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

    }
}
