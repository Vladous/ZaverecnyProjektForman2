using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ZaverecnyProjektForman2.Models
{
    //Pojištěnci
    public class Insured
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public DateTime? Birth { get; set; }
        public string? Email { get; set; }
        public string? Street { get; set; }
        public int? HouseNumber { get; set; }
        public string? City { get; set; }
        public int? PostNumber { get; set; }
        public int? Phone { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? LastChange { get; set; }
        public ApplicationUser? UserCreated { get; set; }
        public ApplicationUser? UserLastChanged { get; set; }

        // Navigační vlastnosti
        public List<InsuranceContracts>? InsuranceContracts { get; set; }
        public List<InsuranceEvents>? InsuranceEvents { get; set; }
    }
}
