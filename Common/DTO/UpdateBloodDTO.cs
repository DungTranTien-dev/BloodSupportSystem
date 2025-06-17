using Common.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO
{
    public class UpdateBloodDTO
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string BloodName { get; set; }

        [Required]
        public BloodComponentType ComponentType { get; set; }

        [Range(50, 1000, ErrorMessage = "Volume must be between 50-1000ml")]
        public double? VolumeInML { get; set; }

        [DataType(DataType.Date)]
        public DateTime? CollectedDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ExpiryDate { get; set; }

        public bool IsAvailable { get; set; } = true;
    }
}
