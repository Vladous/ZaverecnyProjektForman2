using System.ComponentModel.DataAnnotations;

namespace ZaverecnyProjektForman2.Models
{
    public class EditUserViewModel
    {
        public string UserId { get; set; } = "";
        [Display(Name = "Uživatelské jméno")]
        public string UserName { get; set; } = "";
        public string Email { get; set; } = "";
        [Display(Name = "Heslo")]
        public string Password { get; set; } = "";
        [Display(Name = "Datum registrace")]
        public DateTime? RegistrationDate { get; set; } = null;
        [Display(Name = "Práva")]
        public string Role { get; set; } = "";

    }
}
