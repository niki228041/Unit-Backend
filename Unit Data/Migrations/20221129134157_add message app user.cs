using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Unit_Data.Migrations
{
    public partial class addmessageappuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AspNetUsers_UserId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_UserId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Messages");

            migrationBuilder.AddColumn<int>(
                name: "UserAndChatid",
                table: "Messages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "MessageAppUserVM",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageAppUserVM", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Messages_UserAndChatid",
                table: "Messages",
                column: "UserAndChatid");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_MessageAppUserVM_UserAndChatid",
                table: "Messages",
                column: "UserAndChatid",
                principalTable: "MessageAppUserVM",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_MessageAppUserVM_UserAndChatid",
                table: "Messages");

            migrationBuilder.DropTable(
                name: "MessageAppUserVM");

            migrationBuilder.DropIndex(
                name: "IX_Messages_UserAndChatid",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "UserAndChatid",
                table: "Messages");

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Messages",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Messages",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_UserId",
                table: "Messages",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AspNetUsers_UserId",
                table: "Messages",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
