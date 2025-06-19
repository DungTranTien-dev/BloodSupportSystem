using Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO
{
    public class BloodResponseDTO
    {
        public Guid BloodId { get; set; }
        public string BloodName { get; set; }
        public BloodComponentType ComponentType { get; set; }
        public double? VolumeInML { get; set; }
        public DateTime? CollectedDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
