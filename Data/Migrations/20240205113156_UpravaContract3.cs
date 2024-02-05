using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZaverecnyProjektForman2.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpravaContract3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
