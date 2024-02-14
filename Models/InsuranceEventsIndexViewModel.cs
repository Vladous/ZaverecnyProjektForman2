namespace ZaverecnyProjektForman2.Models
{
    /// <summary>
    /// ViewModel pro indexovou stránku pojistných událostí. Obsahuje informace pro řazení, filtrování a stránkování seznamu pojistných událostí.
    /// </summary>
    public class InsuranceEventsIndexViewModel
    {
        /// <summary>
        /// Seznam pojistných událostí k zobrazení.
        /// </summary>
        public IEnumerable<InsuranceEvents> Events { get; set; }
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
        /// <summary>
        /// Aktuální pořadí řazení (např. "date_desc" pro řazení podle data události sestupně).
        /// </summary>
        public string SortOrder { get; set; }
        /// <summary>
        /// Filtr pro vyhledávání událostí podle příjmení pojistníka.
        /// </summary>
        public string SurnameFilter { get; set; }
        /// <summary>
        /// Filtr pro vyhledávání událostí podle typu pojištění.
        /// </summary>
        public string TypFilter { get; set; }
        /// <summary>
        /// Filtr pro vyhledávání událostí podle detailu pojistné události.
        /// </summary>
        public string DetailFilter { get; set; }
    }
}
