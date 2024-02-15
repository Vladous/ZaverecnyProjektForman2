using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ZaverecnyProjektForman2.Models;

namespace ZaverecnyProjektForman2.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Insured> Insureds { get; set; }
        public DbSet<Insurance> Insurances { get; set; }
        public DbSet<InsuranceContracts> InsuranceContracts { get; set; }
        public DbSet<InsuranceEvents> InsuranceEvents { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Zde vložíme Fluent API konfigurace
            modelBuilder.Entity<Insured>()
                .HasMany(i => i.InsuranceContracts)
                .WithOne(ic => ic.Insured)
                .HasForeignKey(ic => ic.InsuredId)
                .OnDelete(DeleteBehavior.Cascade); // Nastaví, co se stane při smazání pojištěnce

            modelBuilder.Entity<Insured>()
                .HasMany(i => i.InsuranceEvents)
                .WithOne(ie => ie.Insured)
                .HasForeignKey(ie => ie.InsuredId)
                .OnDelete(DeleteBehavior.Restrict); // Nastaví, co se stane při smazání pojištěnce

            modelBuilder.Entity<Insurance>()
                .HasMany(i => i.InsuranceContracts)
                .WithOne(ic => ic.Insurance)
                .HasForeignKey(ic => ic.InsuranceId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Insurance>()
                .HasMany(i => i.InsuranceEvents)
                .WithOne(ie => ie.Insurance)
                .HasForeignKey(ie => ie.InsuranceId)
                .OnDelete(DeleteBehavior.Restrict);

            // Nastavte přesnost a měřítko pro decimal vlastnosti
            modelBuilder.Entity<InsuranceContracts>()
                .Property(i => i.Amount)
                .HasPrecision(18, 2); // Příklad: 18 celkových číslic, 2 desetinná místa

            modelBuilder.Entity<InsuranceEvents>()
                .HasOne(ie => ie.InsuranceContracts) // Ujistěte se, že InsuranceContract je správně definován ve vaší třídě InsuranceEvents
                .WithMany(ic => ic.InsuranceEvents) // Předpokládá, že v InsuranceContracts existuje kolekce InsuranceEvents
                .HasForeignKey(ie => ie.InsuranceContractId) // Název vašeho FK pole v InsuranceEvents
                .OnDelete(DeleteBehavior.Restrict); // Zabrání kaskádovému mazání

            modelBuilder.Entity<InsuranceEvents>()
                .Property(ie => ie.FulfillmentAmount)
                .HasPrecision(18, 2); // Nastaví přesnost a měřítko pro FulfillmentAmount

            // Další konfigurace ...

        }
    }
}