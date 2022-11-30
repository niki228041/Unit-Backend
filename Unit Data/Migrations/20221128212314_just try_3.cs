using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Unit_Data.Migrations
{
    public partial class justtry_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUserChat");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Chats",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AppUserVM",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChatId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserVM", x => x.id);
                    table.ForeignKey(
                        name: "FK_AppUserVM_Chats_ChatId",
                        column: x => x.ChatId,
                        principalTable: "Chats",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Chats_AppUserId",
                table: "Chats",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUserVM_ChatId",
                table: "AppUserVM",
                column: "ChatId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_AspNetUsers_AppUserId",
                table: "Chats",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chats_AspNetUsers_AppUserId",
                table: "Chats");

            migrationBuilder.DropTable(
                name: "AppUserVM");

            migrationBuilder.DropIndex(
                name: "IX_Chats_AppUserId",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Chats");

            migrationBuilder.CreateTable(
                name: "AppUserChat",
                columns: table => new
                {
                    AppUser = table.Column<int>(type: "int", nullable: false),
                    UsersId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserChat", x => new { x.AppUser, x.UsersId });
                    table.ForeignKey(
                        name: "FK_AppUserChat_AspNetUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppUserChat_Chats_AppUser",
                        column: x => x.AppUser,
                        principalTable: "Chats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppUserChat_UsersId",
                table: "AppUserChat",
                column: "UsersId");
        }
    }
}
