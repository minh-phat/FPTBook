using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FPTBookStore.Data.Migrations
{
    /// <inheritdoc />
    public partial class orderno2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_AspNetUsers_ApplicationUser",
                table: "OrderDetails");

            migrationBuilder.DropIndex(
                name: "IX_Order_ApplicationUser",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "ApplicationUser",
                table: "OrderDetails");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "OrderDetails",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Order_UserId",
                table: "OrderDetails",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_AspNetUsers_UserId",
                table: "OrderDetails",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_AspNetUsers_UserId",
                table: "OrderDetails");

            migrationBuilder.DropIndex(
                name: "IX_Order_UserId",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "OrderDetails");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUser",
                table: "OrderDetails",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Order_ApplicationUser",
                table: "OrderDetails",
                column: "ApplicationUser");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_AspNetUsers_ApplicationUser",
                table: "OrderDetails",
                column: "ApplicationUser",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
