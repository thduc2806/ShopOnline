using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Identity.Migrations
{
    public partial class addPhoneNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Users",
                type: "nvarchar(12)",
                maxLength: 12,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "CreatedBy", "CreatedDate", "PasswordHash", "TokenEffectiveDate" },
                values: new object[] { "65b6f4fd-7a5b-4de6-9fbb-d1f80fdf1da9", new Guid("3a30ade3-de7b-449f-b990-bcd66940fdd1"), new DateTime(2022, 11, 30, 17, 4, 29, 244, DateTimeKind.Local).AddTicks(3531), "AQAAAAEAACcQAAAAEJ6KCag7jnP4GJRxFZ6GLek/+3B9eT6Pu5Gm07xnxdUPkoFbQVIei6vxC2iwE1x82Q==", new DateTime(2022, 11, 30, 17, 4, 29, 238, DateTimeKind.Local).AddTicks(8461) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(12)",
                oldMaxLength: 12,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "CreatedBy", "CreatedDate", "PasswordHash", "TokenEffectiveDate" },
                values: new object[] { "fbc99968-a3b4-4303-9bce-5f1b2c9c3c39", new Guid("68d9a794-563c-4544-95ab-c8855441fc36"), new DateTime(2022, 11, 30, 16, 44, 47, 94, DateTimeKind.Local).AddTicks(6169), "AQAAAAEAACcQAAAAEOF9ZqKDGZtQnzViBVV3tX2EPNRsh+bcyBiyh8wfLUc++0sHYNOM7Cyew6TsZygWRQ==", new DateTime(2022, 11, 30, 16, 44, 47, 88, DateTimeKind.Local).AddTicks(5311) });
        }
    }
}
