using Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Feedback
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
        public DateTime SubmittedAt { get; set; }
        public FeedbackStatus Status { get; set; } // Use enum for status

        // Navigation property
        public User User { get; set; }
    }
}
