using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZaverecnyProjektForman2.Data.Migrations
{
    /// <inheritdoc />
    public partial class InsuredMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Insured",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Birth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HouseNumber = table.Column<int>(type: "int", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostNumber = table.Column<int>(type: "int", nullable: true),
                    Phone = table.Column<int>(type: "int", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastChange = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserCreatedId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UserLastChangedId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Insured", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Insured_AspNetUsers_UserCreatedId",
                        column: x => x.UserCreatedId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Insured_AspNetUsers_UserLastChangedId",
                        column: x => x.UserLastChangedId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Insured_UserCreatedId",
                table: "Insured",
                column: "UserCreatedId");

            migrationBuilder.CreateIndex(
                name: "IX_Insured_UserLastChangedId",
                table: "Insured",
                column: "UserLastChangedId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Insured");
        }
    }
}
