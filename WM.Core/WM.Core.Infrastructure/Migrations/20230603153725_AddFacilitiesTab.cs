using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WM.Core.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFacilitiesTab : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "Id", "Link", "Name" },
                values: new object[] { 5, "/facilities", "Facilities" });

            migrationBuilder.InsertData(
                table: "MenuRoles",
                columns: new[] { "Id", "MenuId", "RoleId" },
                values: new object[] { 11, 5, 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MenuRoles",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
