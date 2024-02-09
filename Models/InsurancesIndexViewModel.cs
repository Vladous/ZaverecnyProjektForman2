namespace ZaverecnyProjektForman2.Models
{
    public class InsurancesIndexViewModel
    {
        public IEnumerable<Insurance> Insurances { get; set; }
        public string SortOrder { get; set; }
        public string TypFilter { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
    }
}
