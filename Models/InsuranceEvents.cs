using System.ComponentModel.DataAnnotations;

namespace ZaverecnyProjektForman2.Models
{
    /// <summary>
    /// Reprezentuje pojistnou událost, včetně vazeb na pojistníka a pojištění.
    /// </summary>
    public class InsuranceEvents
    {
        public int Id { get; set; }
        /// <summary>
        /// Reference na pojistnou smlouvu spojenou s tímto pojistným případem.
        /// </summary>
        public int InsuredId { get; set; }
        /// <summary>
        /// Reference na pojistníka spojeného s tímto pojistným případem.
        /// </summary>
        [Display(Name = "Pojistník")]
        public Insured? Insured { get; set; }
        public int InsuranceId { get; set; }
        /// <summary>
        /// Reference na typ pojištění spojeného s tímto pojistným případem.
        /// </summary>
        [Display(Name = "Typ pojištění")]
        public Insurance? Insurance { get; set; }
        /// <summary>
        /// Reference na pojistnou smlouvu spojennou s tímto pojistným případem.
        /// </summary>
        [Display(Name = "Pojistná smlouva")]
        public InsuranceEvents? InsuranceContracts { get; set; }
        /// <summary>
        /// Detaily pojistné události.
        /// </summary>
        [Display(Name = "Detail události")]
        public string? EventDetail { get; set; }
        /// <summary>
        /// Částka, která byla vyplacena v rámci této pojistné události.
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "Částka plnění musí být kladné číslo.")]
        [Display(Name = "Částka plnění")]
        public decimal FulfillmentAmount { get; set; }
        /// <summary>
        /// Datum, kdy k události došlo.
        /// </summary>
        [Display(Name = "Datum události")]
        public DateTime? FulfillmentDate { get; set; }
        /// <summary>
        /// Datum vytvoření záznamu o pojistné události.
        /// </summary>
        [Display(Name = "Datum vytvoření")]
        public DateTime? CreationDate { get; set; }
        /// <summary>
        /// Datum poslední změny záznamu.
        /// </summary>
        [Display(Name = "Datum poslední změny")]
        public DateTime? LastChange { get; set; }
        /// <summary>
        /// Informace o uživateli, který záznam vytvořil.
        /// </summary>
        [Display(Name = "Vytvořil uživatel")]
        public UserInfo? UserCreated { get; set; }
        /// <summary>
        /// Informace o uživateli, který záznam naposledy změnil.
        /// </summary>
        [Display(Name = "Poslední změny vytvořil")]
        public UserInfo? UserLastChanged { get; set; }
        /// <summary>
        /// Počet smluv spojených s tímto pojistným případem.
        /// </summary>
        public int EventsCount { get; set; }
    }
}
