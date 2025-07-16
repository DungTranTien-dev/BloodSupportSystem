using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    [Table("ContactQueries")]
    public class ContactQuery
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(200)]
        public string Subject { get; set; }

        [Required]
        [StringLength(2000)]
        public string Message { get; set; }

        [StringLength(50)]
        public string Status { get; set; } = "Pending"; // Pending, Responded, Closed

        [StringLength(2000)]
        public string? AdminResponse { get; set; }

        [StringLength(100)]
        public string? HandledBy { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? RespondedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }

    [Table("SystemSettings")]
    public class SystemSetting
    {
        [Key]
        [StringLength(100)]
        public string Key { get; set; }

        [Required]
        [StringLength(1000)]
        public string Value { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        [StringLength(50)]
        public string Category { get; set; } = "General";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime LastModified { get; set; } = DateTime.UtcNow;

        [StringLength(100)]
        public string? ModifiedBy { get; set; }

        public bool IsActive { get; set; } = true;
    }

    [Table("AdminActivityLogs")]
    public class AdminActivityLog
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(100)]
        public string AdminName { get; set; }

        [Required]
        [StringLength(200)]
        public string Action { get; set; }

        [Required]
        [StringLength(100)]
        public string Module { get; set; }

        [StringLength(1000)]
        public string? Details { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        [StringLength(45)]
        public string? IpAddress { get; set; }

        [StringLength(500)]
        public string? UserAgent { get; set; }

        public bool IsSuccess { get; set; } = true;

        [StringLength(1000)]
        public string? ErrorMessage { get; set; }
    }

    [Table("BloodGroupSettings")]
    public class BloodGroupSetting
    {
        [Key]
        [StringLength(10)]
        public string BloodType { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        public int? MinimumStock { get; set; }

        public int? MaximumStock { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        [StringLength(100)]
        public string? ModifiedBy { get; set; }
    }

    [Table("AdminReports")]
    public class AdminReport
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(100)]
        public string ReportType { get; set; }

        [Required]
        [StringLength(200)]
        public string ReportName { get; set; }

        [Required]
        public string ReportData { get; set; } // JSON string

        [Required]
        [StringLength(100)]
        public string GeneratedBy { get; set; }

        public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [StringLength(500)]
        public string? Filters { get; set; } // JSON string

        public bool IsArchived { get; set; } = false;
    }

    [Table("Notifications")]
    public class Notification
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        [StringLength(1000)]
        public string Message { get; set; }

        [Required]
        [StringLength(50)]
        public string Type { get; set; } // Info, Warning, Error, Success

        [StringLength(100)]
        public string? TargetRole { get; set; } // Admin, User, All

        public Guid? TargetUserId { get; set; }

        public bool IsRead { get; set; } = false;

        public bool IsGlobal { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? ReadAt { get; set; }

        public DateTime? ExpiresAt { get; set; }

        [StringLength(100)]
        public string? CreatedBy { get; set; }

        // Navigation properties
        [ForeignKey("TargetUserId")]
        public virtual User? TargetUser { get; set; }
    }
}
