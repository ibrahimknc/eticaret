using Microsoft.EntityFrameworkCore.Migrations;

namespace eticaret.Data.Migrations
{
    public partial class AddedNewTableTwo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "rating",
                schema: "EticaretSchemas",
                table: "Comments",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "rating",
                schema: "EticaretSchemas",
                table: "Comments");
        }
    }
}
