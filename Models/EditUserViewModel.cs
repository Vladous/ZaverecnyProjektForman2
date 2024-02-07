using System.ComponentModel.DataAnnotations;

namespace ZaverecnyProjektForman2.Models
{
    public class EditUserViewModel
    {
        public string UserId { get; set; } = "";
        [Required(ErrorMessage = "Uživatelské jméno je povinné.")]
        [StringLength(100, ErrorMessage = "Maximální délka uživatelského jména je 100 znaků.")]
        [Display(Name = "Uživatelské jméno")]
        public string UserName { get; set; } = "";
        [Required(ErrorMessage = "Email je povinný.")]
        [EmailAddress(ErrorMessage = "Neplatná emailová adresa.")]
        public string Email { get; set; } = "";
        [Required(ErrorMessage = "Heslo je povinné.")]
        [Display(Name = "Heslo")]
        public string Password { get; set; } = "";
        [DataType(DataType.Password)]
        [Display(Name = "Nové heslo")]
        public string? NewPassword { get; set; } = "";

        [DataType(DataType.Password)]
        [Display(Name = "Potvrzení nového hesla")]
        [Compare("NewPassword", ErrorMessage = "Nové heslo a potvrzení hesla se neshodují.")]
        public string? ConfirmNewPassword { get; set; } = "";
        [Display(Name = "Datum registrace")]
        public DateTime? RegistrationDate { get; set; } = null;
        [Required(ErrorMessage = "Je nutné zadat práva uživatele.")]
        [Display(Name = "Práva")]
        public string Role { get; set; } = "";

    }
}
