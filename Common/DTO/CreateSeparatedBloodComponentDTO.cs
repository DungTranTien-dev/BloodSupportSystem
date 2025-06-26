using Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO
{
    public class CreateSeparatedBloodComponentDTO
    {
        public Guid BloodId { get; set; }
        
        public BloodComponentType ComponentType { get; set; } // RBC, PLASMA, PLATELET
        public double VolumeInML { get; set; }                // Thể tích của thành phần

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? ExpiryDate { get; set; }

        public bool IsAvailable { get; set; } = true;
    }
}
