using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WM.Core.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddWarehouseLink : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "Id", "Link", "Name" },
                values: new object[] { 4, "/warehouses", "Warehouses" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
