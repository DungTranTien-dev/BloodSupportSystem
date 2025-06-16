using Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class UserMedical
    {
        public Guid UserMedicalId { get; set; }
        public Guid BloodId { get; set; }
        public Blood Blood { get; set; }
        public string Weight { get; set; }
        public string CurrentHealthStatus { get; set; }
        public bool HasTransmissibleDisease { get; set; }
        public int DonationCount { get; set; }
        public DateTime LastDonationDate { get; set; }
        public bool IsTakingMedication { get; set; }
        public DateTime CreateDate { get; set; }
        public MedicalType Status { get; set; }
        public User User { get; set; }
        public Guid UserId { get; set; }
    }
}
