using System.ComponentModel.DataAnnotations;

namespace ZaverecnyProjektForman2.Models
{
    public class DetailUserViewModel
    {
        public string UserId { get; set; } = "";
        [Required(ErrorMessage = "Uživatelské jméno je povinné.")]
        [StringLength(100, ErrorMessage = "Maximální délka uživatelského jména je 100 znaků.")]
        [Display(Name = "Uživatelské jméno")]
        public string UserName { get; set; } = "";
        [Required(ErrorMessage = "Email je povinný.")]
        [EmailAddress(ErrorMessage = "Neplatná emailová adresa.")]
        [Display(Name = "Email")]
        public string Email { get; set; } = "";
        [Display(Name = "Datum registrace")]
        public DateTime? RegistrationDate { get; set; } = null;
        [Required(ErrorMessage = "Je nutné zadat práva uživatele.")]
        [Display(Name = "Práva")]
        public string Role { get; set; } = "";
    }
}
