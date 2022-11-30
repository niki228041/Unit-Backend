using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Unit_Data.Migrations
{
    public partial class justtry_6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserVMs_Chats_ChatId",
                table: "UserVMs");

            migrationBuilder.AlterColumn<int>(
                name: "ChatId",
                table: "UserVMs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserVMs_Chats_ChatId",
                table: "UserVMs",
                column: "ChatId",
                principalTable: "Chats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserVMs_Chats_ChatId",
                table: "UserVMs");

            migrationBuilder.AlterColumn<int>(
                name: "ChatId",
                table: "UserVMs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_UserVMs_Chats_ChatId",
                table: "UserVMs",
                column: "ChatId",
                principalTable: "Chats",
                principalColumn: "Id");
        }
    }
}
