using BLL.Services.Interface;
using Common.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Claims;
using DAL.UnitOfWork;

namespace BloodSupportSystemProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class SimpleAdminController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public SimpleAdminController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Test endpoint to verify API is working - No authentication required
        /// </summary>
        [HttpGet("test")]
        [AllowAnonymous]
        public IActionResult TestEndpoint()
        {
            return Ok(new { 
                message = "Admin API is working", 
                timestamp = DateTime.UtcNow,
                status = "success" 
            });
        }

        [HttpGet("dashboard-stats")]
        public async Task<ActionResult> GetDashboardStats()
        {
            try
            {
                // Get basic counts
                var users = await _unitOfWork.UserRepo.ToListAsync();
                var userMedicals = await _unitOfWork.UserMedicalRepo.ToListAsync();
                var bloodRequests = await _unitOfWork.BloodRequestRepo.ToListAsync();
                var blood = await _unitOfWork.BloodRepo.ToListAsync();
                var events = await _unitOfWork.EventRepo.ToListAsync();

                var stats = new
                {
                    TotalUsers = users.Count,
                    RegisteredDonors = userMedicals.Count,
                    TotalBloodRequests = bloodRequests.Count,
                    PendingRequests = bloodRequests.Count(br => br.Status == Common.Enum.BloodRequestStatus.PENDING),
                    TotalBloodUnits = blood.Count,
                    AvailableBloodUnits = blood.Count(b => b.IsAvailable == true),
                    TotalEvents = events.Count,
                    ActiveEvents = events.Count(e => e.StartTime <= DateTime.UtcNow && e.EndTime >= DateTime.UtcNow),
                    LastUpdated = DateTime.UtcNow
                };

                return Ok(stats);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving dashboard stats", error = ex.Message });
            }
        }

        [HttpGet("users")]
        public async Task<ActionResult> GetAllUsers()
        {
            try
            {
                var users = await _unitOfWork.UserRepo.ToListAsync();
                var userList = users.Select(u => new
                {
                    u.UserId,
                    u.UserName,
                    u.Email,
                    Role = u.Role ?? "User",
                    Status = u.Status ?? "Active",
                    CreatedAt = u.CreatedAt,
                    LastLoginDate = u.LastLoginDate
                }).ToList();

                return Ok(userList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving users", error = ex.Message });
            }
        }

        [HttpGet("blood-requests")]
        public async Task<ActionResult> GetAllBloodRequests()
        {
            try
            {
                var requests = await _unitOfWork.BloodRequestRepo.ToListAsync();
                var requestList = requests.Select(r => new
                {
                    r.BloodRequestId,
                    r.PatientName,
                    r.HospitalName,
                    r.BloodGroup,
                    ComponentType = r.ComponentType.ToString(),
                    r.VolumeInML,
                    r.Reason,
                    r.RequestedDate,
                    Status = r.Status.ToString(),
                    ApprovedBy = r.ApprovedBy,
                    ApprovedAt = r.ApprovedAt,
                    RejectionReason = r.RejectionReason
                }).ToList();

                return Ok(requestList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving blood requests", error = ex.Message });
            }
        }

        [HttpGet("blood-inventory")]
        public async Task<ActionResult> GetBloodInventory()
        {
            try
            {
                var blood = await _unitOfWork.BloodRepo.ToListAsync();
                var inventoryList = blood.Select(b => new
                {
                    b.BloodId,
                    b.BloodName,
                    BloodType = b.BloodType ?? "Unknown",
                    b.VolumeInML,
                    Status = b.Status.ToString(),
                    b.CollectedDate,
                    b.ExpiryDate,
                    b.IsAvailable,
                    IsExpired = b.ExpiryDate.HasValue && b.ExpiryDate <= DateTime.UtcNow
                }).ToList();

                return Ok(inventoryList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving blood inventory", error = ex.Message });
            }
        }

        [HttpGet("user-medicals")]
        public async Task<ActionResult> GetUserMedicals()
        {
            try
            {
                var userMedicals = await _unitOfWork.UserMedicalRepo.ToListAsync();
                var medicalList = userMedicals.Select(um => new
                {
                    um.UserMedicalId,
                    um.FullName,
                    um.CitizenId,
                    um.PhoneNumber,
                    um.Email,
                    um.Province,
                    um.CurrentAddress,
                    um.Gender,
                    Age = DateTime.Now.Year - um.DateOfBirth.Year, // Calculate age from DateOfBirth
                    Weight = 0.0, // Weight not available in UserMedical
                    um.DiseaseDescription,
                    IsActive = true // Assume active if record exists
                }).ToList();

                return Ok(medicalList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving user medical records", error = ex.Message });
            }
        }

        [HttpPost("blood-requests/{id}/approve")]
        public async Task<ActionResult> ApproveBloodRequest(Guid id)
        {
            try
            {
                var request = await _unitOfWork.BloodRequestRepo.GetByIdAsync(id);
                if (request == null)
                    return NotFound(new { message = "Blood request not found" });

                request.Status = Common.Enum.BloodRequestStatus.APPROVED;
                request.ApprovedBy = User.FindFirst(ClaimTypes.Name)?.Value ?? "Admin";
                request.ApprovedAt = DateTime.UtcNow;
                request.UpdatedAt = DateTime.UtcNow;

                await _unitOfWork.BloodRequestRepo.UpdateAsync(request);
                await _unitOfWork.SaveAsync();

                return Ok(new { message = "Blood request approved successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error approving blood request", error = ex.Message });
            }
        }

        [HttpPost("blood-requests/{id}/reject")]
        public async Task<ActionResult> RejectBloodRequest(Guid id, [FromBody] RejectRequestDTO rejectDto)
        {
            try
            {
                var request = await _unitOfWork.BloodRequestRepo.GetByIdAsync(id);
                if (request == null)
                    return NotFound(new { message = "Blood request not found" });

                request.Status = Common.Enum.BloodRequestStatus.REJECTED;
                request.RejectionReason = rejectDto.Reason;
                request.UpdatedAt = DateTime.UtcNow;

                await _unitOfWork.BloodRequestRepo.UpdateAsync(request);
                await _unitOfWork.SaveAsync();

                return Ok(new { message = "Blood request rejected successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error rejecting blood request", error = ex.Message });
            }
        }

        [HttpGet("events")]
        public async Task<ActionResult> GetAllEvents()
        {
            try
            {
                var events = await _unitOfWork.EventRepo.ToListAsync();
                var eventList = events.Select(e => new
                {
                    e.DonationEventId,
                    e.Title,
                    e.Location,
                    e.StartTime,
                    e.EndTime,
                    e.Description,
                    IsActive = e.StartTime <= DateTime.UtcNow && e.EndTime >= DateTime.UtcNow,
                    RegistrationCount = e.Registrations?.Count ?? 0
                }).ToList();

                return Ok(eventList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving events", error = ex.Message });
            }
        }

        [HttpGet("reports/summary")]
        public async Task<ActionResult> GetSummaryReport()
        {
            try
            {
                var users = await _unitOfWork.UserRepo.ToListAsync();
                var bloodRequests = await _unitOfWork.BloodRequestRepo.ToListAsync();
                var blood = await _unitOfWork.BloodRepo.ToListAsync();
                var events = await _unitOfWork.EventRepo.ToListAsync();

                var thisMonth = DateTime.UtcNow.AddDays(-30);

                var report = new
                {
                    GeneratedAt = DateTime.UtcNow,
                    UsersStats = new
                    {
                        Total = users.Count,
                        AdminUsers = users.Count(u => u.Role == "Admin" || u.Role == "SuperAdmin"),
                        ActiveUsers = users.Count(u => u.Status == "Active"),
                        NewUsersThisMonth = users.Count(u => u.CreatedAt >= thisMonth)
                    },
                    BloodRequestsStats = new
                    {
                        Total = bloodRequests.Count,
                        Pending = bloodRequests.Count(br => br.Status == Common.Enum.BloodRequestStatus.PENDING),
                        Approved = bloodRequests.Count(br => br.Status == Common.Enum.BloodRequestStatus.APPROVED),
                        Rejected = bloodRequests.Count(br => br.Status == Common.Enum.BloodRequestStatus.REJECTED),
                        Fulfilled = bloodRequests.Count(br => br.Status == Common.Enum.BloodRequestStatus.FULFILLED),
                        ThisMonth = bloodRequests.Count(br => br.RequestedDate >= thisMonth)
                    },
                    BloodInventoryStats = new
                    {
                        Total = blood.Count,
                        Available = blood.Count(b => b.IsAvailable == true),
                        Expired = blood.Count(b => b.ExpiryDate.HasValue && b.ExpiryDate <= DateTime.UtcNow),
                        ExpiringSoon = blood.Count(b => b.ExpiryDate.HasValue && 
                                                      b.ExpiryDate > DateTime.UtcNow && 
                                                      b.ExpiryDate <= DateTime.UtcNow.AddDays(7))
                    },
                    EventsStats = new
                    {
                        Total = events.Count,
                        Active = events.Count(e => e.StartTime <= DateTime.UtcNow && e.EndTime >= DateTime.UtcNow),
                        Upcoming = events.Count(e => e.StartTime > DateTime.UtcNow),
                        Past = events.Count(e => e.EndTime < DateTime.UtcNow)
                    }
                };

                return Ok(report);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error generating summary report", error = ex.Message });
            }
        }
    }

    // Helper DTOs
    public class RejectRequestDTO
    {
        public string Reason { get; set; } = "";
    }
}
