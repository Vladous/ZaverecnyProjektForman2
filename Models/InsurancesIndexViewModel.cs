namespace ZaverecnyProjektForman2.Models
{
    /// <summary>
    /// ViewModel pro indexovou stránku pojistných smluv. Obsahuje informace pro řazení, filtrování a stránkování seznamu pojistných smluv.
    /// </summary>
    public class InsurancesIndexViewModel
    {
        /// <summary>
        /// Seznam pojistných smluv k zobrazení.
        /// </summary>
        public IEnumerable<Insurance> Insurances { get; set; }
        /// <summary>
        /// Aktuální pořadí řazení (např. "name_desc" pro řazení podle jména sestupně).
        /// </summary>
        public string SortOrder { get; set; }
        /// <summary>
        /// Aktuální filtr pro typ pojistné smlouvy.
        /// </summary>
        public string TypFilter { get; set; }
        /// <summary>
        /// Číslo aktuální stránky pro stránkování.
        /// </summary>
        public int CurrentPage { get; set; }
        /// <summary>
        /// Celkový počet stránek dostupných pro stránkování.
        /// </summary>
        public int TotalPages { get; set; }
        /// <summary>
        /// Počet položek zobrazených na jednu stránku.
        /// </summary>
        public int PageSize { get; set; }
    }
}
