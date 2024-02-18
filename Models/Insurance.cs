using System;
using System.ComponentModel.DataAnnotations;

namespace ZaverecnyProjektForman2.Models
{
    /// <summary>
    /// Reprezentuje typ pojištění.
    /// </summary>
    public class Insurance
    {
        public int Id { get; set; }
        /// <summary>
        /// Typ pojištění je povinný. Maximální délka typu pojištění je 100 znaků.
        /// </summary>
        [Required(ErrorMessage = "Typ pojištění je povinný.")]
        [StringLength(100, ErrorMessage = "Maximální délka typu pojištění je 100 znaků.")]
        [Display(Name = "Typ pojištění")]
        public string Type { get; set; }
        /// <summary>
        /// Datum vytvoření typu pojištění.
        /// </summary>
        [Display(Name = "Datum vytvoření")]
        public DateTime? CreationDate { get; set; }
        /// <summary>
        /// Datum poslední změny typu pojištění.
        /// </summary>
        [Display(Name = "Poslední změna")]
        public DateTime? LastChange { get; set; }
        /// <summary>
        /// Uživatel, který vytvořil typ pojištění.
        /// </summary>
        [Display(Name = "Vytvořil uživatel")]
        public UserInfo? UserCreated { get; set; }
        /// <summary>
        /// Uživatel, který naposledy upravil typ pojištění.
        /// </summary>
        [Display(Name = "Naposled upravil")]
        public UserInfo? UserLastChanged { get; set; }
        /// <summary>
        /// Navigační vlastnost pro pojistné smlouvy asociované s tímto typem pojištění.
        /// </summary>
        public virtual ICollection<InsuranceContracts>? InsuranceContracts { get; set; }
    }
}
