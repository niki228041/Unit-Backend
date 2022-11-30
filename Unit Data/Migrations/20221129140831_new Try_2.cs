using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Unit_Data.Migrations
{
    public partial class newTry_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_MessageUserVMs_UserAndChatid",
                table: "Messages");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "MessageUserVMs",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "UserAndChatid",
                table: "Messages",
                newName: "UserAndChatId");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_UserAndChatid",
                table: "Messages",
                newName: "IX_Messages_UserAndChatId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_MessageUserVMs_UserAndChatId",
                table: "Messages",
                column: "UserAndChatId",
                principalTable: "MessageUserVMs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_MessageUserVMs_UserAndChatId",
                table: "Messages");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "MessageUserVMs",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserAndChatId",
                table: "Messages",
                newName: "UserAndChatid");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_UserAndChatId",
                table: "Messages",
                newName: "IX_Messages_UserAndChatid");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_MessageUserVMs_UserAndChatid",
                table: "Messages",
                column: "UserAndChatid",
                principalTable: "MessageUserVMs",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
