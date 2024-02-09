﻿namespace ZaverecnyProjektForman2.Models
{
    public class InsuranceContractsIndexViewModel
    {
        public IEnumerable<InsuranceContracts> Contracts { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public string SortOrder { get; set; }
        public string SurnameFilter { get; set; }
        public string TypFilter { get; set; }
        public string SubjectFilter { get; set; }
    }
}
