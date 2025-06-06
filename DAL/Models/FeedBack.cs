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
        public string Status { get; set; } // new, resolved, ignored

        // Navigation property
        public User User { get; set; }
    }
}
