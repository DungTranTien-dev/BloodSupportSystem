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
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public string CitizenId { get; set; }

        public Guid BloodId { get; set; }
        public Blood Blood { get; set; }

        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Province { get; set; }
        public string CurrentAddress { get; set; }

        public bool HasDonatedBefore { get; set; }
        public int? DonationCount { get; set; }

        public string? DiseaseDescription { get; set; }
        public MedicalType Type { get; set; }

        public DateTime CreateDate { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        // Many-to-Many
        public ICollection<UserMedicalChronicDisease> UserMedicalChronicDiseases { get; set; }
    }

}
