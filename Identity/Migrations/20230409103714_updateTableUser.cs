using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Identity.Migrations
{
    public partial class updateTableUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Ward",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "CreatedBy", "CreatedDate", "FullName", "PasswordHash", "TokenEffectiveDate", "Ward" },
                values: new object[] { "a4fc777f-3f43-4e65-becc-fada414d3c16", new Guid("9e286e08-0309-4337-bb66-c5cac7bb8fae"), new DateTime(2023, 4, 9, 17, 37, 13, 632, DateTimeKind.Local).AddTicks(5625), "admin", "AQAAAAEAACcQAAAAEHi/yyizHW44JTE4eLaRx4GeTwDJJgTQqRl+XvG9+XV657aNSL8jDkmUTk8RCBB3vQ==", new DateTime(2023, 4, 9, 17, 37, 13, 626, DateTimeKind.Local).AddTicks(245), "Phường 13" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Ward",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "CreatedBy", "CreatedDate", "PasswordHash", "TokenEffectiveDate" },
                values: new object[] { "7ba419c5-14a9-4a12-a6e7-91006753e8d2", new Guid("de4fd7e2-de6c-4dc4-9f66-681a135d9a28"), new DateTime(2023, 3, 13, 18, 54, 48, 15, DateTimeKind.Local).AddTicks(4737), "AQAAAAEAACcQAAAAED/1qopLIrHQ5AZgDRBpDs0nzuY1DsTh+rKHqE2n91TrT7l3WyMEURqeRKHCegoMcg==", new DateTime(2023, 3, 13, 18, 54, 48, 9, DateTimeKind.Local).AddTicks(5047) });
        }
    }
}
