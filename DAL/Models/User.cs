using Common.Enum;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public UserRole Role { get; set; }
        public int BloodTypeId { get; set; }
        public int LocationId { get; set; }
        public DateTime LastDonationDate { get; set; }
        public DateTime CreatedAt { get; set; }

        public BloodType BloodType { get; set; }
        public Location Location { get; set; }
        public ICollection<BloodRequest> BloodRequests { get; set; }
        public ICollection<Donation> Donations { get; set; }
        public ICollection<Blog> Blogs { get; set; }
        public ICollection<Reminder> Reminders { get; set; }
        public ICollection<Notification> Notifications { get; set; }
        public ICollection<Feedback> Feedbacks { get; set; }
        public ICollection<Event> OrganizedEvents { get; set; }
        public ICollection<EventParticipant> EventParticipants { get; set; }
        public ICollection<EventFeedback> EventFeedbacks { get; set; }
        public ICollection<BloodRequestStatusLog> StatusLogs { get; set; }
    }

}
