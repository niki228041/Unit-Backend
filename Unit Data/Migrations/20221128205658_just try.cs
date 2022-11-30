using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Unit_Data.Migrations
{
    public partial class justtry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUserChat_Chats_ChatsId",
                table: "AppUserChat");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Chats_ChatId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_AspNetUsers_UserId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Orders_OrderId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Posts_UserId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "Products",
                newName: "Product");

            migrationBuilder.RenameIndex(
                name: "IX_Products_OrderId",
                table: "Products",
                newName: "IX_Products_Product");

            migrationBuilder.RenameColumn(
                name: "ChatId",
                table: "Messages",
                newName: "Message");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_ChatId",
                table: "Messages",
                newName: "IX_Messages_Message");

            migrationBuilder.RenameColumn(
                name: "ChatsId",
                table: "AppUserChat",
                newName: "AppUser");

            migrationBuilder.AddColumn<string>(
                name: "AppUser",
                table: "Posts",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_AppUser",
                table: "Posts",
                column: "AppUser");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserChat_Chats_AppUser",
                table: "AppUserChat",
                column: "AppUser",
                principalTable: "Chats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Chats_Message",
                table: "Messages",
                column: "Message",
                principalTable: "Chats",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_AspNetUsers_AppUser",
                table: "Posts",
                column: "AppUser",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Orders_Product",
                table: "Products",
                column: "Product",
                principalTable: "Orders",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUserChat_Chats_AppUser",
                table: "AppUserChat");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Chats_Message",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_AspNetUsers_AppUser",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Orders_Product",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Posts_AppUser",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "AppUser",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "Product",
                table: "Products",
                newName: "OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_Product",
                table: "Products",
                newName: "IX_Products_OrderId");

            migrationBuilder.RenameColumn(
                name: "Message",
                table: "Messages",
                newName: "ChatId");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_Message",
                table: "Messages",
                newName: "IX_Messages_ChatId");

            migrationBuilder.RenameColumn(
                name: "AppUser",
                table: "AppUserChat",
                newName: "ChatsId");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Posts",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_UserId",
                table: "Posts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserChat_Chats_ChatsId",
                table: "AppUserChat",
                column: "ChatsId",
                principalTable: "Chats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Chats_ChatId",
                table: "Messages",
                column: "ChatId",
                principalTable: "Chats",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_AspNetUsers_UserId",
                table: "Posts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Orders_OrderId",
                table: "Products",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }
    }
}
