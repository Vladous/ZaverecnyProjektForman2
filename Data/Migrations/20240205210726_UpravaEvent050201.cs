using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZaverecnyProjektForman2.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpravaEvent050201 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InsuranceContractsId1",
                table: "InsuranceEvents",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceEvents_InsuranceContractsId1",
                table: "InsuranceEvents",
                column: "InsuranceContractsId1");

            migrationBuilder.AddForeignKey(
                name: "FK_InsuranceEvents_InsuranceEvents_InsuranceContractsId1",
                table: "InsuranceEvents",
                column: "InsuranceContractsId1",
                principalTable: "InsuranceEvents",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InsuranceEvents_InsuranceEvents_InsuranceContractsId1",
                table: "InsuranceEvents");

            migrationBuilder.DropIndex(
                name: "IX_InsuranceEvents_InsuranceContractsId1",
                table: "InsuranceEvents");

            migrationBuilder.DropColumn(
                name: "InsuranceContractsId1",
                table: "InsuranceEvents");
        }
    }
}
