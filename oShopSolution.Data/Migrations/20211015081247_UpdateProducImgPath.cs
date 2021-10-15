using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace oShopSolution.Data.Migrations
{
    public partial class UpdateProducImgPath : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ThumbPath",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("7225da6b-65fc-4b04-8f46-fd3176512eff"),
                column: "ConcurrencyStamp",
                value: "1c5651db-5918-4fcc-9077-485da31b9c39");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("d60a3a17-4053-42bb-a858-f44e7825bdf4"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "6a861b84-ac67-4ff6-82c7-1201eb4ddfd2", "AQAAAAEAACcQAAAAECc0oL/xPjoTafEzcvOb8/rxyQGaTMUMJJybKQ0DEnDbjol/wpEGd22K4HxX3zPEXw==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "ThumbPath" },
                values: new object[] { new DateTime(2021, 10, 15, 15, 12, 46, 367, DateTimeKind.Local).AddTicks(8918), "https://www.imagevenue.com/ME13YCXU][IMG]https://cdn-thumbs.imagevenue.com/b5/a4/b5/ME13YCXU_t.png" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2021, 10, 15, 15, 12, 46, 368, DateTimeKind.Local).AddTicks(3835));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThumbPath",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("7225da6b-65fc-4b04-8f46-fd3176512eff"),
                column: "ConcurrencyStamp",
                value: "443ba250-360d-4f5e-b97d-63bc98ae4382");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("d60a3a17-4053-42bb-a858-f44e7825bdf4"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "a18f9aa7-5591-405b-9e2c-b4bd2cba589c", "AQAAAAEAACcQAAAAEFlGG4IdfLh9LcX7Uo0vz8NttE8wgZGOT4Dr27hJMjrrO7X/gudbPyki5xdKbYGPVA==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2021, 10, 15, 3, 42, 31, 411, DateTimeKind.Local).AddTicks(5643));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2021, 10, 15, 3, 42, 31, 412, DateTimeKind.Local).AddTicks(471));
        }
    }
}
