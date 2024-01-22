using System.ComponentModel.DataAnnotations;

namespace ZaverecnyProjektForman2.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Vyplňte uživatelské jméno")]
        [DataType(DataType.Text)]
        [Display(Name = "Uživatelské jméno")]
        public string UserName { get; set; } = "";


        [Required(ErrorMessage = "Vyplňte heslo")]
        [DataType(DataType.Password)]
        [Display(Name = "Heslo")]
        public string Password { get; set; } = "";

        [Display(Name = "Pamatuj si mě")]
        public bool RememberMe { get; set; }
    }
}
