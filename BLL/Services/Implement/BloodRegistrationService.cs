    using BLL.Services.Interface;
using BLL.Utilities;
using Common.DTO;
using Common.Enum;
using DAL.Models;
using DAL.UnitOfWork;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Implement
{
    public class BloodRegistrationService : IBloodRegistrationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserUtility _userUtility;

        public BloodRegistrationService(IUnitOfWork unitOfWork, UserUtility userUtility)
        {
            _unitOfWork = unitOfWork;
            _userUtility = userUtility;
        }

        public async Task<ResponseDTO> CreateByEvenId(Guid eventId)
        {
            var userId = _userUtility.GetUserIdFromToken();

            // Lấy sự kiện mới chuẩn bị đăng ký
            var newEvent = await _unitOfWork.EventRepo.GetByIdAsync(eventId);
            if (newEvent == null)
            {
                return new ResponseDTO("Event not found.", 404, false);
            }

            // Kiểm tra user đã đăng ký sự kiện nào khác chưa trùng thời điểm
            var conflict = await _unitOfWork.BloodRegistrationRepo
                .FirstOrDefaultAsync(r =>
                    r.UserId == userId &&
                    r.Type != RegisterType.CANCEL && // Chỉ tính những đăng ký còn hiệu lực
                    r.DonationEventId != eventId && // Khác với sự kiện hiện tại
                    r.DonationEvent.StartTime < newEvent.EndTime &&
                    r.DonationEvent.EndTime > newEvent.StartTime);

            if (conflict != null)
            {
                return new ResponseDTO("You have already registered for another event at the same time.", 400, false);
            }

            // Kiểm tra đã đăng ký sự kiện này chưa
            var existed = await _unitOfWork.BloodRegistrationRepo
                .FirstOrDefaultAsync(r =>
                    r.UserId == userId &&
                    r.DonationEventId == eventId &&
                    r.Type != RegisterType.CANCEL);

            if (existed != null)
            {
                return new ResponseDTO("You have already registered for this event.", 400, false);
            }

            // Tạo đăng ký mới
            var registration = new BloodRegistration
            {
                BloodRegistrationId = Guid.NewGuid(),
                CreateDate = DateTime.UtcNow,
                Type = RegisterType.PENDING,
                UserId = userId,
                DonationEventId = eventId
            };

            try
            {
                await _unitOfWork.BloodRegistrationRepo.AddAsync(registration);
                await _unitOfWork.SaveChangeAsync();
            }
            catch (Exception ex)
            {
                return new ResponseDTO($"Error saving Blood register: {ex.InnerException?.Message ?? ex.Message}", 500, false);
            }

            return new ResponseDTO("Registration successful!", 200, true);
        }


        public async Task<ResponseDTO> UpdateStatus(RegisterType type, Guid id)
        {
            var bloodRegister = await _unitOfWork.BloodRegistrationRepo.GetByIdAsync(id);

            if (bloodRegister == null)
            {
                return new ResponseDTO("Not valid", 400, false);
            }
            bloodRegister.Type = type;
            try
            {
                await _unitOfWork.BloodRegistrationRepo.UpdateAsync(bloodRegister);
                await _unitOfWork.SaveChangeAsync();
            }
            catch (Exception ex)
            {
                return new ResponseDTO($"Error saving Blood register: {ex.Message}", 500, false);
            }

            return new ResponseDTO("Change successfully ", 200, true);

        }

        public async Task<ResponseDTO> GetByUserId()
        {
            var userId = _userUtility.GetUserIdFromToken();
            var registrations = await _unitOfWork.BloodRegistrationRepo.GetByUserIdAsync(userId);

            if (registrations == null || !registrations.Any())
            {
                return new ResponseDTO("No registrations found for this user.", 404, false);
            }

            return new ResponseDTO("Registrations retrieved successfully.", 200, true, registrations);
        }

        public async Task<ResponseDTO> GetAll()
        {
            var registrations = await _unitOfWork.BloodRegistrationRepo.GetAllRegistration();

            if (registrations == null || !registrations.Any())
            {
                return new ResponseDTO("No registrations found.", 404, false);
            }

            var registrationDTOs = registrations.Select(r => new RegistrationByUserIdDTO
            {
                BloodRegistrationId = r.BloodRegistrationId,
                CreateDate = r.CreateDate,
                RegisterType = r.Type.ToString(),
                DonationEventId = r.DonationEvent.DonationEventId,
                EventTitle = r.DonationEvent.Title,
                EventLocation = r.DonationEvent.Location,
                StartTime = r.DonationEvent.StartTime,
                EndTime = r.DonationEvent.EndTime,
                FullName = r.User.UserName,
                Email = r.User.Email
            }).ToList();

            return new ResponseDTO("Registrations retrieved successfully.", 200, true, registrationDTOs);
        }

    }
}
