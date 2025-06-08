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
        public Guid UserId { get; set; } // Should be Guid to match User.Id

        // Navigation properties
        public Event Event { get; set; }
        public User User { get; set; }
    }
}
