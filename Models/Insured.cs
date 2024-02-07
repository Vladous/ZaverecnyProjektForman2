using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ZaverecnyProjektForman2.Models
{
    //Pojištěnci
    public class Insured
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Jméno je povinné.")]
        [StringLength(100, ErrorMessage = "Maximální délka jména je 100 znaků.")]
        [Display(Name = "Jméno")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Příjmení je povinné.")]
        [StringLength(100, ErrorMessage = "Maximální délka příjmení je 100 znaků.")]
        [Display(Name = "Příjmení")]
        public string? Surname { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Datum narození")]
        public DateTime? Birth { get; set; }
        [EmailAddress(ErrorMessage = "Neplatná emailová adresa.")]
        [Display(Name = "E-mail")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Ulice je povinná.")]
        [StringLength(200, ErrorMessage = "Maximální délka ulice je 200 znaků.")]
        [Display(Name = "Ulice")]
        public string? Street { get; set; }
        [Required(ErrorMessage = "Číslo domu je povinné.")]
        [Display(Name = "číslo domu")]
        public int? HouseNumber { get; set; }
        [Required(ErrorMessage = "Město je povinné.")]
        [StringLength(100, ErrorMessage = "Maximální délka města je 100 znaků.")]
        [Display(Name = "Město")]
        public string? City { get; set; }
        [Required(ErrorMessage = "PSČ je povinné.")]
        [Display(Name = "PSČ")]
        public int? PostNumber { get; set; }
        [Phone(ErrorMessage = "Neplatné telefonní číslo.")]
        [Display(Name = "Telefon")]
        public string? Phone { get; set; }
        [Display(Name = "Datum registrace")]
        public DateTime? CreationDate { get; set; }
        [Display(Name = "Datum poslední úpravy")]
        public DateTime? LastChange { get; set; }
        [Display(Name = "Registroval uživatel")]
        public ApplicationUser? UserCreated { get; set; }
        [Display(Name = "Poslední změny provedl")]
        public ApplicationUser? UserLastChanged { get; set; }

        // Navigační vlastnosti
        public List<InsuranceContracts>? InsuranceContracts { get; set; }
        public List<InsuranceEvents>? InsuranceEvents { get; set; }
    }
}
