using System.ComponentModel.DataAnnotations;

namespace ZaverecnyProjektForman2.Models
{
    //Pojistné smlouvy
    public class InsuranceContracts
    {
        public int Id { get; set; }
        public int InsuredId { get; set; }
        [Display(Name = "Pojištěnec")]
        public Insured? Insured { get; set; }
        public int InsuranceId { get; set; }
        [Display(Name = "Typ pojištění")]
        public Insurance? Insurance { get; set; }
        [Required(ErrorMessage = "Předmět pojištění je povinný.")]
        [StringLength(100, ErrorMessage = "Maximální délka předmětu pojištění je 100 znaků.")]
        [Display(Name = "Předmět pojištění")]
        public string? NameSubject { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Částka pojištění musí být kladné číslo.")]
        [Display(Name = "Částka pojištění")]
        public decimal? Amount { get; set; }
        [Display(Name = "Pojištěno od")]
        [DataType(DataType.Date)]
        public DateTime? InsuredFrom { get; set; }
        [Display(Name = "Pojištěno do")]
        [DataType(DataType.Date)]
        public DateTime? InsuredUntil { get; set; }
        [Display(Name = "Datum vytvoření")]
        public DateTime? CreationDate { get; set; }
        [Display(Name = "Datum poslední změny")]
        public DateTime? LastChange { get; set; }
        [Display(Name = "Vytvořil uživatel")]
        public ApplicationUser? UserCreated { get; set; }
        [Display(Name = "Naposledy změnil")]
        public ApplicationUser? UserLastChanged { get; set; }

        // Navigační vlastnosti
        //public List<Insurance>? Insurance { get; set; }
        public List<InsuranceEvents>? InsuranceEvents { get; set; }


    }
}
