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
        public string Status { get; set; } // pending, matched, completed
        public DateTime RequestedDate { get; set; }
        public DateTime? ResolvedDate { get; set; }
    }
}
