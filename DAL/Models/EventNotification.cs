using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class EventNotification
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public Guid UserId { get; set; }
        public string NotificationMessage { get; set; }
        public DateTime SentAt { get; set; }

        // Navigation properties
        public Event Event { get; set; }
        public User User { get; set; }
    }
}
