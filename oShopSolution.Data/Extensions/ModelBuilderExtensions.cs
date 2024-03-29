﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using oShopSolution.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oShopSolution.Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppConfig>().HasData(
               new AppConfig() { Key = "HomeTitle", Value = "This is home page of eShopSolution" },
               new AppConfig() { Key = "HomeKeyword", Value = "This is keyword of eShopSolution" },
               new AppConfig() { Key = "HomeDescription", Value = "This is description of eShopSolution" }
               );

            modelBuilder.Entity<Category>().HasData(
                new Category()
                {
                    Id = 1,
                    Name = "Apple",
                    Description = "This Is Apple",
                },
                new Category()
                {
                    Id = 2,
                    Name = "Samsung",
                    Description = "This Is Samsung",
                });
            modelBuilder.Entity<Product>().HasData(
           new Product()
           {
               Id = 1,
               Name = "Iphone 12 Promax",
               CreateDate = DateTime.Now,
               Price = 100000,
               Description = "This is Iphone 12 Promax",
               Rating = 5,
               CategoryId = 1,
               ThumbPath = "https://www.imagevenue.com/ME13YCXU][IMG]https://cdn-thumbs.imagevenue.com/b5/a4/b5/ME13YCXU_t.png"
           },
            new Product()
            {
                Id = 2,
                Name = "Samsung Galaxy Fold",
                CreateDate = DateTime.Now,
                Price = 20000,
                Description = "This is Samsung Galaxy Fold",
                Rating = 10,
                CategoryId = 2
            });

            var roleId = new Guid("7225DA6B-65FC-4B04-8F46-FD3176512EFF");  /*< Guid("7225DA6B-65FC-4B04-8F46-FD3176512EFF") >*/
            var adminId = new Guid("D60A3A17-4053-42BB-A858-F44E7825BDF4");  /*< Guid("D60A3A17-4053-42BB-A858-F44E7825BDF4") >*/
            modelBuilder.Entity<AppRole>().HasData(new AppRole
            {
                Id = roleId,
                Name = "admin",
                NormalizedName = "admin",
                Description = "Admin Role"
            });

            var hasher = new PasswordHasher<AppUser>();
            modelBuilder.Entity<AppUser>().HasData(new AppUser
            {
                Id = adminId,
                UserName = "admin",
                NormalizedUserName = "admin",
                Email = "thduc.2000@gmail.com",
                NormalizedEmail = "thduc.2000@gmail.com",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Duc@123"),
                SecurityStamp = string.Empty,
                FullName = "TRUONG HUYNH DUC",
                DOB = new DateTime(2000, 06, 28)
            });

            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = roleId,
                UserId = adminId
            });
        }
    }
}
