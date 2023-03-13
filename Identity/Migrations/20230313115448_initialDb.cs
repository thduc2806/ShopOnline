using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Identity.Migrations
{
    public partial class initialDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Birthday = table.Column<DateTime>(type: "datetime", nullable: true),
                    Street = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    City = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    State = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    IsActivated = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    TokenEffectiveDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    TokenEffectiveTimeStick = table.Column<long>(type: "bigint", nullable: true),
                    RefreshToken = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "RoleControls",
                columns: table => new
                {
                    SuperiorFid = table.Column<int>(type: "int", nullable: false),
                    SubordinateFid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleControls", x => new { x.SuperiorFid, x.SubordinateFid });
                    table.ForeignKey(
                        name: "FK_RoleControls_Roles_SubordinateFid",
                        column: x => x.SubordinateFid,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "ID", "AccessFailedCount", "City", "ConcurrencyStamp", "Country", "CreatedBy", "CreatedDate", "Birthday", "Email", "EmailConfirmed", "FirstName", "IsActivated", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "SecurityStamp", "State", "Street", "TokenEffectiveDate", "TokenEffectiveTimeStick", "TwoFactorEnabled", "UserId", "UserName" },
                values: new object[] { 1, 0, "HCM", "7ba419c5-14a9-4a12-a6e7-91006753e8d2", "HCM", new Guid("de4fd7e2-de6c-4dc4-9f66-681a135d9a28"), new DateTime(2023, 3, 13, 18, 54, 48, 15, DateTimeKind.Local).AddTicks(4737), new DateTime(2000, 6, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "thduc.2000@gmail.com", true, "admin", true, "admin", true, null, "thduc.2000@gmail.com", "admin", "AQAAAAEAACcQAAAAED/1qopLIrHQ5AZgDRBpDs0nzuY1DsTh+rKHqE2n91TrT7l3WyMEURqeRKHCegoMcg==", null, false, "", "", "", "", new DateTime(2023, 3, 13, 18, 54, 48, 9, DateTimeKind.Local).AddTicks(5047), 0L, false, new Guid("fe6eec2b-239b-4cb6-aeb1-25106220c7f0"), "thduc.2000@gmail.com" });

            migrationBuilder.CreateIndex(
                name: "IX_RoleControls_SubordinateFid",
                table: "RoleControls",
                column: "SubordinateFid");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoleControls");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
