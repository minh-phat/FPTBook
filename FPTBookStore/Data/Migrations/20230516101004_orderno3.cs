using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FPTBookStore.Data.Migrations
{
    /// <inheritdoc />
    public partial class orderno3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Total",
                table: "OrderDetails",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Total",
                table: "OrderDetails",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
