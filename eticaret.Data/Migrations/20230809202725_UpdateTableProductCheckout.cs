using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eticaret.Data.Migrations
{
    public partial class UpdateTableProductCheckout : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductCheckout_Products_productID",
                schema: "EticaretSchemas",
                table: "ProductCheckout");

            migrationBuilder.DropIndex(
                name: "IX_ProductCheckout_productID",
                schema: "EticaretSchemas",
                table: "ProductCheckout");

            migrationBuilder.DropColumn(
                name: "productID",
                schema: "EticaretSchemas",
                table: "ProductCheckout");

            migrationBuilder.RenameColumn(
                name: "shippingAmount",
                schema: "EticaretSchemas",
                table: "ProductCheckout",
                newName: "totalPayment");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductCheckoutid",
                schema: "EticaretSchemas",
                table: "Products",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "billingAddress",
                schema: "EticaretSchemas",
                table: "ProductCheckout",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "billingCity",
                schema: "EticaretSchemas",
                table: "ProductCheckout",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "billingCompanyName",
                schema: "EticaretSchemas",
                table: "ProductCheckout",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "billingCountry",
                schema: "EticaretSchemas",
                table: "ProductCheckout",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "billingFirstName",
                schema: "EticaretSchemas",
                table: "ProductCheckout",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "billingLastName",
                schema: "EticaretSchemas",
                table: "ProductCheckout",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "shippingAddress",
                schema: "EticaretSchemas",
                table: "ProductCheckout",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "shippingCity",
                schema: "EticaretSchemas",
                table: "ProductCheckout",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "shippingCompanyName",
                schema: "EticaretSchemas",
                table: "ProductCheckout",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "shippingCountry",
                schema: "EticaretSchemas",
                table: "ProductCheckout",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "shippingFirstName",
                schema: "EticaretSchemas",
                table: "ProductCheckout",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "shippingLastName",
                schema: "EticaretSchemas",
                table: "ProductCheckout",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "status",
                schema: "EticaretSchemas",
                table: "ProductCheckout",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "totalshippingAmount",
                schema: "EticaretSchemas",
                table: "ProductCheckout",
                type: "numeric",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductCheckoutid",
                schema: "EticaretSchemas",
                table: "Products",
                column: "ProductCheckoutid");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductCheckout_ProductCheckoutid",
                schema: "EticaretSchemas",
                table: "Products",
                column: "ProductCheckoutid",
                principalSchema: "EticaretSchemas",
                principalTable: "ProductCheckout",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductCheckout_ProductCheckoutid",
                schema: "EticaretSchemas",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductCheckoutid",
                schema: "EticaretSchemas",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductCheckoutid",
                schema: "EticaretSchemas",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "billingAddress",
                schema: "EticaretSchemas",
                table: "ProductCheckout");

            migrationBuilder.DropColumn(
                name: "billingCity",
                schema: "EticaretSchemas",
                table: "ProductCheckout");

            migrationBuilder.DropColumn(
                name: "billingCompanyName",
                schema: "EticaretSchemas",
                table: "ProductCheckout");

            migrationBuilder.DropColumn(
                name: "billingCountry",
                schema: "EticaretSchemas",
                table: "ProductCheckout");

            migrationBuilder.DropColumn(
                name: "billingFirstName",
                schema: "EticaretSchemas",
                table: "ProductCheckout");

            migrationBuilder.DropColumn(
                name: "billingLastName",
                schema: "EticaretSchemas",
                table: "ProductCheckout");

            migrationBuilder.DropColumn(
                name: "shippingAddress",
                schema: "EticaretSchemas",
                table: "ProductCheckout");

            migrationBuilder.DropColumn(
                name: "shippingCity",
                schema: "EticaretSchemas",
                table: "ProductCheckout");

            migrationBuilder.DropColumn(
                name: "shippingCompanyName",
                schema: "EticaretSchemas",
                table: "ProductCheckout");

            migrationBuilder.DropColumn(
                name: "shippingCountry",
                schema: "EticaretSchemas",
                table: "ProductCheckout");

            migrationBuilder.DropColumn(
                name: "shippingFirstName",
                schema: "EticaretSchemas",
                table: "ProductCheckout");

            migrationBuilder.DropColumn(
                name: "shippingLastName",
                schema: "EticaretSchemas",
                table: "ProductCheckout");

            migrationBuilder.DropColumn(
                name: "status",
                schema: "EticaretSchemas",
                table: "ProductCheckout");

            migrationBuilder.DropColumn(
                name: "totalshippingAmount",
                schema: "EticaretSchemas",
                table: "ProductCheckout");

            migrationBuilder.RenameColumn(
                name: "totalPayment",
                schema: "EticaretSchemas",
                table: "ProductCheckout",
                newName: "shippingAmount");

            migrationBuilder.AddColumn<Guid>(
                name: "productID",
                schema: "EticaretSchemas",
                table: "ProductCheckout",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ProductCheckout_productID",
                schema: "EticaretSchemas",
                table: "ProductCheckout",
                column: "productID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCheckout_Products_productID",
                schema: "EticaretSchemas",
                table: "ProductCheckout",
                column: "productID",
                principalSchema: "EticaretSchemas",
                principalTable: "Products",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
