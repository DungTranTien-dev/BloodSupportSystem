using Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class BloodRequest
    {
        public int Id { get; set; }
        public int RequesterId { get; set; }
        public int BloodTypeId { get; set; }
        public string ComponentType { get; set; } // whole, plasma, rbc, platelet
        public string UrgencyLevel { get; set; }
        public BloodRequestStatus Status { get; set; } // Use enum for status
        public DateTime RequestedDate { get; set; }
        public DateTime? ResolvedDate { get; set; }

        // Navigation properties
        public User Requester { get; set; }
        public BloodType BloodType { get; set; }
        public ICollection<Donation> Donations { get; set; }
        public ICollection<BloodRequestStatusLog> StatusLogs { get; set; }
    }
}
