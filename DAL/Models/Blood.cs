using Common.Enum;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
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
        public string? BloodType { get; set; } // A+, A-, B+, B-, AB+, AB-, O+, O-
        public double? VolumeInML { get; set; } 
        public BloodSeparationStatus Status { get; set; }
        public DateTime? CollectedDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public DateTime? DonationDate { get; set; }
        public DateTime? ExpirationDate { get; set; } // Alias for ExpiryDate
        public bool? IsAvailable { get; set; }
        public UserMedical UserMedicals { get; set; }
        // Navigation - tách máu
        public ICollection<SeparatedBloodComponent> SeparatedComponents { get; set; }
        public string Code { get; set; }
    }
}
