using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZaverecnyProjektForman2.Data.Migrations
{
    /// <inheritdoc />
    public partial class UserInfo2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InsuranceContracts_AspNetUsers_UserCreatedId",
                table: "InsuranceContracts");

            migrationBuilder.DropForeignKey(
                name: "FK_InsuranceContracts_AspNetUsers_UserLastChangedId",
                table: "InsuranceContracts");

            migrationBuilder.DropForeignKey(
                name: "FK_InsuranceEvents_AspNetUsers_UserCreatedId",
                table: "InsuranceEvents");

            migrationBuilder.DropForeignKey(
                name: "FK_InsuranceEvents_AspNetUsers_UserLastChangedId",
                table: "InsuranceEvents");

            migrationBuilder.DropForeignKey(
                name: "FK_Insurances_AspNetUsers_UserCreatedId",
                table: "Insurances");

            migrationBuilder.DropForeignKey(
                name: "FK_Insurances_AspNetUsers_UserLastChangedId",
                table: "Insurances");

            migrationBuilder.DropForeignKey(
                name: "FK_Insureds_AspNetUsers_UserCreatedId",
                table: "Insureds");

            migrationBuilder.DropForeignKey(
                name: "FK_Insureds_AspNetUsers_UserLastChangedId",
                table: "Insureds");

            migrationBuilder.DropIndex(
                name: "IX_Insureds_UserCreatedId",
                table: "Insureds");

            migrationBuilder.DropIndex(
                name: "IX_Insureds_UserLastChangedId",
                table: "Insureds");

            migrationBuilder.DropIndex(
                name: "IX_Insurances_UserCreatedId",
                table: "Insurances");

            migrationBuilder.DropIndex(
                name: "IX_Insurances_UserLastChangedId",
                table: "Insurances");

            migrationBuilder.DropIndex(
                name: "IX_InsuranceEvents_UserCreatedId",
                table: "InsuranceEvents");

            migrationBuilder.DropIndex(
                name: "IX_InsuranceEvents_UserLastChangedId",
                table: "InsuranceEvents");

            migrationBuilder.DropIndex(
                name: "IX_InsuranceContracts_UserCreatedId",
                table: "InsuranceContracts");

            migrationBuilder.DropIndex(
                name: "IX_InsuranceContracts_UserLastChangedId",
                table: "InsuranceContracts");

            migrationBuilder.DropColumn(
                name: "UserCreatedId",
                table: "Insureds");

            migrationBuilder.DropColumn(
                name: "UserLastChangedId",
                table: "Insureds");

            migrationBuilder.DropColumn(
                name: "UserCreatedId",
                table: "Insurances");

            migrationBuilder.DropColumn(
                name: "UserLastChangedId",
                table: "Insurances");

            migrationBuilder.DropColumn(
                name: "UserCreatedId",
                table: "InsuranceEvents");

            migrationBuilder.DropColumn(
                name: "UserLastChangedId",
                table: "InsuranceEvents");

            migrationBuilder.DropColumn(
                name: "UserCreatedId",
                table: "InsuranceContracts");

            migrationBuilder.DropColumn(
                name: "UserLastChangedId",
                table: "InsuranceContracts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserCreatedId",
                table: "Insureds",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserLastChangedId",
                table: "Insureds",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserCreatedId",
                table: "Insurances",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserLastChangedId",
                table: "Insurances",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserCreatedId",
                table: "InsuranceEvents",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserLastChangedId",
                table: "InsuranceEvents",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserCreatedId",
                table: "InsuranceContracts",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserLastChangedId",
                table: "InsuranceContracts",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Insureds_UserCreatedId",
                table: "Insureds",
                column: "UserCreatedId");

            migrationBuilder.CreateIndex(
                name: "IX_Insureds_UserLastChangedId",
                table: "Insureds",
                column: "UserLastChangedId");

            migrationBuilder.CreateIndex(
                name: "IX_Insurances_UserCreatedId",
                table: "Insurances",
                column: "UserCreatedId");

            migrationBuilder.CreateIndex(
                name: "IX_Insurances_UserLastChangedId",
                table: "Insurances",
                column: "UserLastChangedId");

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceEvents_UserCreatedId",
                table: "InsuranceEvents",
                column: "UserCreatedId");

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceEvents_UserLastChangedId",
                table: "InsuranceEvents",
                column: "UserLastChangedId");

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceContracts_UserCreatedId",
                table: "InsuranceContracts",
                column: "UserCreatedId");

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceContracts_UserLastChangedId",
                table: "InsuranceContracts",
                column: "UserLastChangedId");

            migrationBuilder.AddForeignKey(
                name: "FK_InsuranceContracts_AspNetUsers_UserCreatedId",
                table: "InsuranceContracts",
                column: "UserCreatedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InsuranceContracts_AspNetUsers_UserLastChangedId",
                table: "InsuranceContracts",
                column: "UserLastChangedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InsuranceEvents_AspNetUsers_UserCreatedId",
                table: "InsuranceEvents",
                column: "UserCreatedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InsuranceEvents_AspNetUsers_UserLastChangedId",
                table: "InsuranceEvents",
                column: "UserLastChangedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Insurances_AspNetUsers_UserCreatedId",
                table: "Insurances",
                column: "UserCreatedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Insurances_AspNetUsers_UserLastChangedId",
                table: "Insurances",
                column: "UserLastChangedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
    }
}
