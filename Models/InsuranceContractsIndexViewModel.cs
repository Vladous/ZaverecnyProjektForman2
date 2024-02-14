namespace ZaverecnyProjektForman2.Models
{
    /// <summary>
    /// ViewModel pro zobrazení seznamu pojistných smluv s podporou stránkování, řazení a filtrování.
    /// </summary>
    public class InsuranceContractsIndexViewModel
    {
        /// <summary>
        /// Kolekce pojistných smluv pro zobrazení na aktuální stránce.
        /// </summary>
        public IEnumerable<InsuranceContracts> Contracts { get; set; }
        /// <summary>
        /// Aktuální číslo stránky.
        /// </summary>
        public int CurrentPage { get; set; }
        /// <summary>
        /// Celkový počet stránek dostupných pro stránkování.
        /// </summary>
        public int TotalPages { get; set; }
        /// <summary>
        /// Počet položek zobrazených na jedné stránce.
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// Aktuální způsob řazení smluv.
        /// </summary>
        public string SortOrder { get; set; }
        /// <summary>
        /// Filtrovací kritérium pro příjmení pojistníka.
        /// </summary>
        public string SurnameFilter { get; set; }
        /// <summary>
        /// Filtrovací kritérium pro typ pojištění.
        /// </summary>
        public string TypFilter { get; set; }
        /// <summary>
        /// Filtrovací kritérium pro předmět pojištění.
        /// </summary>
        public string SubjectFilter { get; set; }
    }
}
