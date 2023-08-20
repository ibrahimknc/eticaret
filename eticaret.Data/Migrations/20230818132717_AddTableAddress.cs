using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eticaret.Data.Migrations
{
    public partial class AddTableAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserAddress",
                schema: "EticaretSchemas",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    userID = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    firstName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    lastName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    country = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    address = table.Column<string>(type: "text", nullable: true),
                    city = table.Column<string>(type: "text", nullable: true),
                    isActive = table.Column<bool>(type: "boolean", nullable: false),
                    creatingTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updatedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAddress", x => x.id);
                    table.ForeignKey(
                        name: "FK_UserAddress_Users_userID",
                        column: x => x.userID,
                        principalSchema: "EticaretSchemas",
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserAddress_userID",
                schema: "EticaretSchemas",
                table: "UserAddress",
                column: "userID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserAddress",
                schema: "EticaretSchemas");
        }
    }
}
