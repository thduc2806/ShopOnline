using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace oShopSolution.Data.Migrations
{
    public partial class updateComment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    TextComment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rating = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comment_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("7225da6b-65fc-4b04-8f46-fd3176512eff"),
                column: "ConcurrencyStamp",
                value: "6919be14-a209-419e-84e3-6a040e78f821");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("d60a3a17-4053-42bb-a858-f44e7825bdf4"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d5aa953a-3aee-4777-8504-4ccc3cd8527d", "AQAAAAEAACcQAAAAEH4kn8gJ6sHTRJ22gZt9NxfcCIO8nm7PYwylfLjHVUc194PCuC6s7f3JNNdvst1pWA==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "Description", "Name" },
                values: new object[] { new DateTime(2021, 10, 20, 0, 34, 3, 135, DateTimeKind.Local).AddTicks(4370), "This is Iphone 12 Promax", "Iphone 12 Promax" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2021, 10, 20, 0, 34, 3, 135, DateTimeKind.Local).AddTicks(9283));

            migrationBuilder.CreateIndex(
                name: "IX_Comment_ProductId",
                table: "Comment",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("7225da6b-65fc-4b04-8f46-fd3176512eff"),
                column: "ConcurrencyStamp",
                value: "7d777b4f-6121-4c0b-88c8-13bcd0ce4e1e");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("d60a3a17-4053-42bb-a858-f44e7825bdf4"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "3da02329-b914-4eb6-9597-6e6b4ab2934b", "AQAAAAEAACcQAAAAEKMlCWB0dUkPsrwvT+aHMW8MX0BSw7d5iO4orD7BVB/agJuEqLOLQSz5AB6Y7t+nbg==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "Description", "Name" },
                values: new object[] { new DateTime(2021, 10, 17, 17, 6, 32, 115, DateTimeKind.Local).AddTicks(4304), "This is Iphone 11 Promax", "Iphone 11 Promax" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2021, 10, 17, 17, 6, 32, 115, DateTimeKind.Local).AddTicks(9267));
        }
    }
}
