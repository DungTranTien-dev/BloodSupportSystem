using BLL.Services.Interface;
using BLL.Utilities;
using Common.DTO;
using DAL.Models;
using DAL.UnitOfWork;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services.Implement
{
    public class BloodRequestService : IBloodRequestService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserUtility _userUtility;

        public BloodRequestService(IUnitOfWork unitOfWork, UserUtility userUtility)
        {
            _unitOfWork = unitOfWork;
            _userUtility = userUtility;
        }

        public async Task<ResponseDTO> CreateBloodRequestAsync(CreateBloodRequestDTO dto)
        {
            var userId =  _userUtility.GetUserIdFromToken();

            if (userId == Guid.Empty)
            {
                return new ResponseDTO("khong thay user", 400, false);
            }


            var request = new BloodRequest
            {
                RequestedByUserId = userId,
                BloodRequestId = Guid.NewGuid(),                
                PatientName = dto.PatientName,
                HospitalName = dto.HospitalName,
                BloodGroup = dto.BloodGroup,
                ComponentType = dto.ComponentType,
                VolumeInML = dto.VolumeInML,
                Reason = dto.Reason,
                RequestedDate = dto.RequestedDate,
                Status = dto.Status,
                Latitue = dto.Latitue,
                Longtitue = dto.Longtitue
                
            };

            await _unitOfWork.BloodRequestRepo.AddAsync(request);
            await _unitOfWork.SaveChangeAsync();

            return new ResponseDTO("Blood request created successfully", 200, true);
        }

        public async Task<ResponseDTO> UpdateBloodRequestAsync(UpdateBloodRequestDTO dto)
        {
            var existing = await _unitOfWork.BloodRequestRepo.GetByIdAsync(dto.BloodRequestId);
            if (existing == null)
                return new ResponseDTO("Blood request not found", 404, false);

            
            existing.PatientName = dto.PatientName;
            existing.HospitalName = dto.HospitalName;
            existing.BloodGroup = dto.BloodGroup;
            existing.ComponentType = dto.ComponentType;
            existing.VolumeInML = dto.VolumeInML;
            existing.Reason = dto.Reason;
            existing.RequestedDate = dto.RequestedDate;
            existing.Status = dto.Status;

            await _unitOfWork.BloodRequestRepo.UpdateAsync(existing);
            await _unitOfWork.SaveChangeAsync();

            return new ResponseDTO("Blood request updated successfully", 200, true);
        }

        public async Task<ResponseDTO> DeleteBloodRequestAsync(Guid bloodRequestId)
        {
            var request = await _unitOfWork.BloodRequestRepo.GetByIdAsync(bloodRequestId);
            if (request == null)
                return new ResponseDTO("Blood request not found", 404, false);

            await _unitOfWork.BloodRequestRepo.DeleteAsync(bloodRequestId);
            await _unitOfWork.SaveChangeAsync();

            return new ResponseDTO("Blood request deleted successfully", 200, true);
        }

        public async Task<ResponseDTO> GetAllBloodRequestsAsync()
        {
            var list = _unitOfWork.BloodRequestRepo.GetAll();

            if (list == null || !list.Any())
                return new ResponseDTO("No blood requests found", 404, false);

            var result = list.Select(x => new
            {
                x.BloodRequestId,
                x.PatientName,
                x.HospitalName,
                x.BloodGroup,
                x.ComponentType,
                x.VolumeInML,
                x.Reason,
                x.RequestedDate,
                x.Status
            });

            return new ResponseDTO("List retrieved successfully", 200, true, result);
        }

        public async Task<ResponseDTO> GetBloodRequestByIdAsync(Guid bloodRequestId)
        {
            var request = await _unitOfWork.BloodRequestRepo.GetByIdAsync(bloodRequestId);
            if (request == null)
                return new ResponseDTO("Blood request not found", 404, false);

            var result = new
            {
                request.BloodRequestId,
                request.PatientName,
                request.HospitalName,
                request.BloodGroup,
                request.ComponentType,
                request.VolumeInML,
                request.Reason,
                request.RequestedDate,
                request.Status
            };

            return new ResponseDTO("Blood request retrieved successfully", 200, true, result);
        }
    }
}
