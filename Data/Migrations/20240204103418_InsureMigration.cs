using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZaverecnyProjektForman2.Data.Migrations
{
    /// <inheritdoc />
    public partial class InsureMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Insured_AspNetUsers_UserCreatedId",
                table: "Insured");

            migrationBuilder.DropForeignKey(
                name: "FK_Insured_AspNetUsers_UserLastChangedId",
                table: "Insured");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Insured",
                table: "Insured");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Insured");

            migrationBuilder.RenameTable(
                name: "Insured",
                newName: "Insureds");

            migrationBuilder.RenameIndex(
                name: "IX_Insured_UserLastChangedId",
                table: "Insureds",
                newName: "IX_Insureds_UserLastChangedId");

            migrationBuilder.RenameIndex(
                name: "IX_Insured_UserCreatedId",
                table: "Insureds",
                newName: "IX_Insureds_UserCreatedId");

            migrationBuilder.AlterColumn<string>(
                name: "Surname",
                table: "Insureds",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Street",
                table: "Insureds",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Insureds",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Insureds",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Insureds",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Insureds",
                table: "Insureds",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Insurances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameSubject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    InsuredFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InsuredUntil = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastChange = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserCreatedId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UserLastChangedId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Insurances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Insurances_AspNetUsers_UserCreatedId",
                        column: x => x.UserCreatedId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Insurances_AspNetUsers_UserLastChangedId",
                        column: x => x.UserLastChangedId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InsuranceContracts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InsuredId = table.Column<int>(type: "int", nullable: false),
                    InsuranceId = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastChange = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserCreatedId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UserLastChangedId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsuranceContracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InsuranceContracts_AspNetUsers_UserCreatedId",
                        column: x => x.UserCreatedId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InsuranceContracts_AspNetUsers_UserLastChangedId",
                        column: x => x.UserLastChangedId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InsuranceContracts_Insurances_InsuranceId",
                        column: x => x.InsuranceId,
                        principalTable: "Insurances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InsuranceContracts_Insureds_InsuredId",
                        column: x => x.InsuredId,
                        principalTable: "Insureds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InsuranceEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InsuredId = table.Column<int>(type: "int", nullable: true),
                    InsuranceId = table.Column<int>(type: "int", nullable: true),
                    FulfillmentAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    FulfillmentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastChange = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserCreatedId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UserLastChangedId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsuranceEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InsuranceEvents_AspNetUsers_UserCreatedId",
                        column: x => x.UserCreatedId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InsuranceEvents_AspNetUsers_UserLastChangedId",
                        column: x => x.UserLastChangedId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InsuranceEvents_Insurances_InsuranceId",
                        column: x => x.InsuranceId,
                        principalTable: "Insurances",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InsuranceEvents_Insureds_InsuredId",
                        column: x => x.InsuredId,
                        principalTable: "Insureds",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceContracts_InsuranceId",
                table: "InsuranceContracts",
                column: "InsuranceId");

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceContracts_InsuredId",
                table: "InsuranceContracts",
                column: "InsuredId");

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceContracts_UserCreatedId",
                table: "InsuranceContracts",
                column: "UserCreatedId");

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceContracts_UserLastChangedId",
                table: "InsuranceContracts",
                column: "UserLastChangedId");

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceEvents_InsuranceId",
                table: "InsuranceEvents",
                column: "InsuranceId");

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceEvents_InsuredId",
                table: "InsuranceEvents",
                column: "InsuredId");

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceEvents_UserCreatedId",
                table: "InsuranceEvents",
                column: "UserCreatedId");

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceEvents_UserLastChangedId",
                table: "InsuranceEvents",
                column: "UserLastChangedId");

            migrationBuilder.CreateIndex(
                name: "IX_Insurances_UserCreatedId",
                table: "Insurances",
                column: "UserCreatedId");

            migrationBuilder.CreateIndex(
                name: "IX_Insurances_UserLastChangedId",
                table: "Insurances",
                column: "UserLastChangedId");

            migrationBuilder.AddForeignKey(
                name: "FK_Insureds_AspNetUsers_UserCreatedId",
                table: "Insureds",
                column: "UserCreatedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Insureds_AspNetUsers_UserLastChangedId",
                table: "Insureds",
                column: "UserLastChangedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Insureds_AspNetUsers_UserCreatedId",
                table: "Insureds");

            migrationBuilder.DropForeignKey(
                name: "FK_Insureds_AspNetUsers_UserLastChangedId",
                table: "Insureds");

            migrationBuilder.DropTable(
                name: "InsuranceContracts");

            migrationBuilder.DropTable(
                name: "InsuranceEvents");

            migrationBuilder.DropTable(
                name: "Insurances");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Insureds",
                table: "Insureds");

            migrationBuilder.RenameTable(
                name: "Insureds",
                newName: "Insured");

            migrationBuilder.RenameIndex(
                name: "IX_Insureds_UserLastChangedId",
                table: "Insured",
                newName: "IX_Insured_UserLastChangedId");

            migrationBuilder.RenameIndex(
                name: "IX_Insureds_UserCreatedId",
                table: "Insured",
                newName: "IX_Insured_UserCreatedId");

            migrationBuilder.AlterColumn<string>(
                name: "Surname",
                table: "Insured",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Street",
                table: "Insured",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Insured",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Insured",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Insured",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Insured",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Insured",
                table: "Insured",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Insured_AspNetUsers_UserCreatedId",
                table: "Insured",
                column: "UserCreatedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Insured_AspNetUsers_UserLastChangedId",
                table: "Insured",
                column: "UserLastChangedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
