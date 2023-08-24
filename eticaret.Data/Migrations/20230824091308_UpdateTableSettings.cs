using Microsoft.EntityFrameworkCore.Migrations;

namespace eticaret.Data.Migrations
{
    public partial class UpdateTableSettings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "footerDetail",
                schema: "EticaretSchemas",
                table: "Settings",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductBasket_productID",
                schema: "EticaretSchemas",
                table: "ProductBasket",
                column: "productID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductBasket_Products_productID",
                schema: "EticaretSchemas",
                table: "ProductBasket",
                column: "productID",
                principalSchema: "EticaretSchemas",
                principalTable: "Products",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductBasket_Products_productID",
                schema: "EticaretSchemas",
                table: "ProductBasket");

            migrationBuilder.DropIndex(
                name: "IX_ProductBasket_productID",
                schema: "EticaretSchemas",
                table: "ProductBasket");

            migrationBuilder.DropColumn(
                name: "footerDetail",
                schema: "EticaretSchemas",
                table: "Settings");
        }
    }
}
