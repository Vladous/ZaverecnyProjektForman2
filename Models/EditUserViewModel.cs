using System.ComponentModel.DataAnnotations;

namespace ZaverecnyProjektForman2.Models
{
    /// <summary>
    /// Model pro úpravu informací o uživateli.
    /// </summary>
    public class EditUserViewModel
    {
        public string UserId { get; set; } = "";
        /// <summary>
        /// Uživatelské jméno.
        /// </summary>
        [Required(ErrorMessage = "Uživatelské jméno je povinné.")]
        [StringLength(100, ErrorMessage = "Maximální délka uživatelského jména je 100 znaků.")]
        [Display(Name = "Uživatelské jméno")]
        public string UserName { get; set; } = "";
        /// <summary>
        /// Emailová adresa uživatele.
        /// </summary>
        [Required(ErrorMessage = "Email je povinný.")]
        [EmailAddress(ErrorMessage = "Neplatná emailová adresa.")]
        public string Email { get; set; } = "";
        /// <summary>
        /// Aktuální heslo uživatele, používá se pro potvrzení změn.
        /// </summary>
        [Required(ErrorMessage = "Heslo je povinné.")]
        [Display(Name = "Heslo")]
        public string Password { get; set; } = "";
        /// <summary>
        /// Nové heslo uživatele, pokud si přeje heslo změnit.
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "Nové heslo")]
        public string? NewPassword { get; set; } = "";
        /// <summary>
        /// Potvrzení nového hesla, musí se shodovat s polem Nové heslo.
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "Potvrzení nového hesla")]
        [Compare("NewPassword", ErrorMessage = "Nové heslo a potvrzení hesla se neshodují.")]
        public string? ConfirmNewPassword { get; set; } = "";
        /// <summary>
        /// Datum registrace uživatele.
        /// </summary>
        [Display(Name = "Datum registrace")]
        public DateTime? RegistrationDate { get; set; } = null;
        /// <summary>
        /// Práva uživatele v systému.
        /// </summary>
        [Required(ErrorMessage = "Je nutné zadat práva uživatele.")]
        [Display(Name = "Práva")]
        public string Role { get; set; } = "";
    }
}
