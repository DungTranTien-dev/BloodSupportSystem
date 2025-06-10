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

        public BloodRegistrationService (IUnitOfWork unitOfWork, UserUtility userUtility)
        {
            _unitOfWork = unitOfWork;
            _userUtility = userUtility;
        }

        public async Task<ResponseDTO> CreateByEvenId(Guid eventId)
        {
            var userId = _userUtility.GetUserIDFromToken();

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
                return new ResponseDTO($"Error saving Blood register: {ex.Message}", 500, false);
            }
            return new ResponseDTO("Registration successfully !!!", 200, true);
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
    }
}
