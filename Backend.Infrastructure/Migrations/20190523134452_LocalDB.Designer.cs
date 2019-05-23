﻿// <auto-generated />
using System;
using Backend.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Backend.Infrastructure.Migrations
{
    [DbContext(typeof(ConfigurationContext))]
    [Migration("20190523134452_LocalDB")]
    partial class LocalDB
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Backend.Core.Models.Address", b =>
                {
                    b.Property<Guid>("AddressId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AddressCity");

                    b.Property<string>("AddressComplement");

                    b.Property<string>("AddressCountry");

                    b.Property<string>("AddressNeighborhood");

                    b.Property<string>("AddressNumber");

                    b.Property<string>("AddressState");

                    b.Property<string>("AddressStreet");

                    b.Property<int>("AddressType");

                    b.Property<string>("AddressZipCode");

                    b.HasKey("AddressId");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("Backend.Core.Models.Beneficiary", b =>
                {
                    b.Property<Guid>("BeneficiaryId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.HasKey("BeneficiaryId");

                    b.ToTable("Beneficiaries");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Beneficiary");
                });

            modelBuilder.Entity("Backend.Core.Models.Contract", b =>
                {
                    b.Property<Guid>("ContractId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ContractCategory");

                    b.Property<bool>("ContractDeleted");

                    b.Property<DateTime>("ContractExpiryDate");

                    b.Property<DateTime>("ContractInitialDate");

                    b.Property<int>("ContractType");

                    b.HasKey("ContractId");

                    b.ToTable("Contracts");
                });

            modelBuilder.Entity("Backend.Core.Models.ContractBeneficiary", b =>
                {
                    b.Property<Guid>("ContractBeneficiaryId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("BeneficiaryId");

                    b.Property<Guid?>("SignedContractId");

                    b.HasKey("ContractBeneficiaryId");

                    b.HasIndex("BeneficiaryId");

                    b.HasIndex("SignedContractId");

                    b.ToTable("Contract_Beneficiary");
                });

            modelBuilder.Entity("Backend.Core.Models.IndividualAddress", b =>
                {
                    b.Property<Guid>("IndividualAddressId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("AddressId");

                    b.Property<Guid>("IndividualId");

                    b.HasKey("IndividualAddressId");

                    b.HasIndex("AddressId");

                    b.HasIndex("IndividualId");

                    b.ToTable("Individual_Address");
                });

            modelBuilder.Entity("Backend.Core.Models.IndividualTelephone", b =>
                {
                    b.Property<Guid>("IndividualTelephoneId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("IndividualId");

                    b.Property<Guid>("TelephoneId");

                    b.HasKey("IndividualTelephoneId");

                    b.HasIndex("IndividualId");

                    b.HasIndex("TelephoneId");

                    b.ToTable("Individual_Telephone");
                });

            modelBuilder.Entity("Backend.Core.Models.SignedContract", b =>
                {
                    b.Property<Guid>("ContractSignedId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ContractId");

                    b.Property<bool>("ContractIndividualIsActive");

                    b.Property<Guid>("IndividualId");

                    b.HasKey("ContractSignedId");

                    b.HasIndex("ContractId");

                    b.HasIndex("IndividualId");

                    b.ToTable("SignedContracts");
                });

            modelBuilder.Entity("Backend.Core.Models.Telephone", b =>
                {
                    b.Property<Guid>("TelephoneId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("TelephoneNumber");

                    b.Property<int>("TelephoneType");

                    b.HasKey("TelephoneId");

                    b.ToTable("Telephones");
                });

            modelBuilder.Entity("Backend.Core.Models.Individual", b =>
                {
                    b.HasBaseType("Backend.Core.Models.Beneficiary");

                    b.Property<Guid?>("AddressId");

                    b.Property<DateTime>("IndividualBirthdate");

                    b.Property<string>("IndividualCPF")
                        .HasMaxLength(11);

                    b.Property<bool>("IndividualDeleted");

                    b.Property<string>("IndividualEmail")
                        .HasMaxLength(30);

                    b.Property<Guid>("IndividualId");

                    b.Property<string>("IndividualName")
                        .HasMaxLength(50);

                    b.Property<string>("IndividualRG")
                        .HasMaxLength(9);

                    b.HasIndex("AddressId");

                    b.HasDiscriminator().HasValue("Individual");
                });

            modelBuilder.Entity("Backend.Core.Models.MobileDevice", b =>
                {
                    b.HasBaseType("Backend.Core.Models.Beneficiary");

                    b.Property<string>("MobileDeviceBrand")
                        .HasMaxLength(15);

                    b.Property<bool>("MobileDeviceDeleted");

                    b.Property<Guid>("MobileDeviceId");

                    b.Property<double>("MobileDeviceInvoiceValue");

                    b.Property<DateTime>("MobileDeviceManufactoringYear");

                    b.Property<string>("MobileDeviceModel")
                        .HasMaxLength(20);

                    b.Property<string>("MobileDeviceSerialNumber")
                        .HasMaxLength(40);

                    b.Property<int>("MobileDeviceType");

                    b.HasDiscriminator().HasValue("MobileDevice");
                });

            modelBuilder.Entity("Backend.Core.Models.Pet", b =>
                {
                    b.HasBaseType("Backend.Core.Models.Beneficiary");

                    b.Property<DateTime>("PetBirthdate");

                    b.Property<string>("PetBreed")
                        .HasMaxLength(30);

                    b.Property<bool>("PetDeleted");

                    b.Property<Guid>("PetId");

                    b.Property<string>("PetName")
                        .HasMaxLength(40);

                    b.Property<string>("PetSpecies")
                        .HasMaxLength(25);

                    b.HasDiscriminator().HasValue("Pet");
                });

            modelBuilder.Entity("Backend.Core.Models.Realty", b =>
                {
                    b.HasBaseType("Backend.Core.Models.Beneficiary");

                    b.Property<Guid?>("RealtyAddressAddressId");

                    b.Property<DateTime>("RealtyConstructionDate");

                    b.Property<bool>("RealtyDeleted");

                    b.Property<Guid>("RealtyId");

                    b.Property<double>("RealtyMarketValue");

                    b.Property<string>("RealtyMunicipalRegistration")
                        .HasMaxLength(50);

                    b.Property<double>("RealtySaleValue");

                    b.HasIndex("RealtyAddressAddressId");

                    b.HasDiscriminator().HasValue("Realty");
                });

            modelBuilder.Entity("Backend.Core.Models.Vehicle", b =>
                {
                    b.HasBaseType("Backend.Core.Models.Beneficiary");

                    b.Property<string>("VehicleBrand");

                    b.Property<string>("VehicleChassisNumber");

                    b.Property<int>("VehicleColor");

                    b.Property<double>("VehicleCurrentFipeValue");

                    b.Property<short>("VehicleCurrentMileage");

                    b.Property<bool>("VehicleDoneInspection");

                    b.Property<Guid>("VehicleId");

                    b.Property<DateTime>("VehicleManufactoringYear");

                    b.Property<string>("VehicleModel");

                    b.Property<DateTime>("VehicleModelYear");

                    b.HasDiscriminator().HasValue("Vehicle");
                });

            modelBuilder.Entity("Backend.Core.Models.ContractBeneficiary", b =>
                {
                    b.HasOne("Backend.Core.Models.Beneficiary", "Beneficiary")
                        .WithMany()
                        .HasForeignKey("BeneficiaryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Backend.Core.Models.SignedContract", "SignedContract")
                        .WithMany()
                        .HasForeignKey("SignedContractId");
                });

            modelBuilder.Entity("Backend.Core.Models.IndividualAddress", b =>
                {
                    b.HasOne("Backend.Core.Models.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Backend.Core.Models.Individual", "Individual")
                        .WithMany()
                        .HasForeignKey("IndividualId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Backend.Core.Models.IndividualTelephone", b =>
                {
                    b.HasOne("Backend.Core.Models.Individual", "Individual")
                        .WithMany()
                        .HasForeignKey("IndividualId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Backend.Core.Models.Telephone", "Telephone")
                        .WithMany()
                        .HasForeignKey("TelephoneId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Backend.Core.Models.SignedContract", b =>
                {
                    b.HasOne("Backend.Core.Models.Contract", "ContractSignedContract")
                        .WithMany()
                        .HasForeignKey("ContractId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Backend.Core.Models.Individual", "ContractSignedIndividual")
                        .WithMany()
                        .HasForeignKey("IndividualId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Backend.Core.Models.Individual", b =>
                {
                    b.HasOne("Backend.Core.Models.Address")
                        .WithMany("AddressIndividuals")
                        .HasForeignKey("AddressId");
                });

            modelBuilder.Entity("Backend.Core.Models.Realty", b =>
                {
                    b.HasOne("Backend.Core.Models.Address", "RealtyAddress")
                        .WithMany()
                        .HasForeignKey("RealtyAddressAddressId");
                });
#pragma warning restore 612, 618
        }
    }
}
