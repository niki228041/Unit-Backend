using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Unit_Data.Migrations
{
    public partial class justtry_4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUserVM_Chats_ChatId",
                table: "AppUserVM");

            migrationBuilder.RenameColumn(
                name: "ChatId",
                table: "AppUserVM",
                newName: "AppUser");

            migrationBuilder.RenameIndex(
                name: "IX_AppUserVM_ChatId",
                table: "AppUserVM",
                newName: "IX_AppUserVM_AppUser");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserVM_Chats_AppUser",
                table: "AppUserVM",
                column: "AppUser",
                principalTable: "Chats",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUserVM_Chats_AppUser",
                table: "AppUserVM");

            migrationBuilder.RenameColumn(
                name: "AppUser",
                table: "AppUserVM",
                newName: "ChatId");

            migrationBuilder.RenameIndex(
                name: "IX_AppUserVM_AppUser",
                table: "AppUserVM",
                newName: "IX_AppUserVM_ChatId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserVM_Chats_ChatId",
                table: "AppUserVM",
                column: "ChatId",
                principalTable: "Chats",
                principalColumn: "Id");
        }
    }
}
