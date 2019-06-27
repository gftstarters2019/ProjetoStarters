using Backend.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Backend.Infrastructure.Configuration
{
    public sealed class ConfigurationContext : DbContext
    {

        public ConfigurationContext(DbContextOptions<ConfigurationContext> options) : base(options)
        {
            Database.Migrate();
        }

        public DbSet<IndividualEntity> Individuals { get; set; }
        public DbSet<RealtyEntity> Realties { get; set; }
        public DbSet<PetEntity> Pets { get; set; }
        public DbSet<VehicleEntity> Vehicles { get; set; }
        public DbSet<MobileDeviceEntity> MobileDevices { get; set; }
        public DbSet<ContractEntity> Contracts { get; set; }
        public DbSet<AddressEntity> Addresses { get; set; }
        public DbSet<TelephoneEntity> Telephones { get; set; }
        public DbSet<SignedContractEntity> SignedContracts { get; set; }
        //public DbSet<Beneficiary> Beneficiaries { get; set; }
        public DbSet<BeneficiaryAddress> Beneficiary_Address { get; set; }
        public DbSet<IndividualTelephone> Individual_Telephone { get; set; }
        public DbSet<ContractBeneficiary> Contract_Beneficiary { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            builder.Entity<IndividualEntity>().HasBaseType<BeneficiaryEntity>().ToTable("Beneficiaries");
            builder.Entity<RealtyEntity>().HasBaseType<BeneficiaryEntity>().ToTable("Beneficiaries");
            builder.Entity<PetEntity>().HasBaseType<BeneficiaryEntity>().ToTable("Beneficiaries");
            builder.Entity<VehicleEntity>().HasBaseType<BeneficiaryEntity>().ToTable("Beneficiaries");
            builder.Entity<MobileDeviceEntity>().HasBaseType<BeneficiaryEntity>().ToTable("Beneficiaries");
            builder.Entity<ContractEntity>().ToTable("Contracts");
            builder.Entity<AddressEntity>().ToTable("Addresses");
            builder.Entity<TelephoneEntity>().ToTable("Telephones");
            builder.Entity<SignedContractEntity>().ToTable("SignedContracts");
            //builder.Entity<Beneficiary>();
            builder.Entity<BeneficiaryAddress>().ToTable("Beneficiary_Address");
            builder.Entity<IndividualTelephone>().ToTable("Individual_Telephone");
            builder.Entity<ContractBeneficiary>().ToTable("Contract_Beneficiary");

        }
    }
}
