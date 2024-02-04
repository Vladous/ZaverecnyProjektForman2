namespace ZaverecnyProjektForman2.Models
{
    //Typy pojištění
    public class Insurance
    {
        public int Id { get; set; }
        public string? Type { get; set; }
        public string? NameSubject { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? InsuredFrom { get; set; }
        public DateTime? InsuredUntil { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? LastChange { get; set; }
        public ApplicationUser? UserCreated { get; set; }
        public ApplicationUser? UserLastChanged { get; set; }

        // Navigační vlastnosti
        public List<InsuranceContracts>? InsuranceContracts { get; set; }
        public List<InsuranceEvents>? InsuranceEvents { get; set; }
    }
}
