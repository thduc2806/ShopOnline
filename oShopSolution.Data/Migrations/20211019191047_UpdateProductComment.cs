using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace oShopSolution.Data.Migrations
{
    public partial class UpdateProductComment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                column: "CreateDate",
                value: new DateTime(2021, 10, 20, 0, 34, 3, 135, DateTimeKind.Local).AddTicks(4370));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2021, 10, 20, 0, 34, 3, 135, DateTimeKind.Local).AddTicks(9283));
        }
    }
}
