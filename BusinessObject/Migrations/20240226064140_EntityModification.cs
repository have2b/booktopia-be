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
                    BookId = table.Column<int>(type: "int", nullable: false)
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
                    table.PrimaryKey("PK_books", x => x.BookId);
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
                values: new object[] { "0bd62814-ca94-4743-8677-579fd9c6466d", "e3d151fd-d0f9-43c8-9768-0f8f46c4c9b0", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6cb19d72-1182-49b1-87f2-171ff0a50bad", "9d396de3-4185-49a7-8934-e6098ab9736c", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "CreatedAt", "Email", "EmailConfirmed", "IsActive", "LastModifiedAt", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "23f2dc71-60c7-41bb-9a92-41ef8d6a4c61", 0, "", "9937ada5-55af-4c27-b095-920a226b7c33", new DateTime(2024, 2, 26, 13, 41, 40, 247, DateTimeKind.Local).AddTicks(3455), null, false, true, new DateTime(2024, 2, 26, 13, 41, 40, 247, DateTimeKind.Local).AddTicks(3462), false, null, "PrincipalAdmin", null, "ADMIN", "AQAAAAEAACcQAAAAEKczx8h6OKsIjEmqRXkIujpa0Fp7ZgrAcsa2mlPluerq9UcHpm/8+HuBs2kJBaVFHw==", null, false, "18ef3d26-da3f-4819-a272-602227df4ae3", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "0bd62814-ca94-4743-8677-579fd9c6466d", "23f2dc71-60c7-41bb-9a92-41ef8d6a4c61" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "6cb19d72-1182-49b1-87f2-171ff0a50bad", "23f2dc71-60c7-41bb-9a92-41ef8d6a4c61" });

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
                principalColumn: "BookId",
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
                keyValues: new object[] { "0bd62814-ca94-4743-8677-579fd9c6466d", "23f2dc71-60c7-41bb-9a92-41ef8d6a4c61" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "6cb19d72-1182-49b1-87f2-171ff0a50bad", "23f2dc71-60c7-41bb-9a92-41ef8d6a4c61" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0bd62814-ca94-4743-8677-579fd9c6466d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6cb19d72-1182-49b1-87f2-171ff0a50bad");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "23f2dc71-60c7-41bb-9a92-41ef8d6a4c61");

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
