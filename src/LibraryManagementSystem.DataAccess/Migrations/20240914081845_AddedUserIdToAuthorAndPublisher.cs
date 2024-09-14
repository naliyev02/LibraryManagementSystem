using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagementSystem.DataAccess.Migrations
{
    public partial class AddedUserIdToAuthorAndPublisher : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Publishers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Authors",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Publishers_UserId",
                table: "Publishers",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Authors_UserId",
                table: "Authors",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_AspNetUsers_UserId",
                table: "Authors",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Publishers_AspNetUsers_UserId",
                table: "Publishers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Authors_AspNetUsers_UserId",
                table: "Authors");

            migrationBuilder.DropForeignKey(
                name: "FK_Publishers_AspNetUsers_UserId",
                table: "Publishers");

            migrationBuilder.DropIndex(
                name: "IX_Publishers_UserId",
                table: "Publishers");

            migrationBuilder.DropIndex(
                name: "IX_Authors_UserId",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Publishers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Authors");
        }
    }
}
