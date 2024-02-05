using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZaverecnyProjektForman2.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpravaInsurance1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InsuredFrom",
                table: "Insurances");

            migrationBuilder.DropColumn(
                name: "InsuredUntil",
                table: "Insurances");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "InsuredFrom",
                table: "Insurances",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InsuredUntil",
                table: "Insurances",
                type: "datetime2",
                nullable: true);
        }
    }
}
