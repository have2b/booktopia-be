using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObject.Migrations
{
    public partial class EntityModification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_order_details_products_ProductId",
                table: "order_details");

            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_order_details",
                table: "order_details");

            migrationBuilder.DropIndex(
                name: "IX_order_details_ProductId",
                table: "order_details");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "d9b0a6e5-416d-4d72-9c46-81eb105ada11", "34cc0c06-f9e1-4d99-93e6-f7e5562893fb" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "f05dcf30-b7d9-46a9-94c3-dcd8a6a7035e", "34cc0c06-f9e1-4d99-93e6-f7e5562893fb" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d9b0a6e5-416d-4d72-9c46-81eb105ada11");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f05dcf30-b7d9-46a9-94c3-dcd8a6a7035e");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "34cc0c06-f9e1-4d99-93e6-f7e5562893fb");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "order_details");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "order_details",
                newName: "Discount");

            migrationBuilder.AddColumn<int>(
                name: "BookId",
                table: "order_details",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_order_details",
                table: "order_details",
                columns: new[] { "OrderId", "BookId" });

            migrationBuilder.CreateTable(
                name: "books",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Author = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CostPrice = table.Column<decimal>(type: "decimal(4,2)", precision: 4, scale: 2, nullable: false),
                    SellPrice = table.Column<decimal>(type: "decimal(4,2)", precision: 4, scale: 2, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PublisherId = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_books", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_books_categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0e0d20cc-ecc5-4326-bc73-2acc2c15c7de", "a0f09f32-57ee-4b39-b49b-2245e3fa01dd", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "207a4154-2f5d-456c-977c-db9400792c85", "4f7967aa-95f9-43ae-80f0-69f72f5f457b", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "CreatedAt", "Email", "EmailConfirmed", "IsActive", "LastModifiedAt", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "eb17aafc-9c51-486b-91d4-68a0388d6e25", 0, "", "554fb3e0-7ebb-4459-92fc-ec1d62e7abef", new DateTime(2024, 2, 25, 20, 59, 33, 119, DateTimeKind.Local).AddTicks(6387), null, false, true, new DateTime(2024, 2, 25, 20, 59, 33, 119, DateTimeKind.Local).AddTicks(6397), false, null, "PrincipalAdmin", null, "ADMIN", "AQAAAAEAACcQAAAAEJt0KPn6ECdSxmR9s4NIpbOsPTYFob3tGGF8ZQaxpztjm2iIT/jRK3FCh9EhVzfz5g==", null, false, "8374df5f-8a6c-4109-b514-f7f93f9ef1ac", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "0e0d20cc-ecc5-4326-bc73-2acc2c15c7de", "eb17aafc-9c51-486b-91d4-68a0388d6e25" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "207a4154-2f5d-456c-977c-db9400792c85", "eb17aafc-9c51-486b-91d4-68a0388d6e25" });

            migrationBuilder.CreateIndex(
                name: "IX_order_details_BookId",
                table: "order_details",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_books_CategoryId",
                table: "books",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_order_details_books_BookId",
                table: "order_details",
                column: "BookId",
                principalTable: "books",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_order_details_books_BookId",
                table: "order_details");

            migrationBuilder.DropTable(
                name: "books");

            migrationBuilder.DropPrimaryKey(
                name: "PK_order_details",
                table: "order_details");

            migrationBuilder.DropIndex(
                name: "IX_order_details_BookId",
                table: "order_details");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "0e0d20cc-ecc5-4326-bc73-2acc2c15c7de", "eb17aafc-9c51-486b-91d4-68a0388d6e25" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "207a4154-2f5d-456c-977c-db9400792c85", "eb17aafc-9c51-486b-91d4-68a0388d6e25" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0e0d20cc-ecc5-4326-bc73-2acc2c15c7de");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "207a4154-2f5d-456c-977c-db9400792c85");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "eb17aafc-9c51-486b-91d4-68a0388d6e25");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "order_details");

            migrationBuilder.RenameColumn(
                name: "Discount",
                table: "order_details",
                newName: "ProductId");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "orders",
                type: "decimal(4,2)",
                precision: 4,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "order_details",
                type: "decimal(4,2)",
                precision: 4,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddPrimaryKey(
                name: "PK_order_details",
                table: "order_details",
                columns: new[] { "OrderId", "ProductId" });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(4,2)", precision: 4, scale: 2, nullable: false),
                    PublisherId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_products_categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d9b0a6e5-416d-4d72-9c46-81eb105ada11", "f8123d15-0952-405d-b86c-49746cb60e63", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f05dcf30-b7d9-46a9-94c3-dcd8a6a7035e", "0040691b-ddc9-4228-b670-dd5bd25b08f9", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "CreatedAt", "Email", "EmailConfirmed", "IsActive", "LastModifiedAt", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "34cc0c06-f9e1-4d99-93e6-f7e5562893fb", 0, "", "16e8fd0e-e004-4f41-9e6f-469daac4c368", new DateTime(2024, 2, 25, 20, 26, 4, 114, DateTimeKind.Local).AddTicks(7084), null, false, true, new DateTime(2024, 2, 25, 20, 26, 4, 114, DateTimeKind.Local).AddTicks(7091), false, null, "PrincipalAdmin", null, "ADMIN", "AQAAAAEAACcQAAAAEE5NWgiQevtiITizl1VoAeWcDvzX1rrRg6oPAXUMiq3SWenNX/RnjhUXF37jGZz3og==", null, false, "6b97335f-1af4-439c-8c2d-8c690487bdc4", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "d9b0a6e5-416d-4d72-9c46-81eb105ada11", "34cc0c06-f9e1-4d99-93e6-f7e5562893fb" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "f05dcf30-b7d9-46a9-94c3-dcd8a6a7035e", "34cc0c06-f9e1-4d99-93e6-f7e5562893fb" });

            migrationBuilder.CreateIndex(
                name: "IX_order_details_ProductId",
                table: "order_details",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_products_CategoryId",
                table: "products",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_order_details_products_ProductId",
                table: "order_details",
                column: "ProductId",
                principalTable: "products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
