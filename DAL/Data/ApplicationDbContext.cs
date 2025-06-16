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

        public DbSet<BloodRequest> BloodRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //PRIMARY KEY
            modelBuilder.Entity<User>().HasKey(u => u.UserId);
            modelBuilder.Entity<RefreshToken>().HasKey(rt => rt.RefreshTokenId);
            modelBuilder.Entity<UserMedical>().HasKey(um => um.UserMedicalId);
            modelBuilder.Entity<BloodRegistration>().HasKey(um => um.BloodRegistrationId);
            modelBuilder.Entity<DonationHistory>().HasKey(um => um.DonationHistoryId);
            modelBuilder.Entity<BloodRequest>().HasKey(um => um.BloodRequestId);

            //User-Token (1-n)
            modelBuilder.Entity<RefreshToken>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(rt => rt.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            //User - UserMedical (1-n)
            // Đúng:
            modelBuilder.Entity<UserMedical>()
                .HasOne(um => um.User)
                .WithMany(u => u.UserMedicals)
                .HasForeignKey(um => um.UserId);

            //User - BloodRegistration (1-n)
            modelBuilder.Entity<BloodRegistration>()
    .HasOne(br => br.User)
    .WithMany(u => u.BloodRegistrations)
    .HasForeignKey(br => br.UserId);

            //User - DonationHistory (1-n)
            modelBuilder.Entity<DonationHistory>()
     .HasOne(dh => dh.User)
     .WithMany(u => u.DonationHistorys)
     .HasForeignKey(dh => dh.UserId);
            //UserMedical Blood (1-1)
            modelBuilder.Entity<UserMedical>()
                .HasOne(um => um.Blood)
                .WithOne(b => b.UserMedicals)
                .HasForeignKey<UserMedical>(um => um.BloodId);




            base.OnModelCreating(modelBuilder);
        }



    }
}
