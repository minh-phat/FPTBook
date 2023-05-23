using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FPTBookStore.Data.Migrations
{
    /// <inheritdoc />
    public partial class addingStock : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Available",
                table: "Book",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Available",
                table: "Book");
        }
    }
}
