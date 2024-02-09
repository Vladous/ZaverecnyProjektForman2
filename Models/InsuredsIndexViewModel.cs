namespace ZaverecnyProjektForman2.Models
{
    public class InsuredsIndexViewModel
    {
        public IEnumerable<Insured> Items { get; set; }
        public string SortOrder { get; set; }
        public string NameFilter { get; set; }
        public string SurnameFilter { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
    }
}


