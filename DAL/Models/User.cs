using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class User
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ICollection<UserMedical> UserMedicals { get; set; } = new HashSet<UserMedical>();
        public ICollection<BloodRegistration> BloodRegistrations { get; set; } = new HashSet<BloodRegistration>();

        public ICollection<DonationHistory> DonationHistorys { get; set; } = new HashSet<DonationHistory>();

    }
}
