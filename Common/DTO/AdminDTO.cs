using System;
using System.ComponentModel.DataAnnotations;

namespace Common.DTO
{
    // Dashboard DTOs
    public class DashboardStatsDTO
    {
        public int TotalBloodGroups { get; set; }
        public int RegisteredDonors { get; set; }
        public int PendingRequests { get; set; }
        public int TotalQueries { get; set; }
        public double BloodInventoryLevel { get; set; }
        public int ActiveEvents { get; set; }
    }

    public class BloodInventoryStatDTO
    {
        public string BloodType { get; set; }
        public int AvailableUnits { get; set; }
        public int ReservedUnits { get; set; }
        public int ExpiredUnits { get; set; }
        public DateTime LastUpdated { get; set; }
    }

    // Admin User Management DTOs
    public class AdminUserDTO
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class CreateAdminUserDTO
    {
        [Required]
        [StringLength(100)]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; } // admin, staff, etc.
    }

    public class UpdateAdminUserDTO
    {
        [Required]
        [StringLength(100)]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string? Password { get; set; } // Optional for updates

        [Required]
        public string Role { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }

    // Contact Query Management DTOs
    public class ContactQueryDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string Status { get; set; } // Pending, Responded, Closed
        public DateTime CreatedAt { get; set; }
        public DateTime? RespondedAt { get; set; }
        public string? AdminResponse { get; set; }
        public string? HandledBy { get; set; }
    }

    public class RespondToQueryDTO
    {
        [Required]
        public string Response { get; set; }
        
        public string Status { get; set; } = "Responded";
    }

    // Blood Request Management DTOs
    public class AdminBloodRequestDTO
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
        public string? ApprovedBy { get; set; }
        public DateTime? ApprovedAt { get; set; }
        public string? RejectionReason { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

    public class ProcessBloodRequestDTO
    {
        [Required]
        public string Action { get; set; } // Approve, Reject, Fulfill
        
        public string? Notes { get; set; }
        
        public List<Guid>? BloodComponentIds { get; set; } // For fulfillment
    }

    // Blood Group Management DTOs
    public class BloodGroupStatsDTO
    {
        public string BloodType { get; set; }
        public int TotalDonors { get; set; }
        public int AvailableUnits { get; set; }
        public int RequestCount { get; set; }
        public DateTime LastDonation { get; set; }
        public bool IsActive { get; set; }
    }

    public class ManageBloodGroupDTO
    {
        [Required]
        public string BloodType { get; set; }
        
        [Required]
        public bool IsActive { get; set; }
        
        public string? Description { get; set; }
        public int? MinimumStock { get; set; }
        public int? MaximumStock { get; set; }
    }

    // System Settings DTOs
    public class SystemSettingDTO
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public DateTime LastModified { get; set; }
        public string ModifiedBy { get; set; }
    }

    public class UpdateSystemSettingDTO
    {
        [Required]
        public string Key { get; set; }
        
        [Required]
        public string Value { get; set; }
        
        public string? Description { get; set; }
    }

    // Activity Log DTOs
    public class AdminActivityLogDTO
    {
        public Guid Id { get; set; }
        public string AdminName { get; set; }
        public string Action { get; set; }
        public string Module { get; set; }
        public string? Details { get; set; }
        public DateTime Timestamp { get; set; }
        public string IpAddress { get; set; }
    }

    // Report DTOs
    public class AdminReportDTO
    {
        public string ReportType { get; set; }
        public DateTime GeneratedAt { get; set; }
        public object Data { get; set; }
        public string GeneratedBy { get; set; }
    }

    public class GenerateReportRequestDTO
    {
        [Required]
        public string ReportType { get; set; } // Daily, Weekly, Monthly
        
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<string>? Filters { get; set; }
    }
}
