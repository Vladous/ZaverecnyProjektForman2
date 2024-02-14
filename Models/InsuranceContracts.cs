using System.ComponentModel.DataAnnotations;

namespace ZaverecnyProjektForman2.Models
{
    /// <summary>
    /// Reprezentuje pojistnou smlouvu v systému.
    /// </summary>
    public class InsuranceContracts
    {
        public int Id { get; set; }
        /// <summary>
        /// Identifikátor pojistníka, ke kterému smlouva patří.
        /// </summary>
        public int InsuredId { get; set; }
        /// <summary>
        /// Reference na pojistníka.
        /// </summary>
        [Display(Name = "Pojištěnec")]
        public Insured? Insured { get; set; }
        /// <summary>
        /// Identifikátor typu pojištění.
        /// </summary>
        public int InsuranceId { get; set; }
        /// <summary>
        /// Reference na typ pojištění.
        /// </summary>
        [Display(Name = "Typ pojištění")]
        public Insurance? Insurance { get; set; }
        /// <summary>
        /// Předmět pojistné smlouvy.
        /// </summary>
        [Required(ErrorMessage = "Předmět pojištění je povinný.")]
        [StringLength(100, ErrorMessage = "Maximální délka předmětu pojištění je 100 znaků.")]
        [Display(Name = "Předmět pojištění")]
        public string? NameSubject { get; set; }
        /// <summary>
        /// Částka pojištění.
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "Částka pojištění musí být kladné číslo.")]
        [Display(Name = "Částka pojištění")]
        public decimal? Amount { get; set; }
        /// <summary>
        /// Datum počátku platnosti pojistné smlouvy.
        /// </summary>
        [Display(Name = "Pojištěno od")]
        [DataType(DataType.Date)]
        public DateTime? InsuredFrom { get; set; }
        /// <summary>
        /// Datum konce platnosti pojistné smlouvy.
        /// </summary>
        [Display(Name = "Pojištěno do")]
        [DataType(DataType.Date)]
        public DateTime? InsuredUntil { get; set; }
        /// <summary>
        /// Datum vytvoření pojistné smlouvy.
        /// </summary>
        [Display(Name = "Datum vytvoření")]
        public DateTime? CreationDate { get; set; }
        /// <summary>
        /// Datum poslední změny pojistné smlouvy.
        /// </summary>
        [Display(Name = "Datum poslední změny")]
        public DateTime? LastChange { get; set; }
        /// <summary>
        /// Informace o uživateli, který smlouvu vytvořil.
        /// </summary>
        [Display(Name = "Vytvořil uživatel")]
        public UserInfo? UserCreated { get; set; }
        /// <summary>
        /// Informace o uživateli, který smlouvu naposledy upravil.
        /// </summary>
        [Display(Name = "Naposledy změnil")]
        public UserInfo? UserLastChanged { get; set; }
        // Navigační vlastnosti
        /// <summary>
        /// Seznam pojistných událostí, které se týkají této smlouvy.
        /// </summary>
        public List<InsuranceEvents>? InsuranceEvents { get; set; }
    }
}
