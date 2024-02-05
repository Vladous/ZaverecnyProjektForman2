namespace ZaverecnyProjektForman2.Models
{
    //Pojistné smlouvy
    public class InsuranceContracts
    {
        public int Id { get; set; }
        public int InsuredId { get; set; }
        public Insured? Insured { get; set; }
        public int InsuranceId { get; set; }
        public Insurance? Insurance { get; set; }
        public string? NameSubject { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? InsuredFrom { get; set; }
        public DateTime? InsuredUntil { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? LastChange { get; set; }
        public ApplicationUser? UserCreated { get; set; }
        public ApplicationUser? UserLastChanged { get; set; }

        // Navigační vlastnosti
        //public List<Insurance>? Insurance { get; set; }
        public List<InsuranceEvents>? InsuranceEvents { get; set; }


    }
}
