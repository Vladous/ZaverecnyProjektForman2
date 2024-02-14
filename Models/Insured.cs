using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ZaverecnyProjektForman2.Models
{
    /// <summary>
    ///  Reprezentuje pojištěnce v systému. Obsahuje osobní údaje, kontaktní informace a vazby na pojistné smlouvy a události.
    /// </summary>
    public class Insured
    {
        public int Id { get; set; }
        /// <summary>
        /// Jméno pojištěnce. Pole je povinné a jeho maximální délka je 100 znaků.
        /// </summary>
        [Required(ErrorMessage = "Jméno je povinné.")]
        [StringLength(100, ErrorMessage = "Maximální délka jména je 100 znaků.")]
        [Display(Name = "Jméno")]
        public string? Name { get; set; }
        /// <summary>
        /// Příjmení pojištěnce. Pole je povinné a jeho maximální délka je 100 znaků.
        /// </summary>
        [Required(ErrorMessage = "Příjmení je povinné.")]
        [StringLength(100, ErrorMessage = "Maximální délka příjmení je 100 znaků.")]
        [Display(Name = "Příjmení")]
        public string? Surname { get; set; }
        /// <summary>
        /// Datum narození pojištěnce. Používá se formát data.
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "Datum narození")]
        public DateTime? Birth { get; set; }
        /// <summary>
        /// E-mailová adresa pojištěnce. Musí být platný e-mail.
        /// </summary>
        [EmailAddress(ErrorMessage = "Neplatná emailová adresa.")]
        [Display(Name = "E-mail")]
        public string? Email { get; set; }
        /// <summary>
        /// Ulice bydliště pojištěnce. Pole je povinné.
        /// </summary>
        [Required(ErrorMessage = "Ulice je povinná.")]
        [StringLength(200, ErrorMessage = "Maximální délka ulice je 200 znaků.")]
        [Display(Name = "Ulice")]
        public string? Street { get; set; }
        /// <summary>
        /// Číslo domu bydliště pojištěnce. Pole je povinné.
        /// </summary>
        [Required(ErrorMessage = "Číslo domu je povinné.")]
        [Display(Name = "Číslo domu")]
        public int? HouseNumber { get; set; }
        /// <summary>
        /// Město bydliště pojištěnce. Pole je povinné.
        /// </summary>
        [Required(ErrorMessage = "Město je povinné.")]
        [StringLength(100, ErrorMessage = "Maximální délka města je 100 znaků.")]
        [Display(Name = "Město")]
        public string? City { get; set; }
        /// <summary>
        /// Poštovní směrovací číslo bydliště pojištěnce. Pole je povinné.
        /// </summary>
        [Required(ErrorMessage = "PSČ je povinné.")]
        [Display(Name = "PSČ")]
        public int? PostNumber { get; set; }
        /// <summary>
        /// Telefonní číslo pojištěnce. Musí být platné telefonní číslo.
        /// </summary>
        [Phone(ErrorMessage = "Neplatné telefonní číslo.")]
        [Display(Name = "Telefon")]
        public string? Phone { get; set; }
        /// <summary>
        /// Datum a čas registrace pojištěnce v systému.
        /// </summary>
        [Display(Name = "Datum registrace")]
        public DateTime? CreationDate { get; set; }
        /// <summary>
        /// Datum a čas poslední úpravy záznamu pojištěnce.
        /// </summary>
        [Display(Name = "Datum poslední úpravy")]
        public DateTime? LastChange { get; set; }
        /// <summary>
        /// Informace o uživateli, který pojištěnce zaregistroval.
        /// </summary>
        [NotMapped]
        [Display(Name = "Registroval uživatel")]
        public UserInfo? UserCreated { get; set; }
        /// <summary>
        /// Informace o uživateli, který naposledy upravil záznam pojištěnce.
        /// </summary>
        [NotMapped]
        [Display(Name = "Poslední změny provedl")]
        public UserInfo? UserLastChanged { get; set; }
        /// <summary>
        /// Seznam pojistných smluv spojených s pojištěncem.
        /// </summary>
        public List<InsuranceContracts>? InsuranceContracts { get; set; }
        /// <summary>
        /// Seznam pojistných událostí spojených s pojištěncem.
        /// </summary>
        public List<InsuranceEvents>? InsuranceEvents { get; set; }
    }
}
