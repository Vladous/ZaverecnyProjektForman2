using System.ComponentModel.DataAnnotations;

namespace ZaverecnyProjektForman2.Models
{
    // Pojistné události
    public class InsuranceEvents
    {
        public int Id { get; set; }
        public int? InsuredId { get; set; }
        [Display(Name = "Pojistník")]
        public Insured? Insured { get; set; }
        public int? InsuranceId { get; set; }
        [Display(Name = "Typ pojištění")]
        public Insurance? Insurance { get; set; }
        [Display(Name = "Pojistná smlouva")]
        public InsuranceEvents? InsuranceContracts { get; set; }
        [Display(Name = "Detail události")]
        public string? EventDetail { get; set; }
        [Display(Name = "Částka plnění")]
        public decimal? FulfillmentAmount { get; set; }
        [Display(Name = "Datum události")]
        public DateTime? FulfillmentDate { get; set; }
        [Display(Name = "Datum vytvoření")]
        public DateTime? CreationDate { get; set; }
        [Display(Name = "Datum poslední změny")]
        public DateTime? LastChange { get; set; }
        [Display(Name = "Vytvořil uživatel")]
        public ApplicationUser? UserCreated { get; set; }
        [Display(Name = "Poslední změny vytvořil")]
        public ApplicationUser? UserLastChanged { get; set; }

        // Nová vlastnost pro počet smluv
        public int EventsCount { get; set; }
    }
}
