using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Unit_Data.Migrations
{
    public partial class justtry_5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUserVM_Chats_AppUser",
                table: "AppUserVM");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppUserVM",
                table: "AppUserVM");

            migrationBuilder.RenameTable(
                name: "AppUserVM",
                newName: "UserVMs");

            migrationBuilder.RenameColumn(
                name: "AppUser",
                table: "UserVMs",
                newName: "ChatId");

            migrationBuilder.RenameIndex(
                name: "IX_AppUserVM_AppUser",
                table: "UserVMs",
                newName: "IX_UserVMs_ChatId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserVMs",
                table: "UserVMs",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserVMs_Chats_ChatId",
                table: "UserVMs",
                column: "ChatId",
                principalTable: "Chats",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserVMs_Chats_ChatId",
                table: "UserVMs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserVMs",
                table: "UserVMs");

            migrationBuilder.RenameTable(
                name: "UserVMs",
                newName: "AppUserVM");

            migrationBuilder.RenameColumn(
                name: "ChatId",
                table: "AppUserVM",
                newName: "AppUser");

            migrationBuilder.RenameIndex(
                name: "IX_UserVMs_ChatId",
                table: "AppUserVM",
                newName: "IX_AppUserVM_AppUser");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppUserVM",
                table: "AppUserVM",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserVM_Chats_AppUser",
                table: "AppUserVM",
                column: "AppUser",
                principalTable: "Chats",
                principalColumn: "Id");
        }
    }
}
