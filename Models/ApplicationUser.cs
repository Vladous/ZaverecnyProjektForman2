using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ZaverecnyProjektForman2.Models
{
    /// <summary>
    /// Rozšíření identity uživatele o dodatečné informace.
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Datum, kdy byl uživatel registrován v systému.
        /// </summary>
        [Display(Name = "Datum registrace")]
        public DateTime RegistrationDate { get; set; }
    }
}
