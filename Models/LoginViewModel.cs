using System.ComponentModel.DataAnnotations;

namespace ZaverecnyProjektForman2.Models
{
    /// <summary>
    /// ViewModel pro přihlášení uživatele.
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// Uživatelské jméno pro přihlášení. Vyžadováno.
        /// </summary>
        [Required(ErrorMessage = "Vyplňte uživatelské jméno")]
        [DataType(DataType.Text)]
        [Display(Name = "Uživatelské jméno")]
        public string UserName { get; set; } = "";
        /// <summary>
        /// Heslo pro přihlášení. Vyžadováno.
        /// </summary>
        [Required(ErrorMessage = "Vyplňte heslo")]
        [DataType(DataType.Password)]
        [Display(Name = "Heslo")]
        public string Password { get; set; } = "";
        /// <summary>
        /// Příznak určující, zda si systém má pamatovat uživatele po zavření prohlížeče.
        /// </summary>
        [Display(Name = "Pamatuj si mě")]
        public bool RememberMe { get; set; }
    }
}
