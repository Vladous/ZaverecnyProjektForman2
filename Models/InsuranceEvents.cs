﻿namespace ZaverecnyProjektForman2.Models
{
    // Pojistné události
    public class InsuranceEvents
    {
        public int Id { get; set; }
        public int? InsuredId { get; set; }
        public Insured? Insured { get; set; }
        public int? InsuranceId { get; set; }
        public Insurance? Insurance { get; set; }
        public decimal? FulfillmentAmount { get; set; }
        public DateTime? FulfillmentDate { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? LastChange { get; set; }
        public ApplicationUser? UserCreated { get; set; }
        public ApplicationUser? UserLastChanged { get; set; }
    }
}
