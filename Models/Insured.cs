using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ZaverecnyProjektForman2.Models
{
    public class Insured
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Surname { get; set; } = "";
        public InsurTypeEnum.InsurType Type { get; set; }
        public DateTime? Birth { get; set; } = null;
        public string Email { get; set; } = "";
        public string Street { get; set; } = "";
        public int? HouseNumber { get; set; } = null;
        public string City { get; set; } = "";
        public int? PostNumber { get; set; } = null;
        public int? Phone { get; set; } = null;
        public DateTime? CreationDate { get; set; } = null;
        public DateTime? LastChange { get; set; } = null;
        public ApplicationUser? UserCreated { get; set; } = null;
        public ApplicationUser? UserLastChanged { get; set; } = null;
    }
}
