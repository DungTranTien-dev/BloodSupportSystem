using Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Blood
    {
        public Guid BloodId { get; set; }
        public string BloodName { get; set; }
        public double? VolumeInML { get; set; } 
        public BloodComponentType ComponentType { get; set; } // RBC, Plasma, etc.
        public DateTime? CollectedDate { get; set; }
        public DateTime? ExpiryDate { get; set; } 
        public bool? IsAvailable { get; set; }
        public ICollection<UserMedical> UserMedicals { get; set; } = new List<UserMedical>();



    }
}
