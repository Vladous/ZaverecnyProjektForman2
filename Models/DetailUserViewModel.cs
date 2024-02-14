using System.ComponentModel.DataAnnotations;

namespace ZaverecnyProjektForman2.Models
{
    /// <summary>
    /// Model pro zobrazení detailů uživatele.
    /// </summary>
    public class DetailUserViewModel
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
        [Display(Name = "Email")]
        public string Email { get; set; } = "";
        /// <summary>
        /// Datum registrace uživatele v systému.
        /// </summary>
        [Display(Name = "Datum registrace")]
        public DateTime? RegistrationDate { get; set; } = null;
        /// <summary>
        /// Přístupová práva uživatele v systému.
        /// </summary>
        [Required(ErrorMessage = "Je nutné zadat práva uživatele.")]
        [Display(Name = "Práva")]
        public string Role { get; set; } = "";
    }
}
