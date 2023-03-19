using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WM.Core.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMenu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_Product_ProductId",
                table: "Inventory");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_Warehouse_WarehouseId",
                table: "Inventory");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Facility_FacilityId",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_Warehouse_Facility_FacilityId",
                table: "Warehouse");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Warehouse",
                table: "Warehouse");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Product",
                table: "Product");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Inventory",
                table: "Inventory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Facility",
                table: "Facility");

            migrationBuilder.RenameTable(
                name: "Warehouse",
                newName: "Warehouses");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "Product",
                newName: "Products");

            migrationBuilder.RenameTable(
                name: "Inventory",
                newName: "Inventories");

            migrationBuilder.RenameTable(
                name: "Facility",
                newName: "Facilities");

            migrationBuilder.RenameIndex(
                name: "IX_Warehouse_FacilityId",
                table: "Warehouses",
                newName: "IX_Warehouses_FacilityId");

            migrationBuilder.RenameIndex(
                name: "IX_User_FacilityId",
                table: "Users",
                newName: "IX_Users_FacilityId");

            migrationBuilder.RenameIndex(
                name: "IX_Inventory_WarehouseId",
                table: "Inventories",
                newName: "IX_Inventories_WarehouseId");

            migrationBuilder.RenameIndex(
                name: "IX_Inventory_ProductId",
                table: "Inventories",
                newName: "IX_Inventories_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Warehouses",
                table: "Warehouses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Inventories",
                table: "Inventories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Facilities",
                table: "Facilities",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "MenuItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItems", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Inventory" },
                    { 2, "Orders" },
                    { 3, "Products" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Inventories_Products_ProductId",
                table: "Inventories",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventories_Warehouses_WarehouseId",
                table: "Inventories",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Facilities_FacilityId",
                table: "Users",
                column: "FacilityId",
                principalTable: "Facilities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Warehouses_Facilities_FacilityId",
                table: "Warehouses",
                column: "FacilityId",
                principalTable: "Facilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventories_Products_ProductId",
                table: "Inventories");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventories_Warehouses_WarehouseId",
                table: "Inventories");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Facilities_FacilityId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Warehouses_Facilities_FacilityId",
                table: "Warehouses");

            migrationBuilder.DropTable(
                name: "MenuItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Warehouses",
                table: "Warehouses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Inventories",
                table: "Inventories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Facilities",
                table: "Facilities");

            migrationBuilder.RenameTable(
                name: "Warehouses",
                newName: "Warehouse");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "Product");

            migrationBuilder.RenameTable(
                name: "Inventories",
                newName: "Inventory");

            migrationBuilder.RenameTable(
                name: "Facilities",
                newName: "Facility");

            migrationBuilder.RenameIndex(
                name: "IX_Warehouses_FacilityId",
                table: "Warehouse",
                newName: "IX_Warehouse_FacilityId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_FacilityId",
                table: "User",
                newName: "IX_User_FacilityId");

            migrationBuilder.RenameIndex(
                name: "IX_Inventories_WarehouseId",
                table: "Inventory",
                newName: "IX_Inventory_WarehouseId");

            migrationBuilder.RenameIndex(
                name: "IX_Inventories_ProductId",
                table: "Inventory",
                newName: "IX_Inventory_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Warehouse",
                table: "Warehouse",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Product",
                table: "Product",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Inventory",
                table: "Inventory",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Facility",
                table: "Facility",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_Product_ProductId",
                table: "Inventory",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_Warehouse_WarehouseId",
                table: "Inventory",
                column: "WarehouseId",
                principalTable: "Warehouse",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Facility_FacilityId",
                table: "User",
                column: "FacilityId",
                principalTable: "Facility",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Warehouse_Facility_FacilityId",
                table: "Warehouse",
                column: "FacilityId",
                principalTable: "Facility",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
