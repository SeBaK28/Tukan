using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class ApplicationDbContext : IdentityDbContext<UserData>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<UserData> UserDatas { get; set; }
        public DbSet<FamillyData> FamillyDatas { get; set; }
        public DbSet<TransactionData> TransactionDatas { get; set; }        

        protected override void OnModelCreating(ModelBuilder builder)
        {
            
            base.OnModelCreating(builder);

            builder.Entity<UserData>()     //Create familly
                .HasMany(e => e.FamillyGroups)
                .WithMany(e => e.Members);

            builder.Entity<UserData>()         //relationship one User can have many transactions
                .HasMany(e => e.TransactionList)
                .WithOne(e => e.UserData)
                .HasForeignKey("UserDataId")
                .IsRequired(false);

            var FamillyCreatorRoleId = "a3b8f65c-4c8e-4f9b-9a83-72e1fbd99b2f";
            var FamillyMemberRoleId = "d12e7e62-7a24-49b1-bb34-6b9f9d3a8e2a";
            var adminAppId = "f57d4bc1-1e94-4c0f-b16e-0f09e36a73b4";

            var roles = new List<IdentityRole>
            {
                new IdentityRole{
                    Id = FamillyCreatorRoleId,
                    Name = "FamillyCreator",
                    NormalizedName = "FamillyCreator".ToUpper(),
                    ConcurrencyStamp = FamillyCreatorRoleId
                },
                new IdentityRole{
                    Id = FamillyMemberRoleId,
                    Name = "FamillyMember",
                    NormalizedName = "FamillyMember".ToUpper(),
                    ConcurrencyStamp = FamillyMemberRoleId
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);

            var admin = new UserData
            {
                Id = adminAppId,
                UserName = "admin@tukano.com",
                Email = "admin@tukano.com",
                NormalizedEmail = "admin@tukano.com".ToUpper(),
                NormalizedUserName = "admin@tukano.com".ToUpper()
            };

            admin.PasswordHash = new PasswordHasher<UserData>().HashPassword(admin, "Admin123$");

            builder.Entity<UserData>().HasData(admin);

            var adminRoles = new List<IdentityUserRole<string>>
            {
                new (){
                    UserId = adminAppId,
                    RoleId = FamillyCreatorRoleId
                },
                new (){
                    UserId = adminAppId,
                    RoleId = FamillyMemberRoleId
                }
            };

            builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);




        }
    }
}