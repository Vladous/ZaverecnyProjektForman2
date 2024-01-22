namespace ZaverecnyProjektForman2.Models
{
    public class Insurance
    {
        public int Id { get; set; }
        public string Type { get; set; } = "";
        public string NameSubject { get; set; } = "";
        public decimal? Amount { get; set; } = null;
        public DateTime? InsuredFrom { get; set; } = null;
        public DateTime? InsuredUntil { get; set; } = null;
        public DateTime? CreationDate { get; set; } = null;
        public DateTime? LastChange { get; set; } = null;
        public ApplicationUser? UserCreated { get; set; } = null;
        public ApplicationUser? UserLastChanged { get; set; } = null;
    }
}
