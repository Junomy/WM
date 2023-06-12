using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WM.Core.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDashboard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "Id", "Link", "Name" },
                values: new object[] { 6, "/dashboard", "Dashboard" });

            migrationBuilder.UpdateData(
                table: "MenuRoles",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "MenuId", "RoleId" },
                values: new object[] { 1, 2 });

            migrationBuilder.UpdateData(
                table: "MenuRoles",
                keyColumn: "Id",
                keyValue: 5,
                column: "MenuId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "MenuRoles",
                keyColumn: "Id",
                keyValue: 6,
                column: "MenuId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "MenuRoles",
                keyColumn: "Id",
                keyValue: 7,
                column: "MenuId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "MenuRoles",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "MenuId", "RoleId" },
                values: new object[] { 1, 3 });

            migrationBuilder.UpdateData(
                table: "MenuRoles",
                keyColumn: "Id",
                keyValue: 9,
                column: "MenuId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "MenuRoles",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "MenuId", "RoleId" },
                values: new object[] { 5, 1 });

            migrationBuilder.UpdateData(
                table: "MenuRoles",
                keyColumn: "Id",
                keyValue: 11,
                column: "MenuId",
                value: 6);

            migrationBuilder.InsertData(
                table: "MenuRoles",
                columns: new[] { "Id", "MenuId", "RoleId" },
                values: new object[,]
                {
                    { 12, 6, 2 },
                    { 13, 6, 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MenuRoles",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "MenuRoles",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.UpdateData(
                table: "MenuRoles",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "MenuId", "RoleId" },
                values: new object[] { 4, 1 });

            migrationBuilder.UpdateData(
                table: "MenuRoles",
                keyColumn: "Id",
                keyValue: 5,
                column: "MenuId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "MenuRoles",
                keyColumn: "Id",
                keyValue: 6,
                column: "MenuId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "MenuRoles",
                keyColumn: "Id",
                keyValue: 7,
                column: "MenuId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "MenuRoles",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "MenuId", "RoleId" },
                values: new object[] { 4, 2 });

            migrationBuilder.UpdateData(
                table: "MenuRoles",
                keyColumn: "Id",
                keyValue: 9,
                column: "MenuId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "MenuRoles",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "MenuId", "RoleId" },
                values: new object[] { 3, 3 });

            migrationBuilder.UpdateData(
                table: "MenuRoles",
                keyColumn: "Id",
                keyValue: 11,
                column: "MenuId",
                value: 5);
        }
    }
}
