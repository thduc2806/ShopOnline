using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace oShopSolution.Data.Migrations
{
    public partial class SeedIdentityUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 10, 11, 23, 1, 38, 967, DateTimeKind.Local).AddTicks(4953),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 10, 11, 22, 46, 34, 11, DateTimeKind.Local).AddTicks(2895));

            migrationBuilder.InsertData(
                table: "AppRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[] { new Guid("7225da6b-65fc-4b04-8f46-fd3176512eff"), "99b63b15-12fc-4842-97f1-924b458c3cdd", "Admin Role", "admin", "admin" });

            migrationBuilder.InsertData(
                table: "AppUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("7225da6b-65fc-4b04-8f46-fd3176512eff"), new Guid("d60a3a17-4053-42bb-a858-f44e7825bdf4") });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DOB", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("d60a3a17-4053-42bb-a858-f44e7825bdf4"), 0, "f1b32ae7-ba5b-4cb0-ad1f-8087631327e0", new DateTime(2000, 6, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "thduc.2000@gmail.com", true, "TRUONG HUYNH DUC", false, null, "thduc.2000@gmail.com", "admin", "AQAAAAEAACcQAAAAEC2ZwTVdR7ggkw08B/C2eKV5M55SxoJXHj7hQkS8+P7tRBoP5bjluBL9SPbJicE3Qw==", null, false, "", false, "admin" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2021, 10, 11, 23, 1, 38, 974, DateTimeKind.Local).AddTicks(6755));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2021, 10, 11, 23, 1, 38, 974, DateTimeKind.Local).AddTicks(7846));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("7225da6b-65fc-4b04-8f46-fd3176512eff"));

            migrationBuilder.DeleteData(
                table: "AppUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("7225da6b-65fc-4b04-8f46-fd3176512eff"), new Guid("d60a3a17-4053-42bb-a858-f44e7825bdf4") });

            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("d60a3a17-4053-42bb-a858-f44e7825bdf4"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 10, 11, 22, 46, 34, 11, DateTimeKind.Local).AddTicks(2895),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 10, 11, 23, 1, 38, 967, DateTimeKind.Local).AddTicks(4953));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2021, 10, 11, 22, 46, 34, 17, DateTimeKind.Local).AddTicks(8012));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2021, 10, 11, 22, 46, 34, 17, DateTimeKind.Local).AddTicks(9040));
        }
    }
}
