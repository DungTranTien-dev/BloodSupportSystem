using Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
   public class DonationHistory
    {
        public Guid DonationHistoryId { get; set; }
        public string BloodName { get; set; }
        public DateTime CreateAt { get; set; }
        public DonateStatus Status { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
