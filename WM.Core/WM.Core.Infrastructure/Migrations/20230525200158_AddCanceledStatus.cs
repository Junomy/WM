using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WM.Core.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCanceledStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "OrderStatuses",
                columns: new[] { "Id", "Name" },
                values: new object[] { 4, "Canceled" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
