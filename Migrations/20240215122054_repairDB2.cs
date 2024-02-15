using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZaverecnyProjektForman2.Migrations
{
    /// <inheritdoc />
    public partial class repairDB2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InsuranceEvents_InsuranceContracts_InsuranceContractsId",
                table: "InsuranceEvents");

            migrationBuilder.DropIndex(
                name: "IX_InsuranceEvents_InsuranceContractsId",
                table: "InsuranceEvents");

            migrationBuilder.DropColumn(
                name: "InsuranceContractsId",
                table: "InsuranceEvents");

            migrationBuilder.AddColumn<int>(
                name: "InsuranceContractId",
                table: "InsuranceEvents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceEvents_InsuranceContractId",
                table: "InsuranceEvents",
                column: "InsuranceContractId");

            migrationBuilder.AddForeignKey(
                name: "FK_InsuranceEvents_InsuranceContracts_InsuranceContractId",
                table: "InsuranceEvents",
                column: "InsuranceContractId",
                principalTable: "InsuranceContracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InsuranceEvents_InsuranceContracts_InsuranceContractId",
                table: "InsuranceEvents");

            migrationBuilder.DropIndex(
                name: "IX_InsuranceEvents_InsuranceContractId",
                table: "InsuranceEvents");

            migrationBuilder.DropColumn(
                name: "InsuranceContractId",
                table: "InsuranceEvents");

            migrationBuilder.AddColumn<int>(
                name: "InsuranceContractsId",
                table: "InsuranceEvents",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceEvents_InsuranceContractsId",
                table: "InsuranceEvents",
                column: "InsuranceContractsId");

            migrationBuilder.AddForeignKey(
                name: "FK_InsuranceEvents_InsuranceContracts_InsuranceContractsId",
                table: "InsuranceEvents",
                column: "InsuranceContractsId",
                principalTable: "InsuranceContracts",
                principalColumn: "Id");
        }
    }
}
