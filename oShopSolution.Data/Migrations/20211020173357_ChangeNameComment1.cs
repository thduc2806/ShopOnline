using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace oShopSolution.Data.Migrations
{
    public partial class ChangeNameComment1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("7225da6b-65fc-4b04-8f46-fd3176512eff"),
                column: "ConcurrencyStamp",
                value: "8b2c6505-7c16-4098-988a-80a1f0e24c2e");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("d60a3a17-4053-42bb-a858-f44e7825bdf4"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "c3f68c40-4c85-46fd-a09a-be7d9c9d54af", "AQAAAAEAACcQAAAAEOwhGb+k0r7rrYz/n99V63506ulWrZzVfQ2s0qz3jHPcHnLm60rqH3cIrwSOh+s6lQ==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2021, 10, 21, 0, 33, 57, 73, DateTimeKind.Local).AddTicks(8963));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2021, 10, 21, 0, 33, 57, 74, DateTimeKind.Local).AddTicks(4651));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("7225da6b-65fc-4b04-8f46-fd3176512eff"),
                column: "ConcurrencyStamp",
                value: "04fd2f6f-24b8-498d-9d94-6da798497338");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("d60a3a17-4053-42bb-a858-f44e7825bdf4"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "eb9c9e80-d92d-4c41-852a-85b05cdb8a44", "AQAAAAEAACcQAAAAEI/UTv7R4F6LRFUK9NsTUiHxQYlQQWWiyGDMSUQX8pTLaIRSdrf2ZDcQzycBOkhhvA==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2021, 10, 20, 16, 46, 2, 874, DateTimeKind.Local).AddTicks(293));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2021, 10, 20, 16, 46, 2, 875, DateTimeKind.Local).AddTicks(2412));
        }
    }
}
