using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class BloodRequestStatusLog
    {
        public int Id { get; set; }
        public int BloodRequestId { get; set; }
        public int StaffId { get; set; }
        public string PreviousStatus { get; set; }
        public string NewStatus { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Note { get; set; }

        // Navigation properties
        public BloodRequest BloodRequest { get; set; }
        public User Staff { get; set; }
    }
}
