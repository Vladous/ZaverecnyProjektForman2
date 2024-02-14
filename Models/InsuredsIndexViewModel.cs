namespace ZaverecnyProjektForman2.Models
{
    /// <summary>
    /// ViewModel pro indexovou stránku pojištěnců s podporou stránkování a filtrování.
    /// </summary>
    public class InsuredsIndexViewModel
    {
        /// <summary>
        /// Seznam pojištěnců pro zobrazení.
        /// </summary>
        public IEnumerable<Insured> Items { get; set; }
        /// <summary>
        /// Aktuální řazení seznamu.
        /// </summary>
        public string SortOrder { get; set; }
        /// <summary>
        /// Filtr pro vyhledávání podle jména.
        /// </summary>
        public string NameFilter { get; set; }
        /// <summary>
        /// Filtr pro vyhledávání podle příjmení.
        /// </summary>
        public string SurnameFilter { get; set; }
        /// <summary>
        /// Číslo aktuální stránky pro stránkování.
        /// </summary>
        public int CurrentPage { get; set; }
        /// <summary>
        /// Celkový počet stránek dostupných pro stránkování.
        /// </summary>
        public int TotalPages { get; set; }
        /// <summary>
        /// Počet položek na stránku pro stránkování.
        /// </summary>
        public int PageSize { get; set; }
    }
}


