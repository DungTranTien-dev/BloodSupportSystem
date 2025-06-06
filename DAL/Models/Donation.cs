using Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Donation
    {
        public int Id { get; set; }
        public int DonorId { get; set; }
        public int BloodRequestId { get; set; }
        public DateTime DonationDate { get; set; }
        public DonationStatus Status { get; set; } // Use enum for status
        public string Notes { get; set; }

        // Navigation properties
        public User Donor { get; set; }
        public BloodRequest BloodRequest { get; set; }
    }
}
