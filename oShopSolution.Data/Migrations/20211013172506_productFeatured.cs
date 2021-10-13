using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace oShopSolution.Data.Migrations
{
    public partial class productFeatured : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Featured",
                table: "Products",
                type: "bit",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("7225da6b-65fc-4b04-8f46-fd3176512eff"),
                column: "ConcurrencyStamp",
                value: "1d25f57b-ccb7-41db-bf9d-adc762ba5e84");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("d60a3a17-4053-42bb-a858-f44e7825bdf4"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d8d9b837-0f07-4253-9585-b90a07fc31b3", "AQAAAAEAACcQAAAAEJGGuE9UAAw6oCboRbo/BY1vMnIJJwjjDs4s6WLk+/r+hs/jN+79oPMnaFYfeI+C1g==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "Featured" },
                values: new object[] { new DateTime(2021, 10, 14, 0, 25, 5, 633, DateTimeKind.Local).AddTicks(6110), true });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreateDate", "Featured" },
                values: new object[] { new DateTime(2021, 10, 14, 0, 25, 5, 634, DateTimeKind.Local).AddTicks(1414), true });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Featured",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("7225da6b-65fc-4b04-8f46-fd3176512eff"),
                column: "ConcurrencyStamp",
                value: "7129de31-4ead-436d-9d25-84c94dc6c917");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("d60a3a17-4053-42bb-a858-f44e7825bdf4"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e2df84e8-9b6d-48ce-b3fd-0b3e385669d9", "AQAAAAEAACcQAAAAEBnqOObWgUJHNu1OMDHXGB7U8DJeRIcNgpoVlMkgQo0vDiNNeqLvn3g8Cn7QfGgWbw==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2021, 10, 12, 15, 57, 40, 386, DateTimeKind.Local).AddTicks(5214));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2021, 10, 12, 15, 57, 40, 386, DateTimeKind.Local).AddTicks(9429));
        }
    }
}
