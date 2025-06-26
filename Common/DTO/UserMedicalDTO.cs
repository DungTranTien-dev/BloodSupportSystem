using Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO
{
    public class UserMedicalDTO
    {
        public Guid UserMedicalId { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public string CitizenId { get; set; }
        public string BloodName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Province { get; set; }
        public string CurrentAddress { get; set; }
        public bool HasDonatedBefore { get; set; }
        public int? DonationCount { get; set; }
        public string DiseaseDescription { get; set; }
        public double Latitue { get; set; }
        public double Longtitue { get; set; }
        public Guid UserId { get; set; }
        
    }
}
