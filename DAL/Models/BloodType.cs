using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class BloodType
    {
        public int Id { get; set; }
        public string Name { get; set; } // A+, A-, B+, etc.
        public string CanDonateTo { get; set; }
        public string CanReceiveFrom { get; set; }

        // Navigation property
        public ICollection<BloodRequest> BloodRequests { get; set; }
        public ICollection<Donation> Donations { get; set; }
        public ICollection<BloodUnit> BloodUnits { get; set; }
        public ICollection<DonationHistory> DonationHistories { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
