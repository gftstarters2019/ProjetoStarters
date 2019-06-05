using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Infrastructure.Migrations
{
    public partial class ChangeddbContexttoBeneficiary_Address : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Individual_Address_Addresses_AddressId",
                table: "Individual_Address");

            migrationBuilder.DropForeignKey(
                name: "FK_Individual_Address_Beneficiaries_BeneficiaryId",
                table: "Individual_Address");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Individual_Address",
                table: "Individual_Address");

            migrationBuilder.RenameTable(
                name: "Individual_Address",
                newName: "Beneficiary_Address");

            migrationBuilder.RenameIndex(
                name: "IX_Individual_Address_BeneficiaryId",
                table: "Beneficiary_Address",
                newName: "IX_Beneficiary_Address_BeneficiaryId");

            migrationBuilder.RenameIndex(
                name: "IX_Individual_Address_AddressId",
                table: "Beneficiary_Address",
                newName: "IX_Beneficiary_Address_AddressId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Beneficiary_Address",
                table: "Beneficiary_Address",
                column: "BeneficiaryAddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Beneficiary_Address_Addresses_AddressId",
                table: "Beneficiary_Address",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Beneficiary_Address_Beneficiaries_BeneficiaryId",
                table: "Beneficiary_Address",
                column: "BeneficiaryId",
                principalTable: "Beneficiaries",
                principalColumn: "BeneficiaryId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Beneficiary_Address_Addresses_AddressId",
                table: "Beneficiary_Address");

            migrationBuilder.DropForeignKey(
                name: "FK_Beneficiary_Address_Beneficiaries_BeneficiaryId",
                table: "Beneficiary_Address");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Beneficiary_Address",
                table: "Beneficiary_Address");

            migrationBuilder.RenameTable(
                name: "Beneficiary_Address",
                newName: "Individual_Address");

            migrationBuilder.RenameIndex(
                name: "IX_Beneficiary_Address_BeneficiaryId",
                table: "Individual_Address",
                newName: "IX_Individual_Address_BeneficiaryId");

            migrationBuilder.RenameIndex(
                name: "IX_Beneficiary_Address_AddressId",
                table: "Individual_Address",
                newName: "IX_Individual_Address_AddressId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Individual_Address",
                table: "Individual_Address",
                column: "BeneficiaryAddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Individual_Address_Addresses_AddressId",
                table: "Individual_Address",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Individual_Address_Beneficiaries_BeneficiaryId",
                table: "Individual_Address",
                column: "BeneficiaryId",
                principalTable: "Beneficiaries",
                principalColumn: "BeneficiaryId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
