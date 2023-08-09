using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eticaret.Data.Migrations
{
    public partial class addTableProducCheckout : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "shippingAmount",
                schema: "EticaretSchemas",
                table: "Products",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProductCheckout",
                schema: "EticaretSchemas",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    productID = table.Column<Guid>(type: "uuid", nullable: false),
                    userID = table.Column<Guid>(type: "uuid", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    shippingAmount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: true),
                    isPayment = table.Column<bool>(type: "boolean", nullable: true),
                    isActive = table.Column<bool>(type: "boolean", nullable: false),
                    creatingTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updatedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCheckout", x => x.id);
                    table.ForeignKey(
                        name: "FK_ProductCheckout_Products_productID",
                        column: x => x.productID,
                        principalSchema: "EticaretSchemas",
                        principalTable: "Products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductCheckout_Users_userID",
                        column: x => x.userID,
                        principalSchema: "EticaretSchemas",
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductCheckout_productID",
                schema: "EticaretSchemas",
                table: "ProductCheckout",
                column: "productID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCheckout_userID",
                schema: "EticaretSchemas",
                table: "ProductCheckout",
                column: "userID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductCheckout",
                schema: "EticaretSchemas");

            migrationBuilder.DropColumn(
                name: "shippingAmount",
                schema: "EticaretSchemas",
                table: "Products");
        }
    }
}
