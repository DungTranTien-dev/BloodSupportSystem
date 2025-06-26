using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO
{
    public class DonationHistoryDto
    {
        public Guid DonationHistoryId { get; set; }
        public string BloodName { get; set; }
        public DateTime CreateAt { get; set; }
        public string Status { get; set; } // convert từ Enum sang string
    }
}

