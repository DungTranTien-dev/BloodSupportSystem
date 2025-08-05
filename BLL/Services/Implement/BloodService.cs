using BLL.Services.Interface;
using Common.DTO;
using Common.Enum;

using DAL.Models;
using DAL.Repositories.Interface;
using DAL.Repositories.Interfaces;
using DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace BLL.Services.Implement
{
    public class BloodService : IBloodService
    {
        private readonly IBloodRepository _bloodRepo;
        private readonly IUnitOfWork _unitOfWork;

        public BloodService(
            IBloodRepository bloodRepo,
            IUnitOfWork unitOfWork)
        {
            _bloodRepo = bloodRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseDTO> GetBloodByIdAsync(Guid id)
        {
            var blood = await _bloodRepo.GetBloodByIdAsync(id);
            if (blood == null)
                return new ResponseDTO("Blood not found.", 404, false, null);

            var dto = new BloodResponseDTO
            {
                BloodId = blood.BloodId,
                BloodName = blood.BloodName,
                VolumeInML = blood.VolumeInML,
                CollectedDate = blood.CollectedDate,
                ExpiryDate = blood.ExpiryDate,
                IsAvailable = blood.IsAvailable,
                Status = blood.Status.ToString(),
                Code = blood.Code,

            };

            return new ResponseDTO("Blood retrieved successfully.", 200, true, dto);
        }

        public async Task<ResponseDTO> GetAllBloodsAsync()
        {
            var bloods = await _bloodRepo.GetAllAsync();
            var dtoList = bloods.Select(b => new BloodResponseDTO
            {
                BloodId = b.BloodId,
                BloodName = b.BloodName,
                VolumeInML = b.VolumeInML,
                CollectedDate = b.CollectedDate,
                ExpiryDate = b.ExpiryDate,
                IsAvailable = b.IsAvailable,
                Status = b.Status.ToString(),
                Code = b.Code,
                UserName = b.UserMedicals?.FullName ?? "Unknown",
         
            }).ToList();

            return new ResponseDTO("Bloods retrieved successfully.", 200, true, dtoList);
        }

        public async Task<ResponseDTO> CreateBloodAsync(CreateBloodDTO dto)
        {
            var blood = new Blood
            {
                BloodId = Guid.NewGuid(),
                BloodName = dto.BloodName,
                VolumeInML = dto.VolumeInML,
                CollectedDate = dto.CollectedDate,
                ExpiryDate = dto.CollectedDate.HasValue ? dto.CollectedDate.Value.AddDays(42) : (DateTime?)null,
                IsAvailable = true,
                Status = BloodSeparationStatus.UNPROCESSED,
                Code = await GenerateNewBloodCodeAsync(),
                UserMedicalId = dto.UserMedicalId

            };

            var dtoResponse = new BloodResponseDTO
            {
                BloodId = blood.BloodId,
                BloodName = blood.BloodName,
                VolumeInML = blood.VolumeInML,
                CollectedDate = blood.CollectedDate,
                ExpiryDate = blood.CollectedDate.HasValue ? blood.CollectedDate.Value.AddDays(42) : (DateTime?)null,
                IsAvailable = blood.IsAvailable,
                Status = blood.Status.ToString(),
                Code = blood.Code,
                UserName = blood.UserMedicals?.FullName ?? "Unknown",
            };


            await _bloodRepo.CreateBloodAsync(blood);

            // 2. Tăng số lần hiến của user
            var user = await _unitOfWork.UserMedicalRepo.GetByIdAsync(blood.UserMedicalId);
            if (user != null)
            {
                user.DonationCount += 1;
                _unitOfWork.UserMedicalRepo.UpdateAsync(user); // hoặc EF tracking tự động
            }

            // 3. Lưu thay đổi
            await _unitOfWork.SaveChangeAsync();

            return new ResponseDTO("Blood created successfully.", 201, true, dtoResponse);
        }

        public async Task<ResponseDTO> UpdateBloodAsync(Guid id, UpdateBloodDTO dto)
        {
            var blood = await _bloodRepo.GetBloodByIdAsync(id);
            if (blood == null)
                return new ResponseDTO("Blood not found.", 404, false, null);

            blood.BloodName = dto.BloodName;
            blood.VolumeInML = dto.VolumeInML;
            blood.CollectedDate = dto.CollectedDate;
            blood.ExpiryDate = dto.ExpiryDate;
            blood.IsAvailable = true;
            blood.Status = BloodSeparationStatus.UNPROCESSED;


            var updated = await _bloodRepo.UpdateAsync(blood);
            await _unitOfWork.SaveChangeAsync();

          

            return new ResponseDTO("Blood updated successfully.", 200, true);
        }

        public async Task<ResponseDTO> ChangeStatus (Guid id, BloodSeparationStatus status)
        {
            var blood = await _bloodRepo.GetBloodByIdAsync(id);
            if (blood == null)
                return new ResponseDTO("Blood not found.", 404, false, null);
            blood.Status = status;
            var updated = await _bloodRepo.UpdateAsync(blood);
            await _unitOfWork.SaveChangeAsync();
            return new ResponseDTO("Blood status updated successfully.", 200, true);
        }

        //public async Task<ResponseDTO> DeleteBloodAsync(Guid id)
        //{
        //    var deleted = await _bloodRepo.DeleteAsync(id);
        //    if (!deleted)
        //        return new ResponseDTO("Delete failed. Blood not found.", 404, false, null);

        //    await _unitOfWork.SaveChangesAsync();
        //    return new ResponseDTO("Blood deleted successfully.", 200, true, null);
        //}
        private async Task<string> GenerateNewBloodCodeAsync()
        {
            var latestBlood = await _unitOfWork.BloodRepo.GetAll()
                .OrderByDescending(b => b.Code)
                .FirstOrDefaultAsync();

            int latestNumber = 0;
            if (latestBlood != null && !string.IsNullOrEmpty(latestBlood.Code))
            {
                int.TryParse(latestBlood.Code.Substring(1), out latestNumber); // B00001 -> 00001
            }

            return $"B{(latestNumber + 1).ToString("D5")}";
        }
    }
}
