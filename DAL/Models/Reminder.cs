using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Reminder
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime ReminderDate { get; set; }
        public string Content { get; set; }
        public string Status { get; set; } // pending, sent, acknowledged

        // Navigation property
        public User User { get; set; }
    }
}
