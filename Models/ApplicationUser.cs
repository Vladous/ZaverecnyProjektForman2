using Microsoft.AspNetCore.Identity;

namespace ZaverecnyProjektForman2.Models
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime RegistrationDate { get; set; }
    }
}
