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

            modelBuilder.Entity<InsuranceContracts>()
               .HasOne(ic => ic.Insurance)
               .WithMany(i => i.InsuranceContracts)
               .HasForeignKey(ic => ic.InsuranceId)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<InsuranceContracts>()
                .HasOne(ic => ic.Insured)
                .WithMany(ins => ins.InsuranceContracts)
                .HasForeignKey(ic => ic.InsuredId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<InsuranceEvents>()
                .HasOne(ie => ie.InsuranceContracts)
                .WithMany(ic => ic.InsuranceEvents)
                .HasForeignKey(ie => ie.InsuranceContractId)
                .OnDelete(DeleteBehavior.Cascade);


            // Nastavte přesnost a měřítko pro decimal vlastnosti
            modelBuilder.Entity<InsuranceContracts>()
                .Property(i => i.Amount)
                .HasPrecision(18, 2); // Příklad: 18 celkových číslic, 2 desetinná místa

            modelBuilder.Entity<InsuranceEvents>()
                .Property(ie => ie.FulfillmentAmount)
                .HasPrecision(18, 2); // Nastaví přesnost a měřítko pro FulfillmentAmount

            // Další konfigurace ...

        }
    }
}