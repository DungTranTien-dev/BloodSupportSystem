using Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class BloodRequest
    {
        public int Id { get; set; }

        public string PatientName { get; set; } = string.Empty;
        public int PatientAge { get; set; }
        public string BloodType { get; set; } = string.Empty;

        public string LocationName { get; set; } = string.Empty;

        // Dùng enum ở đây
        public LocationType LocationType { get; set; } = LocationType.HOSPITAL;

        public int QuantityInUnits { get; set; }
        public string UrgencyLevel { get; set; } = "Normal";
        public DateTime RequestedTime { get; set; }

        public string RequestedBy { get; set; } = string.Empty;
        public string ContactPhone { get; set; } = string.Empty;

        public string Notes { get; set; } = string.Empty;
    }

}
