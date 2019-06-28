using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Infrastructure.Migrations
{
    public partial class Nome : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SignedContracts_Beneficiary_SignedContractIndividualBeneficiaryId",
                table: "SignedContracts");

            migrationBuilder.DropIndex(
                name: "IX_SignedContracts_SignedContractIndividualBeneficiaryId",
                table: "SignedContracts");

            migrationBuilder.DropColumn(
                name: "SignedContractIndividualBeneficiaryId",
                table: "SignedContracts");

            migrationBuilder.RenameColumn(
                name: "IndividualId",
                table: "SignedContracts",
                newName: "BeneficiaryId");

            migrationBuilder.CreateIndex(
                name: "IX_SignedContracts_BeneficiaryId",
                table: "SignedContracts",
                column: "BeneficiaryId");

            migrationBuilder.AddForeignKey(
                name: "FK_SignedContracts_Beneficiary_BeneficiaryId",
                table: "SignedContracts",
                column: "BeneficiaryId",
                principalTable: "Beneficiary",
                principalColumn: "BeneficiaryId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SignedContracts_Beneficiary_BeneficiaryId",
                table: "SignedContracts");

            migrationBuilder.DropIndex(
                name: "IX_SignedContracts_BeneficiaryId",
                table: "SignedContracts");

            migrationBuilder.RenameColumn(
                name: "BeneficiaryId",
                table: "SignedContracts",
                newName: "IndividualId");

            migrationBuilder.AddColumn<Guid>(
                name: "SignedContractIndividualBeneficiaryId",
                table: "SignedContracts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SignedContracts_SignedContractIndividualBeneficiaryId",
                table: "SignedContracts",
                column: "SignedContractIndividualBeneficiaryId");

            migrationBuilder.AddForeignKey(
                name: "FK_SignedContracts_Beneficiary_SignedContractIndividualBeneficiaryId",
                table: "SignedContracts",
                column: "SignedContractIndividualBeneficiaryId",
                principalTable: "Beneficiary",
                principalColumn: "BeneficiaryId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
