using Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class BloodRegistration
    {
        public Guid BloodRegistrationId { get; set; }

        public DateTime CreateDate { get; set; }

        public RegisterType Type { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid DonationEventId { get; set; }
        public DonationEvent DonationEvent { get; set; }
    }

}
