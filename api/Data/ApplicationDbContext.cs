using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        
        public DbSet<UserData> UserDatas { get; set; }
        public DbSet<FamillyData> FamillyDatas { get; set; }
        public DbSet<TransactionData> TransactionDatas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserData>()     //Create familly
                .HasMany(e => e.FamillyGroups)
                .WithMany(e => e.Members);

            modelBuilder.Entity<UserData>()         //relationship one User can have many transactions
                .HasMany(e => e.TransactionList)
                .WithOne(e => e.UserData)
                .HasForeignKey("UserDataId")
                .IsRequired(false);
        }
    }
}