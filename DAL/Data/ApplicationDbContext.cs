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

        // MODELS
        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BloodRequest> BloodRequests { get; set; }
        public DbSet<BloodRequestStatusLog> BloodRequestStatusLogs { get; set; }
        public DbSet<BloodType> BloodTypes { get; set; }
        public DbSet<Donation> Donations { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventFeedback> EventFeedbacks { get; set; }
        public DbSet<EventParticipant> EventParticipants { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<DonationHistory> DonationHistories { get; set; }
        public DbSet<BloodUnit> BloodUnits { get; set; }
        public DbSet<Reminder> Reminders { get; set; }
        public DbSet<EventNotification> EventNotifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // PRIMARY KEYS
            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<RefreshToken>().HasKey(rt => rt.RefreshTokenId);
            modelBuilder.Entity<Blog>().HasKey(b => b.Id);
            modelBuilder.Entity<BloodRequest>().HasKey(br => br.Id);
            modelBuilder.Entity<BloodRequestStatusLog>().HasKey(brsl => brsl.Id);
            modelBuilder.Entity<BloodType>().HasKey(bt => bt.Id);
            modelBuilder.Entity<Donation>().HasKey(d => d.Id);
            modelBuilder.Entity<Event>().HasKey(e => e.Id);
            modelBuilder.Entity<EventFeedback>().HasKey(ef => ef.Id);
            modelBuilder.Entity<EventParticipant>().HasKey(ep => ep.Id);
            modelBuilder.Entity<Feedback>().HasKey(fb => fb.Id);
            modelBuilder.Entity<Notification>().HasKey(n => n.Id);
            modelBuilder.Entity<Location>().HasKey(l => l.Id);
            modelBuilder.Entity<DonationHistory>().HasKey(dh => dh.Id);
            modelBuilder.Entity<BloodUnit>().HasKey(bu => bu.Id);
            modelBuilder.Entity<Reminder>().HasKey(r => r.Id);
            modelBuilder.Entity<EventNotification>().HasKey(en => en.Id);

            // RELATIONSHIPS

            // User - RefreshToken (1-n)
            modelBuilder.Entity<RefreshToken>()
                .HasOne(rt => rt.User)
                .WithMany(u => u.RefreshTokens)
                .HasForeignKey(rt => rt.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Blog - User (Author) (n-1)
            modelBuilder.Entity<Blog>()
                .HasOne(b => b.Author)
                .WithMany(u => u.Blogs)
                .HasForeignKey(b => b.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);

            // BloodRequest - User (Requester) (n-1)
            modelBuilder.Entity<BloodRequest>()
                .HasOne(br => br.Requester)
                .WithMany(u => u.BloodRequests)
                .HasForeignKey(br => br.RequesterId)
                .OnDelete(DeleteBehavior.Restrict);

            // BloodRequest - BloodType (n-1)
            modelBuilder.Entity<BloodRequest>()
                .HasOne(br => br.BloodType)
                .WithMany(bt => bt.BloodRequests)
                .HasForeignKey(br => br.BloodTypeId)
                .OnDelete(DeleteBehavior.Cascade);

            // Donation - User (Donor) (n-1)
            modelBuilder.Entity<Donation>()
                .HasOne(d => d.Donor)
                .WithMany(u => u.Donations)
                .HasForeignKey(d => d.DonorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Donation - BloodRequest (n-1)
            modelBuilder.Entity<Donation>()
                .HasOne(d => d.BloodRequest)
                .WithMany(br => br.Donations)
                .HasForeignKey(d => d.BloodRequestId)
                .OnDelete(DeleteBehavior.Cascade);

            // BloodRequestStatusLog - BloodRequest (n-1)
            modelBuilder.Entity<BloodRequestStatusLog>()
                .HasOne(brsl => brsl.BloodRequest)
                .WithMany(br => br.StatusLogs)
                .HasForeignKey(brsl => brsl.BloodRequestId)
                .OnDelete(DeleteBehavior.Cascade);

            // BloodRequestStatusLog - User (Staff) (n-1)
            modelBuilder.Entity<BloodRequestStatusLog>()
                .HasOne(brsl => brsl.Staff)
                .WithMany(u => u.StatusLogs)
                .HasForeignKey(brsl => brsl.StaffId)
                .OnDelete(DeleteBehavior.Restrict);

            // User - BloodType (n-1)
            modelBuilder.Entity<User>()
                .HasOne(u => u.BloodType)
                .WithMany(bt => bt.Users)
                .HasForeignKey(u => u.BloodTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            // User - Location (n-1)
            modelBuilder.Entity<User>()
                .HasOne(u => u.Location)
                .WithMany(l => l.Users)
                .HasForeignKey(u => u.LocationId)
                .OnDelete(DeleteBehavior.Restrict);

            // Feedback - User (n-1)
            modelBuilder.Entity<Feedback>()
                .HasOne(fb => fb.User)
                .WithMany(u => u.Feedbacks)
                .HasForeignKey(fb => fb.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Notification - User (n-1)
            modelBuilder.Entity<Notification>()
                .HasOne(n => n.User)
                .WithMany(u => u.Notifications)
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Reminder - User (n-1)
            modelBuilder.Entity<Reminder>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reminders)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Event - User (Organizer) (n-1)
            modelBuilder.Entity<Event>()
                .HasOne(e => e.Organizer)
                .WithMany(u => u.OrganizedEvents)
                .HasForeignKey(e => e.OrganizerId)
                .OnDelete(DeleteBehavior.Restrict);

            // EventFeedback - Event (n-1)
            modelBuilder.Entity<EventFeedback>()
                .HasOne(ef => ef.Event)
                .WithMany(e => e.Feedbacks)
                .HasForeignKey(ef => ef.EventId)
                .OnDelete(DeleteBehavior.Cascade);

            // EventFeedback - User (n-1)
            modelBuilder.Entity<EventFeedback>()
                .HasOne(ef => ef.User)
                .WithMany(u => u.EventFeedbacks)
                .HasForeignKey(ef => ef.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // EventParticipant - Event (n-1)
            modelBuilder.Entity<EventParticipant>()
                .HasOne(ep => ep.Event)
                .WithMany(e => e.Participants)
                .HasForeignKey(ep => ep.EventId)
                .OnDelete(DeleteBehavior.Cascade);

            // EventParticipant - User (n-1)
            modelBuilder.Entity<EventParticipant>()
                .HasOne(ep => ep.User)
                .WithMany(u => u.EventParticipants)
                .HasForeignKey(ep => ep.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // EventNotification - Event (n-1)
            modelBuilder.Entity<EventNotification>()
                .HasOne(en => en.Event)
                .WithMany(e => e.Notifications)
                .HasForeignKey(en => en.EventId)
                .OnDelete(DeleteBehavior.Cascade);

            // EventNotification - User (n-1)
            modelBuilder.Entity<EventNotification>()
                .HasOne(en => en.User)
                .WithMany(u => u.EventNotifications)
                .HasForeignKey(en => en.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // BloodUnit - BloodType (n-1)
            modelBuilder.Entity<BloodUnit>()
                .HasOne(bu => bu.BloodType)
                .WithMany(bt => bt.BloodUnits)
                .HasForeignKey(bu => bu.BloodTypeId)
                .OnDelete(DeleteBehavior.Cascade);

            // DonationHistory - User (Member) (n-1)
            modelBuilder.Entity<DonationHistory>()
                .HasOne(dh => dh.Member)
                .WithMany()
                .HasForeignKey(dh => dh.MemberId)
                .OnDelete(DeleteBehavior.Restrict);

            // DonationHistory - BloodType (n-1)
            modelBuilder.Entity<DonationHistory>()
                .HasOne(dh => dh.BloodType)
                .WithMany(bt => bt.DonationHistories)
                .HasForeignKey(dh => dh.BloodTypeId)
                .OnDelete(DeleteBehavior.Cascade);

            // DonationHistory - Location (n-1)
            modelBuilder.Entity<DonationHistory>()
                .HasOne(dh => dh.Location)
                .WithMany(l => l.DonationHistories)
                .HasForeignKey(dh => dh.LocationId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}
