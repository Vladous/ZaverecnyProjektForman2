using System.ComponentModel.DataAnnotations;

namespace ZaverecnyProjektForman2.Models
{
    /// <summary>
    /// ViewModel pro registraci nového uživatele.
    /// </summary>
    public class RegisterViewModel
    {
        /// <summary>
        /// Vyžaduje se k určení přístupových práv uživatele (např. admin, manager, viewer).
        /// </summary>
        [Required(ErrorMessage = "Vyplňte požadovaný přístup (admin,manager,Viewer")]
        [Display(Name = "Přístupová práva")]
        public string Role { get; set; } = "";
        /// <summary>
        /// Uživatelské jméno pro nový účet.
        /// </summary>
        [Required(ErrorMessage = "Vyplňte uživatelské jméno")]
        [DataType(DataType.Text)]
        [Display(Name = "Uživatelské jméno")]
        public string UserName { get; set; } = "";
        /// <summary>
        /// Emailová adresa pro nový účet.
        /// </summary>
        [Required(ErrorMessage = "Vyplňte emailovou adresu")]
        [EmailAddress(ErrorMessage = "Neplatná emailová adresa")]
        [Display(Name = "Email")]
        public string Email { get; set; } = "";
        /// <summary>
        /// Heslo pro nový účet. Musí splňovat minimální délku a bezpečnostní požadavky.
        /// </summary>
        [Required(ErrorMessage = "Vyplňte heslo")]
        [StringLength(100, ErrorMessage = "{0} musí mít délku alespoň {2} a nejvíc {1} znaků.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Heslo")]
        public string Password { get; set; } = "";
        /// <summary>
        /// Potvrzení hesla musí shodovat s heslem pro ověření, že uživatel správně zadává heslo, které si přeje použít.
        /// </summary>
        [Required(ErrorMessage = "Vyplňte heslo")]
        [DataType(DataType.Password)]
        [Display(Name = "Potvrzení hesla")]
        [Compare(nameof(Password), ErrorMessage = "Zadaná hesla se musí shodovat.")]
        public string ConfirmPassword { get; set; } = "";

    }
}
