using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ZaverecnyProjektForman2.Models
{
    //Pojištěnci
    public class Insured
    {
        public int Id { get; set; }
        [Display(Name = "Jméno")]
        public string? Name { get; set; }
        [Display(Name = "Příjmení")]
        public string? Surname { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Datum narození")]
        public DateTime? Birth { get; set; }
        [Display(Name = "E-mail")]
        public string? Email { get; set; }
        [Display(Name = "Ulice")]
        public string? Street { get; set; }
        [Display(Name = "číslo domu")]
        public int? HouseNumber { get; set; }
        [Display(Name = "Město")]
        public string? City { get; set; }
        [Display(Name = "PSČ")]
        public int? PostNumber { get; set; }
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
