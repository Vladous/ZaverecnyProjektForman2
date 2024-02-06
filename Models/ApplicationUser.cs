using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ZaverecnyProjektForman2.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "Datum registrace")]
        public DateTime RegistrationDate { get; set; }
    }
}
