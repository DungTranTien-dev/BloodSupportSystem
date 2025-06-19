using Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO
{
    public class CreateUserMediCalDTO
    {
        
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
    }
}
