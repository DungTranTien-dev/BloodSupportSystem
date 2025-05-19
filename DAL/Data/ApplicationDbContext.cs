using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        //MODELS
        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens {get; set;}
        public DbSet<UserMedical> UserMedicals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //PRIMARY KEY
            modelBuilder.Entity<User>().HasKey(u => u.UserId);
            modelBuilder.Entity<RefreshToken>().HasKey(rt => rt.RefreshTokenId);
            modelBuilder.Entity<UserMedical>().HasKey(um => um.UserBloodId);

            //User-Token (1-n)
            modelBuilder.Entity<RefreshToken>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(rt => rt.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            //User - UserMedical (1-n)
            modelBuilder.Entity<UserMedical>()
                .HasOne<User>()
                .WithMany(u => u.UserMedicals)
                .HasForeignKey(um => um.UserId)
                .OnDelete(DeleteBehavior.SetNull);


            base.OnModelCreating(modelBuilder);
        }



    }
}
