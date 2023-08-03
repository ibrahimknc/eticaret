using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eticaret.Data.Migrations
{
    public partial class AddedNewTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Comments",
                schema: "EticaretSchemas",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    productID = table.Column<Guid>(type: "uuid", nullable: false),
                    userID = table.Column<Guid>(type: "uuid", nullable: false),
                    detail = table.Column<string>(type: "text", nullable: false),
                    isActive = table.Column<bool>(type: "boolean", nullable: false),
                    creatingTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updatedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.id);
                    table.ForeignKey(
                        name: "FK_Comments_Products_productID",
                        column: x => x.productID,
                        principalSchema: "EticaretSchemas",
                        principalTable: "Products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Users_userID",
                        column: x => x.userID,
                        principalSchema: "EticaretSchemas",
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_productID",
                schema: "EticaretSchemas",
                table: "Comments",
                column: "productID");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_userID",
                schema: "EticaretSchemas",
                table: "Comments",
                column: "userID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments",
                schema: "EticaretSchemas");
        }
    }
}
