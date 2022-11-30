using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Unit_Data.Migrations
{
    public partial class idkdouworkornot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AppUserContacts_AppUserContactId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "AppUserAppUserContact");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_AppUserContactId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AppUserContactId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "AppUser",
                table: "AppUserContacts",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ContactId",
                table: "AppUserContacts",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "ContactId",
                table: "AppUserContacts");

            migrationBuilder.AddColumn<int>(
                name: "AppUserContactId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AppUserAppUserContact",
                columns: table => new
                {
                    AppUserAppUserContactId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContactId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserAppUserContact", x => x.AppUserAppUserContactId);
                    table.ForeignKey(
                        name: "FK_AppUserAppUserContact_AppUserContacts_ContactId",
                        column: x => x.ContactId,
                        principalTable: "AppUserContacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppUserAppUserContact_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AppUserContactId",
                table: "AspNetUsers",
                column: "AppUserContactId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUserAppUserContact_ContactId",
                table: "AppUserAppUserContact",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUserAppUserContact_UserId",
                table: "AppUserAppUserContact",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AppUserContacts_AppUserContactId",
                table: "AspNetUsers",
                column: "AppUserContactId",
                principalTable: "AppUserContacts",
                principalColumn: "Id");
        }
    }
}
