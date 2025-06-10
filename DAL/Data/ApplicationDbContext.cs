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
        public DbSet<BloodRegistration> BloodRegistrations { get; set; }
        public DbSet<DonationHistory> DonationHistorys { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //PRIMARY KEY
            modelBuilder.Entity<User>().HasKey(u => u.UserId);
            modelBuilder.Entity<RefreshToken>().HasKey(rt => rt.RefreshTokenId);
            modelBuilder.Entity<UserMedical>().HasKey(um => um.UserBloodId);
            modelBuilder.Entity<BloodRegistration>().HasKey(um => um.BloodRegistrationId);
            modelBuilder.Entity<DonationHistory>().HasKey(um => um.DonationHistoryId);

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

            //User - BloodRegistration (1-n)
            modelBuilder.Entity<BloodRegistration>()
                .HasOne<User>()
                .WithMany(u => u.BloodRegistrations)
                .HasForeignKey(um => um.UserId)
                .OnDelete(DeleteBehavior.SetNull);

            //User - DonationHistory (1-n)
            modelBuilder.Entity<DonationHistory>()
                .HasOne<User>()
                .WithMany(u => u.DonationHistorys)
                .HasForeignKey(um => um.UserId)
                .OnDelete(DeleteBehavior.SetNull);


            base.OnModelCreating(modelBuilder);
        }



    }
}
