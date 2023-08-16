using Microsoft.EntityFrameworkCore.Migrations;

namespace eticaret.Data.Migrations
{
    public partial class UpdateProductCheckoutTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "token",
                schema: "EticaretSchemas",
                table: "ProductCheckout",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "token",
                schema: "EticaretSchemas",
                table: "ProductCheckout");
        }
    }
}
