using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Infrastructure.Migrations
{
    public partial class LocalDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContractIndividuals",
                columns: table => new
                {
                    ContractIndividualId = table.Column<Guid>(nullable: false),
                    ContractIndividualIsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractIndividuals", x => x.ContractIndividualId);
                });

            migrationBuilder.CreateTable(
                name: "MobileDevices",
                columns: table => new
                {
                    MobileDeviceId = table.Column<Guid>(nullable: false),
                    BeneficiaryId = table.Column<Guid>(nullable: false),
                    MobileDeviceBrand = table.Column<string>(nullable: true),
                    MobileDeviceModel = table.Column<string>(nullable: true),
                    MobileDeviceSerialNumber = table.Column<string>(nullable: true),
                    MobileDeviceManufactoringYear = table.Column<DateTime>(nullable: false),
                    MobileDeviceType = table.Column<int>(nullable: false),
                    MobileDeviceInvoiceValue = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MobileDevices", x => x.MobileDeviceId);
                });

            migrationBuilder.CreateTable(
                name: "Pets",
                columns: table => new
                {
                    PetId = table.Column<Guid>(nullable: false),
                    BeneficiaryId = table.Column<Guid>(nullable: false),
                    PetName = table.Column<string>(nullable: true),
                    PetSpecies = table.Column<string>(nullable: true),
                    PetBreed = table.Column<string>(nullable: true),
                    PetBirthdate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pets", x => x.PetId);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    VehicleId = table.Column<Guid>(nullable: false),
                    BeneficiaryId = table.Column<Guid>(nullable: false),
                    VehicleBrand = table.Column<string>(nullable: true),
                    VehicleModel = table.Column<string>(nullable: true),
                    VehicleManufactoringYear = table.Column<DateTime>(nullable: false),
                    VehicleColor = table.Column<int>(nullable: false),
                    VehicleModelYear = table.Column<DateTime>(nullable: false),
                    VehicleChassisNumber = table.Column<string>(nullable: true),
                    VehicleCurrentMileage = table.Column<short>(nullable: false),
                    VehicleCurrentFipeValue = table.Column<double>(nullable: false),
                    VehicleDoneInspection = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.VehicleId);
                });

            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    ContractId = table.Column<Guid>(nullable: false),
                    ContractType = table.Column<int>(nullable: false),
                    ContractCategory = table.Column<int>(nullable: false),
                    ContractExpiryDate = table.Column<DateTime>(nullable: false),
                    ContractInitialDate = table.Column<DateTime>(nullable: false),
                    ContractIndividualId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.ContractId);
                    table.ForeignKey(
                        name: "FK_Contracts_ContractIndividuals_ContractIndividualId",
                        column: x => x.ContractIndividualId,
                        principalTable: "ContractIndividuals",
                        principalColumn: "ContractIndividualId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Individuals",
                columns: table => new
                {
                    IndividualId = table.Column<Guid>(nullable: false),
                    BeneficiaryId = table.Column<Guid>(nullable: false),
                    IndividualName = table.Column<string>(nullable: true),
                    IndividualCPF = table.Column<string>(maxLength: 30, nullable: true),
                    IndividualRG = table.Column<string>(nullable: true),
                    IndividualEmail = table.Column<string>(nullable: true),
                    IndividualBirthdate = table.Column<DateTime>(nullable: false),
                    ContractIndividualId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Individuals", x => x.IndividualId);
                    table.ForeignKey(
                        name: "FK_Individuals_ContractIndividuals_ContractIndividualId",
                        column: x => x.ContractIndividualId,
                        principalTable: "ContractIndividuals",
                        principalColumn: "ContractIndividualId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    AddressId = table.Column<Guid>(nullable: false),
                    AddressStreet = table.Column<string>(nullable: true),
                    AddressNumber = table.Column<string>(nullable: true),
                    AddressComplement = table.Column<string>(nullable: true),
                    AddressNeighborhood = table.Column<string>(nullable: true),
                    AddressCity = table.Column<string>(nullable: true),
                    AddressState = table.Column<string>(nullable: true),
                    AddressCountry = table.Column<string>(nullable: true),
                    AddressZipCode = table.Column<string>(nullable: true),
                    AddressType = table.Column<int>(nullable: false),
                    IndividualId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.AddressId);
                    table.ForeignKey(
                        name: "FK_Addresses_Individuals_IndividualId",
                        column: x => x.IndividualId,
                        principalTable: "Individuals",
                        principalColumn: "IndividualId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Telephones",
                columns: table => new
                {
                    TelephoneId = table.Column<Guid>(nullable: false),
                    TelephoneNumber = table.Column<string>(nullable: true),
                    TelephoneType = table.Column<int>(nullable: false),
                    IndividualId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Telephones", x => x.TelephoneId);
                    table.ForeignKey(
                        name: "FK_Telephones_Individuals_IndividualId",
                        column: x => x.IndividualId,
                        principalTable: "Individuals",
                        principalColumn: "IndividualId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reaties",
                columns: table => new
                {
                    RealtyId = table.Column<Guid>(nullable: false),
                    BeneficiaryId = table.Column<Guid>(nullable: false),
                    RealtyAddressId = table.Column<Guid>(nullable: false),
                    RealtyMunicipalRegistration = table.Column<string>(nullable: true),
                    RealtyConstructionDate = table.Column<DateTime>(nullable: false),
                    RealtySaleValue = table.Column<double>(nullable: false),
                    RealtyMarketValue = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reaties", x => x.RealtyId);
                    table.ForeignKey(
                        name: "FK_Reaties_Addresses_RealtyAddressId",
                        column: x => x.RealtyAddressId,
                        principalTable: "Addresses",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_IndividualId",
                table: "Addresses",
                column: "IndividualId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_ContractIndividualId",
                table: "Contracts",
                column: "ContractIndividualId");

            migrationBuilder.CreateIndex(
                name: "IX_Individuals_ContractIndividualId",
                table: "Individuals",
                column: "ContractIndividualId");

            migrationBuilder.CreateIndex(
                name: "IX_Reaties_RealtyAddressId",
                table: "Reaties",
                column: "RealtyAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Telephones_IndividualId",
                table: "Telephones",
                column: "IndividualId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.DropTable(
                name: "MobileDevices");

            migrationBuilder.DropTable(
                name: "Pets");

            migrationBuilder.DropTable(
                name: "Reaties");

            migrationBuilder.DropTable(
                name: "Telephones");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Individuals");

            migrationBuilder.DropTable(
                name: "ContractIndividuals");
        }
    }
}
