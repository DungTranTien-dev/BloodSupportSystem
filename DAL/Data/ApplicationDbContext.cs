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
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<UserMedical> UserMedicals { get; set; }
        public DbSet<BloodRegistration> BloodRegistrations { get; set; }
        public DbSet<DonationHistory> DonationHistorys { get; set; }
        public DbSet<BloodRequest> BloodRequests { get; set; }
        public DbSet<Blood> Blood { get; set; }
        
        // Admin Models
        public DbSet<ContactQuery> ContactQueries { get; set; }
        public DbSet<SystemSetting> SystemSettings { get; set; }
        public DbSet<AdminActivityLog> AdminActivityLogs { get; set; }
        public DbSet<BloodGroupSetting> BloodGroupSettings { get; set; }
        public DbSet<AdminReport> AdminReports { get; set; }
        public DbSet<Notification> Notifications { get; set; }

 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //PRIMARY KEY
            modelBuilder.Entity<User>().HasKey(u => u.UserId);
            modelBuilder.Entity<RefreshToken>().HasKey(rt => rt.RefreshTokenId);
            modelBuilder.Entity<UserMedical>().HasKey(um => um.UserMedicalId);
            modelBuilder.Entity<BloodRegistration>().HasKey(um => um.BloodRegistrationId);
            modelBuilder.Entity<DonationHistory>().HasKey(um => um.DonationHistoryId);
            modelBuilder.Entity<BloodRequest>().HasKey(um => um.BloodRequestId);
            modelBuilder.Entity<UserMedicalChronicDisease>()
                .HasKey(uc => new { uc.UserMedicalId, uc.ChronicDiseaseId });

            // Admin Models Primary Keys
            modelBuilder.Entity<ContactQuery>().HasKey(cq => cq.Id);
            modelBuilder.Entity<SystemSetting>().HasKey(ss => ss.Key);
            modelBuilder.Entity<AdminActivityLog>().HasKey(al => al.Id);
            modelBuilder.Entity<BloodGroupSetting>().HasKey(bgs => bgs.BloodType);
            modelBuilder.Entity<AdminReport>().HasKey(ar => ar.Id);
            modelBuilder.Entity<Notification>().HasKey(n => n.Id);

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


            modelBuilder.Entity<Blood>()
                .HasMany(b => b.SeparatedComponents)
                .WithOne(sc => sc.Blood)
                .HasForeignKey(sc => sc.BloodId);




            modelBuilder.Entity<UserMedicalChronicDisease>()
                .HasOne(uc => uc.UserMedical)
                .WithMany(u => u.UserMedicalChronicDiseases)
                .HasForeignKey(uc => uc.UserMedicalId);

            modelBuilder.Entity<UserMedicalChronicDisease>()
                .HasOne(uc => uc.ChronicDisease)
                .WithMany(c => c.UserMedicalChronicDiseases)
                .HasForeignKey(uc => uc.ChronicDiseaseId);

            // Admin model relationships
            modelBuilder.Entity<Notification>()
                .HasOne(n => n.TargetUser)
                .WithMany()
                .HasForeignKey(n => n.TargetUserId)
                .OnDelete(DeleteBehavior.SetNull);

            DbSeeder.Seed(modelBuilder);


            base.OnModelCreating(modelBuilder);
        }
    }
}
