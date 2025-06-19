using Common.Enum;
using System;
using System.ComponentModel.DataAnnotations;

namespace Common.DTO
{
    public class CreateBloodDTO
    {
        public string BloodName { get; set; }
        public BloodComponentType ComponentType { get; set; }
        public double? VolumeInML { get; set; }
        public DateTime? CollectedDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public bool IsAvailable { get; set; } = true;
        public Guid? UserId { get; set; } // Changed to nullable
    }
}
