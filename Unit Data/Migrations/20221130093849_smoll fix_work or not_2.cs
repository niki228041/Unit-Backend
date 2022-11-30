using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Unit_Data.Migrations
{
    public partial class smollfix_workornot_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUserContacts_AspNetUsers_AppUser",
                table: "AppUserContacts");

            migrationBuilder.DropIndex(
                name: "IX_AppUserContacts_AppUser",
                table: "AppUserContacts");

            migrationBuilder.DropColumn(
                name: "AppUser",
                table: "AppUserContacts");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "AppUserContacts",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_AppUserContacts_UserId",
                table: "AppUserContacts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserContacts_AspNetUsers_UserId",
                table: "AppUserContacts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUserContacts_AspNetUsers_UserId",
                table: "AppUserContacts");

            migrationBuilder.DropIndex(
                name: "IX_AppUserContacts_UserId",
                table: "AppUserContacts");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "AppUserContacts",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "AppUser",
                table: "AppUserContacts",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppUserContacts_AppUser",
                table: "AppUserContacts",
                column: "AppUser");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserContacts_AspNetUsers_AppUser",
                table: "AppUserContacts",
                column: "AppUser",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
