using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Unit_Data.Migrations
{
    public partial class smollfix_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_MessageAppUserVM_UserAndChatid",
                table: "Messages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MessageAppUserVM",
                table: "MessageAppUserVM");

            migrationBuilder.RenameTable(
                name: "MessageAppUserVM",
                newName: "MessageUserVMs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MessageUserVMs",
                table: "MessageUserVMs",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_MessageUserVMs_UserAndChatid",
                table: "Messages",
                column: "UserAndChatid",
                principalTable: "MessageUserVMs",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_MessageUserVMs_UserAndChatid",
                table: "Messages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MessageUserVMs",
                table: "MessageUserVMs");

            migrationBuilder.RenameTable(
                name: "MessageUserVMs",
                newName: "MessageAppUserVM");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MessageAppUserVM",
                table: "MessageAppUserVM",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_MessageAppUserVM_UserAndChatid",
                table: "Messages",
                column: "UserAndChatid",
                principalTable: "MessageAppUserVM",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
