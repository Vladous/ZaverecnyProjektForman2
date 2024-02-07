using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZaverecnyProjektForman2.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixForeignKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
    name: "FK_Insurances_AspNetUsers_UserLastChangedId",
    table: "Insurances",
    column: "UserLastChangedId",
    principalTable: "AspNetUsers",
    principalColumn: "Id",
    onDelete: ReferentialAction.Restrict // Změňte z CASCADE na RESTRICT nebo NO ACTION podle potřeby
);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
