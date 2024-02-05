using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZaverecnyProjektForman2.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpravaInsurance2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameSubject",
                table: "Insurances");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NameSubject",
                table: "Insurances",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
