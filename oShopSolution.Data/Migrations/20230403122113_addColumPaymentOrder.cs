using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace oShopSolution.Data.Migrations
{
    public partial class addColumPaymentOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PaymentId",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("7225da6b-65fc-4b04-8f46-fd3176512eff"),
                column: "ConcurrencyStamp",
                value: "8ac44ed9-2867-427c-baf5-afebe1477aa8");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("d60a3a17-4053-42bb-a858-f44e7825bdf4"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "12267bc8-990e-45eb-898b-c692bb7a738a", "AQAAAAEAACcQAAAAEO9SpFAiGRsKfm/Rqc4GZQ6+ivv3iRNsBWG7C/3akx1j7UyIE1q67FT88FqB9HuV2A==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2023, 4, 3, 19, 21, 13, 108, DateTimeKind.Local).AddTicks(7622));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2023, 4, 3, 19, 21, 13, 109, DateTimeKind.Local).AddTicks(6273));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentId",
                table: "Orders");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("7225da6b-65fc-4b04-8f46-fd3176512eff"),
                column: "ConcurrencyStamp",
                value: "7d845bf7-a167-4cb9-a82c-3678f5eebacc");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("d60a3a17-4053-42bb-a858-f44e7825bdf4"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ae40fdac-6192-48b7-a811-9eb64e8e11b5", "AQAAAAEAACcQAAAAEABcvMGfuNa8DiaIr6VK1We9Xfo0MoOaFeq9qi4nAVqqBo3+X8MjUjOXd0Kl0HvVLA==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2023, 4, 3, 19, 14, 12, 357, DateTimeKind.Local).AddTicks(21));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2023, 4, 3, 19, 14, 12, 361, DateTimeKind.Local).AddTicks(5491));
        }
    }
}
