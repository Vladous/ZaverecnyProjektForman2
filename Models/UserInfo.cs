using Humanizer;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZaverecnyProjektForman2.Models
{
    /// <summary>
    /// Třída pro uchování základních informací o uživateli.
    /// </summary>
    [NotMapped]
    public class UserInfo
    {
        /// <summary>
        /// Uživatelské jméno.
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// E-mailová adresa uživatele. Může být null, pokud není nastavena.
        /// </summary>
        public string? Email { get; set; }
        /// <summary>
        /// Telefonní číslo uživatele. Může být null, pokud není nastaveno.
        /// </summary>
        public string? PhoneNumber { get; set; }
        /// <summary>
        /// Datum a čas registrace uživatele. Může být null, pokud není známé.
        /// </summary>
        public DateTime? RegistrationDate { get; set; }
    }
}
