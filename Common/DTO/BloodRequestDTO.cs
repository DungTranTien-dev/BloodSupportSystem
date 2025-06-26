using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO
{
    public class BloodRequestDTO
    {
        public Guid BloodRequestId { get; set; }
        public string PatientName { get; set; }
        public string HospitalName { get; set; }
        public string BloodGroup { get; set; }
        public string ComponentType { get; set; }
        public double VolumeInML { get; set; }
        public string Reason { get; set; }
        public DateTime RequestedDate { get; set; }
        public string Status { get; set; }
    }
}
