using Backend.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Backend.Infrastructure.Configuration
{
    public sealed class ConfigurationContext : DbContext
    {

        public ConfigurationContext(DbContextOptions<ConfigurationContext> options) : base(options)
        {
            //Database.Migrate();
        }

        public DbSet<Individual> Individuals { get; set; }
        public DbSet<Realty> Realties { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<MobileDevice> MobileDevices { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Telephone> Telephones { get; set; }
        public DbSet<SignedContract> SignedContracts { get; set; }
        //public DbSet<Beneficiary> Beneficiaries { get; set; }
        public DbSet<BeneficiaryAddress> Beneficiary_Address { get; set; }
        public DbSet<BeneficiaryTelephone> Individual_Telephone { get; set; }
        public DbSet<ContractBeneficiary> Contract_Beneficiary { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            builder.Entity<Individual>().HasBaseType<Beneficiary>();
            builder.Entity<Realty>().HasBaseType<Beneficiary>();
            builder.Entity<Pet>().HasBaseType<Beneficiary>();
            builder.Entity<Vehicle>().HasBaseType<Beneficiary>();
            builder.Entity<MobileDevice>().HasBaseType<Beneficiary>();
            builder.Entity<Contract>();
            builder.Entity<Address>();
            builder.Entity<Telephone>();
            builder.Entity<SignedContract>();
            //builder.Entity<Beneficiary>();
            builder.Entity<BeneficiaryAddress>();
            builder.Entity<BeneficiaryTelephone>();
            builder.Entity<ContractBeneficiary>();

        }
    }
}
