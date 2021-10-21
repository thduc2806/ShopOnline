using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace oShopSolution.Data.Migrations
{
    public partial class ChangeNameComment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("7225da6b-65fc-4b04-8f46-fd3176512eff"),
                column: "ConcurrencyStamp",
                value: "3ffc3b67-0311-432b-9ac6-45788458fd64");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("d60a3a17-4053-42bb-a858-f44e7825bdf4"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "eaf8acf2-26c8-469d-92b7-f97bba1f2649", "AQAAAAEAACcQAAAAEPSQZhI7X99ARxRif1RaioJGA02Pt2yPrEP7kAdcr8+NKisEIVwxvdTe4DehGDOLCg==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2021, 10, 20, 2, 10, 46, 424, DateTimeKind.Local).AddTicks(4443));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2021, 10, 20, 2, 10, 46, 424, DateTimeKind.Local).AddTicks(9161));
        }
    }
}
