using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class DonationHistory
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public DateTime DonationDate { get; set; }
        public int BloodTypeId { get; set; }
        public int LocationId { get; set; }
        public string Status { get; set; }

        // Navigation properties
        public User Member { get; set; }
        public BloodType BloodType { get; set; }
        public Location Location { get; set; }
    }
}
