using Identity.Database.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Identity.Database
{
    public static class Seed
    {
        public static void Seeds(this ModelBuilder modelBuilder)
        {
            var hasher = new PasswordHasher<Users>();
            modelBuilder.Entity<Users>().HasData(new Users("thduc.2000@gmail.com","admin","admin", "admin")
            {
                Id = 1,
                UserId = new Guid("FE6EEC2B-239B-4CB6-AEB1-25106220C7F0"),
                UserName = "thduc.2000@gmail.com",
                NormalizedUserName = "admin",
                NormalizedEmail = "thduc.2000@gmail.com",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Duc@123"),
                SecurityStamp = string.Empty,
                DOB = new DateTime(2000, 06, 28),
                City = "HCM",
                Country = "HCM",
                State = "",
                Street = "",
                RefreshToken = "",
                CreatedDate = DateTime.Now,
                FullName = "admin",
                Ward = "Phường 13"
            });
        }
    }
}
