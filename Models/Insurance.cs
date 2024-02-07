using System.ComponentModel.DataAnnotations;

namespace ZaverecnyProjektForman2.Models
{
    //Typy pojištění
    public class Insurance
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Typ pojištění je povinný.")]
        [StringLength(100, ErrorMessage = "Maximální délka typu pojištění je 100 znaků.")]
        [Display(Name = "Typ pojištění")]
        public string Type { get; set; }
        [Display(Name = "Datum vytvoření")]
        public DateTime CreationDate { get; set; }
        [Display(Name = "Poslední změna")]
        public DateTime LastChange { get; set; }
        [Display(Name = "Vytvořil uživatel")]
        public ApplicationUser UserCreated { get; set; }
        [Display(Name = "Naposled upravil")]
        public ApplicationUser UserLastChanged { get; set; }

        // Navigační vlastnosti
        public List<InsuranceContracts>? InsuranceContracts { get; set; }
        public List<InsuranceEvents>? InsuranceEvents { get; set; }
    }
}
