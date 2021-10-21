using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace oShopSolution.Data.Migrations
{
    public partial class updateProductIdinProductComment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Products_ProductId",
                table: "Comment");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "Comment",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("7225da6b-65fc-4b04-8f46-fd3176512eff"),
                column: "ConcurrencyStamp",
                value: "5dc8c871-07d2-4bf2-a364-84348e52a6d9");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("d60a3a17-4053-42bb-a858-f44e7825bdf4"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "353a3013-648d-4f30-a344-8305f198f854", "AQAAAAEAACcQAAAAEPFQvNE0PLii6J/JKlwz4pVDJpOh5rdAGtzh32AzEK4iVUhjV8sRHkbFZt3DjfQmSQ==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2021, 10, 21, 18, 2, 6, 292, DateTimeKind.Local).AddTicks(3625));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2021, 10, 21, 18, 2, 6, 292, DateTimeKind.Local).AddTicks(8120));

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Products_ProductId",
                table: "Comment",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Products_ProductId",
                table: "Comment");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "Comment",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Products_ProductId",
                table: "Comment",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
