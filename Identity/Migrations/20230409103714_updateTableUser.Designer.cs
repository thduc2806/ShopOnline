﻿// <auto-generated />
using System;
using Identity.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Identity.Migrations
{
    [DbContext(typeof(IdentityDbContext))]
    [Migration("20230409103714_updateTableUser")]
    partial class updateTableUser
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Identity.Database.Entities.RoleControls", b =>
                {
                    b.Property<int>("SuperiorFid")
                        .HasColumnType("int")
                        .HasColumnName("SuperiorFid");

                    b.Property<int>("SubordinateFid")
                        .HasColumnType("int")
                        .HasColumnName("SubordinateFid");

                    b.HasKey("SuperiorFid", "SubordinateFid");

                    b.HasIndex("SubordinateFid");

                    b.ToTable("RoleControls", (string)null);
                });

            modelBuilder.Entity("Identity.Database.Entities.Roles", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles", (string)null);
                });

            modelBuilder.Entity("Identity.Database.Entities.UserRoles", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles", (string)null);
                });

            modelBuilder.Entity("Identity.Database.Entities.Users", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("City");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("Country");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("CreatedBy");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime")
                        .HasColumnName("CreatedDate");

                    b.Property<DateTime?>("DOB")
                        .HasColumnType("datetime")
                        .HasColumnName("Birthday");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("FirstName");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActivated")
                        .HasColumnType("bit")
                        .HasColumnName("IsActivated");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)")
                        .HasColumnName("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("RefreshToken")
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)")
                        .HasColumnName("RefreshToken");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("State");

                    b.Property<string>("Street")
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)")
                        .HasColumnName("Street");

                    b.Property<DateTime?>("TokenEffectiveDate")
                        .HasColumnType("datetime")
                        .HasColumnName("TokenEffectiveDate");

                    b.Property<long?>("TokenEffectiveTimeStick")
                        .HasColumnType("bigint")
                        .HasColumnName("TokenEffectiveTimeStick");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("UserId");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ward")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AccessFailedCount = 0,
                            City = "HCM",
                            ConcurrencyStamp = "a4fc777f-3f43-4e65-becc-fada414d3c16",
                            Country = "HCM",
                            CreatedBy = new Guid("9e286e08-0309-4337-bb66-c5cac7bb8fae"),
                            CreatedDate = new DateTime(2023, 4, 9, 17, 37, 13, 632, DateTimeKind.Local).AddTicks(5625),
                            DOB = new DateTime(2000, 6, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "thduc.2000@gmail.com",
                            EmailConfirmed = true,
                            FirstName = "admin",
                            FullName = "admin",
                            IsActivated = true,
                            LastName = "admin",
                            LockoutEnabled = true,
                            NormalizedEmail = "thduc.2000@gmail.com",
                            NormalizedUserName = "admin",
                            PasswordHash = "AQAAAAEAACcQAAAAEHi/yyizHW44JTE4eLaRx4GeTwDJJgTQqRl+XvG9+XV657aNSL8jDkmUTk8RCBB3vQ==",
                            PhoneNumberConfirmed = false,
                            RefreshToken = "",
                            SecurityStamp = "",
                            State = "",
                            Street = "",
                            TokenEffectiveDate = new DateTime(2023, 4, 9, 17, 37, 13, 626, DateTimeKind.Local).AddTicks(245),
                            TokenEffectiveTimeStick = 0L,
                            TwoFactorEnabled = false,
                            UserId = new Guid("fe6eec2b-239b-4cb6-aeb1-25106220c7f0"),
                            UserName = "thduc.2000@gmail.com",
                            Ward = "Phường 13"
                        });
                });

            modelBuilder.Entity("Identity.Database.Entities.RoleControls", b =>
                {
                    b.HasOne("Identity.Database.Entities.Roles", "SubordinateRole")
                        .WithMany("SubordinateRoles")
                        .HasForeignKey("SubordinateFid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SubordinateRole");
                });

            modelBuilder.Entity("Identity.Database.Entities.UserRoles", b =>
                {
                    b.HasOne("Identity.Database.Entities.Roles", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Identity.Database.Entities.Users", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Identity.Database.Entities.Roles", b =>
                {
                    b.Navigation("SubordinateRoles");

                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("Identity.Database.Entities.Users", b =>
                {
                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}