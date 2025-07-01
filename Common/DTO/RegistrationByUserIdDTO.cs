using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO
{
    public class RegistrationByUserIdDTO
    {
        public Guid BloodRegistrationId { get; set; }
        public DateTime CreateDate { get; set; }
        public string RegisterType { get; set; }

        // Info from DonationEvent
        public Guid DonationEventId { get; set; }
        public string EventTitle { get; set; }
        public string EventLocation { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
