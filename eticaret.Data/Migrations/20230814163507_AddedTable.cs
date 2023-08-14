using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eticaret.Data.Migrations
{
    public partial class AddedTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductBasket",
                schema: "EticaretSchemas",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    ProductCheckoutID = table.Column<Guid>(type: "uuid", nullable: false),
                    productID = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    image = table.Column<string>(type: "text", nullable: true),
                    price = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    stock = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: true),
                    shippingAmount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: true),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    ProductCheckoutid = table.Column<Guid>(type: "uuid", nullable: true),
                    isActive = table.Column<bool>(type: "boolean", nullable: false),
                    creatingTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updatedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductBasket", x => x.id);
                    table.ForeignKey(
                        name: "FK_ProductBasket_ProductCheckout_ProductCheckoutID",
                        column: x => x.ProductCheckoutID,
                        principalSchema: "EticaretSchemas",
                        principalTable: "ProductCheckout",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductBasket_ProductCheckout_ProductCheckoutid",
                        column: x => x.ProductCheckoutid,
                        principalSchema: "EticaretSchemas",
                        principalTable: "ProductCheckout",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductBasket_ProductCheckoutID",
                schema: "EticaretSchemas",
                table: "ProductBasket",
                column: "ProductCheckoutID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductBasket_ProductCheckoutid",
                schema: "EticaretSchemas",
                table: "ProductBasket",
                column: "ProductCheckoutid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductBasket",
                schema: "EticaretSchemas");
        }
    }
}
