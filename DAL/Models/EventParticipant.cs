using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class EventParticipant
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public int UserId { get; set; }
        public DateTime RegistrationDate { get; set; }

        // Navigation properties
        public Event Event { get; set; }
        public User User { get; set; }
    }
}
