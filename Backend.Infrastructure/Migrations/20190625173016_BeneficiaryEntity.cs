using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Infrastructure.Migrations
{
    public partial class BeneficiaryEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Beneficiary_Address_Beneficiary_BeneficiaryId",
                table: "Beneficiary_Address");

            migrationBuilder.DropForeignKey(
                name: "FK_Contract_Beneficiary_Beneficiary_BeneficiaryId",
                table: "Contract_Beneficiary");

            migrationBuilder.DropForeignKey(
                name: "FK_Individual_Telephone_Beneficiary_BeneficiaryId",
                table: "Individual_Telephone");

            migrationBuilder.DropForeignKey(
                name: "FK_SignedContracts_Beneficiary_BeneficiaryId",
                table: "SignedContracts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Beneficiary",
                table: "Beneficiary");

            migrationBuilder.RenameTable(
                name: "Beneficiary",
                newName: "BeneficiaryEntity");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BeneficiaryEntity",
                table: "BeneficiaryEntity",
                column: "BeneficiaryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Beneficiary_Address_BeneficiaryEntity_BeneficiaryId",
                table: "Beneficiary_Address",
                column: "BeneficiaryId",
                principalTable: "BeneficiaryEntity",
                principalColumn: "BeneficiaryId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_Beneficiary_BeneficiaryEntity_BeneficiaryId",
                table: "Contract_Beneficiary",
                column: "BeneficiaryId",
                principalTable: "BeneficiaryEntity",
                principalColumn: "BeneficiaryId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Individual_Telephone_BeneficiaryEntity_BeneficiaryId",
                table: "Individual_Telephone",
                column: "BeneficiaryId",
                principalTable: "BeneficiaryEntity",
                principalColumn: "BeneficiaryId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SignedContracts_BeneficiaryEntity_BeneficiaryId",
                table: "SignedContracts",
                column: "BeneficiaryId",
                principalTable: "BeneficiaryEntity",
                principalColumn: "BeneficiaryId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Beneficiary_Address_BeneficiaryEntity_BeneficiaryId",
                table: "Beneficiary_Address");

            migrationBuilder.DropForeignKey(
                name: "FK_Contract_Beneficiary_BeneficiaryEntity_BeneficiaryId",
                table: "Contract_Beneficiary");

            migrationBuilder.DropForeignKey(
                name: "FK_Individual_Telephone_BeneficiaryEntity_BeneficiaryId",
                table: "Individual_Telephone");

            migrationBuilder.DropForeignKey(
                name: "FK_SignedContracts_BeneficiaryEntity_BeneficiaryId",
                table: "SignedContracts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BeneficiaryEntity",
                table: "BeneficiaryEntity");

            migrationBuilder.RenameTable(
                name: "BeneficiaryEntity",
                newName: "Beneficiary");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Beneficiary",
                table: "Beneficiary",
                column: "BeneficiaryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Beneficiary_Address_Beneficiary_BeneficiaryId",
                table: "Beneficiary_Address",
                column: "BeneficiaryId",
                principalTable: "Beneficiary",
                principalColumn: "BeneficiaryId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_Beneficiary_Beneficiary_BeneficiaryId",
                table: "Contract_Beneficiary",
                column: "BeneficiaryId",
                principalTable: "Beneficiary",
                principalColumn: "BeneficiaryId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Individual_Telephone_Beneficiary_BeneficiaryId",
                table: "Individual_Telephone",
                column: "BeneficiaryId",
                principalTable: "Beneficiary",
                principalColumn: "BeneficiaryId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SignedContracts_Beneficiary_BeneficiaryId",
                table: "SignedContracts",
                column: "BeneficiaryId",
                principalTable: "Beneficiary",
                principalColumn: "BeneficiaryId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
