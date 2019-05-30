using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    AddressType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.AddressId);
                });

            migrationBuilder.CreateTable(
                name: "Beneficiaries",
                columns: table => new
                {
                    BeneficiaryId = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    IndividualId = table.Column<Guid>(nullable: true),
                    IndividualName = table.Column<string>(maxLength: 50, nullable: true),
                    IndividualCPF = table.Column<string>(maxLength: 11, nullable: true),
                    IndividualRG = table.Column<string>(maxLength: 9, nullable: true),
                    IndividualEmail = table.Column<string>(maxLength: 30, nullable: true),
                    IndividualBirthdate = table.Column<DateTime>(nullable: true),
                    MobileDeviceId = table.Column<Guid>(nullable: true),
                    MobileDeviceBrand = table.Column<string>(maxLength: 15, nullable: true),
                    MobileDeviceModel = table.Column<string>(maxLength: 20, nullable: true),
                    MobileDeviceSerialNumber = table.Column<string>(maxLength: 40, nullable: true),
                    MobileDeviceManufactoringYear = table.Column<DateTime>(nullable: true),
                    MobileDeviceType = table.Column<int>(nullable: true),
                    MobileDeviceInvoiceValue = table.Column<double>(nullable: true),
                    PetId = table.Column<Guid>(nullable: true),
                    PetName = table.Column<string>(maxLength: 40, nullable: true),
                    PetSpecies = table.Column<int>(nullable: true),
                    PetBreed = table.Column<string>(maxLength: 30, nullable: true),
                    PetBirthdate = table.Column<DateTime>(nullable: true),
                    RealtyId = table.Column<Guid>(nullable: true),
                    RealtyMunicipalRegistration = table.Column<string>(maxLength: 50, nullable: true),
                    RealtyConstructionDate = table.Column<DateTime>(nullable: true),
                    RealtySaleValue = table.Column<double>(nullable: true),
                    RealtyMarketValue = table.Column<double>(nullable: true),
                    VehicleId = table.Column<Guid>(nullable: true),
                    VehicleBrand = table.Column<string>(nullable: true),
                    VehicleModel = table.Column<string>(nullable: true),
                    VehicleManufactoringYear = table.Column<DateTime>(nullable: true),
                    VehicleColor = table.Column<int>(nullable: true),
                    VehicleModelYear = table.Column<DateTime>(nullable: true),
                    VehicleChassisNumber = table.Column<string>(nullable: true),
                    VehicleCurrentMileage = table.Column<int>(nullable: true),
                    VehicleCurrentFipeValue = table.Column<double>(nullable: true),
                    VehicleDoneInspection = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beneficiaries", x => x.BeneficiaryId);
                });

            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    ContractId = table.Column<Guid>(nullable: false),
                    ContractType = table.Column<int>(nullable: false),
                    ContractCategory = table.Column<int>(nullable: false),
                    ContractExpiryDate = table.Column<DateTime>(nullable: false),
                    ContractDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.ContractId);
                });

            migrationBuilder.CreateTable(
                name: "Telephones",
                columns: table => new
                {
                    TelephoneId = table.Column<Guid>(nullable: false),
                    TelephoneNumber = table.Column<string>(nullable: true),
                    TelephoneType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Telephones", x => x.TelephoneId);
                });

            migrationBuilder.CreateTable(
                name: "Individual_Address",
                columns: table => new
                {
                    BeneficiaryAddressId = table.Column<Guid>(nullable: false),
                    AddressId = table.Column<Guid>(nullable: false),
                    BeneficiaryId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Individual_Address", x => x.BeneficiaryAddressId);
                    table.ForeignKey(
                        name: "FK_Individual_Address_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Individual_Address_Beneficiaries_BeneficiaryId",
                        column: x => x.BeneficiaryId,
                        principalTable: "Beneficiaries",
                        principalColumn: "BeneficiaryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SignedContracts",
                columns: table => new
                {
                    ContractSignedId = table.Column<Guid>(nullable: false),
                    IndividualId = table.Column<Guid>(nullable: false),
                    ContractId = table.Column<Guid>(nullable: false),
                    ContractIndividualIsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SignedContracts", x => x.ContractSignedId);
                    table.ForeignKey(
                        name: "FK_SignedContracts_Contracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contracts",
                        principalColumn: "ContractId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SignedContracts_Beneficiaries_IndividualId",
                        column: x => x.IndividualId,
                        principalTable: "Beneficiaries",
                        principalColumn: "BeneficiaryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Individual_Telephone",
                columns: table => new
                {
                    BeneficiaryTelephoneId = table.Column<Guid>(nullable: false),
                    TelephoneId = table.Column<Guid>(nullable: false),
                    BeneficiaryId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Individual_Telephone", x => x.BeneficiaryTelephoneId);
                    table.ForeignKey(
                        name: "FK_Individual_Telephone_Beneficiaries_BeneficiaryId",
                        column: x => x.BeneficiaryId,
                        principalTable: "Beneficiaries",
                        principalColumn: "BeneficiaryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Individual_Telephone_Telephones_TelephoneId",
                        column: x => x.TelephoneId,
                        principalTable: "Telephones",
                        principalColumn: "TelephoneId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contract_Beneficiary",
                columns: table => new
                {
                    ContractBeneficiaryId = table.Column<Guid>(nullable: false),
                    SignedContractId = table.Column<Guid>(nullable: false),
                    BeneficiaryId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contract_Beneficiary", x => x.ContractBeneficiaryId);
                    table.ForeignKey(
                        name: "FK_Contract_Beneficiary_Beneficiaries_BeneficiaryId",
                        column: x => x.BeneficiaryId,
                        principalTable: "Beneficiaries",
                        principalColumn: "BeneficiaryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contract_Beneficiary_SignedContracts_SignedContractId",
                        column: x => x.SignedContractId,
                        principalTable: "SignedContracts",
                        principalColumn: "ContractSignedId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contract_Beneficiary_BeneficiaryId",
                table: "Contract_Beneficiary",
                column: "BeneficiaryId");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_Beneficiary_SignedContractId",
                table: "Contract_Beneficiary",
                column: "SignedContractId");

            migrationBuilder.CreateIndex(
                name: "IX_Individual_Address_AddressId",
                table: "Individual_Address",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Individual_Address_BeneficiaryId",
                table: "Individual_Address",
                column: "BeneficiaryId");

            migrationBuilder.CreateIndex(
                name: "IX_Individual_Telephone_BeneficiaryId",
                table: "Individual_Telephone",
                column: "BeneficiaryId");

            migrationBuilder.CreateIndex(
                name: "IX_Individual_Telephone_TelephoneId",
                table: "Individual_Telephone",
                column: "TelephoneId");

            migrationBuilder.CreateIndex(
                name: "IX_SignedContracts_ContractId",
                table: "SignedContracts",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_SignedContracts_IndividualId",
                table: "SignedContracts",
                column: "IndividualId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contract_Beneficiary");

            migrationBuilder.DropTable(
                name: "Individual_Address");

            migrationBuilder.DropTable(
                name: "Individual_Telephone");

            migrationBuilder.DropTable(
                name: "SignedContracts");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Telephones");

            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.DropTable(
                name: "Beneficiaries");
        }
    }
}
