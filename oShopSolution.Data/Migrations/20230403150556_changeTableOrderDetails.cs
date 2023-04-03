using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace oShopSolution.Data.Migrations
{
    public partial class changeTableOrderDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("7225da6b-65fc-4b04-8f46-fd3176512eff"),
                column: "ConcurrencyStamp",
                value: "21866f30-e5ad-4ce7-90ea-f9d641ddbd7a");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("d60a3a17-4053-42bb-a858-f44e7825bdf4"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ae5ed668-eba1-4c04-9c97-6939616df7b7", "AQAAAAEAACcQAAAAEGYg/w+FpJIn57wnjC5QnQP1t2dkDLN8/NK6yNR753F0wnCHCSPo/LNSM7ZtHuCKrw==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2023, 4, 3, 22, 5, 55, 499, DateTimeKind.Local).AddTicks(3597));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2023, 4, 3, 22, 5, 55, 500, DateTimeKind.Local).AddTicks(2693));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("7225da6b-65fc-4b04-8f46-fd3176512eff"),
                column: "ConcurrencyStamp",
                value: "99e1f2f3-6d1d-4c4b-8880-a47be784c458");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("d60a3a17-4053-42bb-a858-f44e7825bdf4"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "c0884c3e-a61b-47b0-a0f8-62b7db8f7b0e", "AQAAAAEAACcQAAAAEDokgKMUjf/T6GEaZDo/LlQll6YL+fznG5HqyrMsv93LUxY9Q9bcOkwO6rVpQo1x3w==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2023, 4, 3, 21, 12, 7, 387, DateTimeKind.Local).AddTicks(4338));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2023, 4, 3, 21, 12, 7, 388, DateTimeKind.Local).AddTicks(2955));
        }
    }
}
