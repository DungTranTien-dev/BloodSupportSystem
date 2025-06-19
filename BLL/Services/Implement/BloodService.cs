using AutoMapper;
using BLL.Services.Interface;
using Common.DTO;
using DAL.Models;
using DAL.Repositories.Interface;
using DAL.Repositories.Interfaces;
using DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Implement
{
    public class BloodService : IBloodService
    {
        private readonly IBloodRepository _bloodRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public BloodService(
            IBloodRepository bloodRepo,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _bloodRepo = bloodRepo;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        //public async Task<Blood> CreateBloodAsync(CreateBloodDTO dto)
        //{
        //    // Create blood entity without requiring user medical record
        //    var blood = new Blood
        //    {
        //        BloodId = Guid.NewGuid(),
        //        BloodName = dto.BloodName,
        //        ComponentType = dto.ComponentType,
        //        VolumeInML = dto.VolumeInML,
        //        CollectedDate = dto.CollectedDate,
        //        ExpiryDate = dto.ExpiryDate,
        //        IsAvailable = dto.IsAvailable
        //    };

        //    // Only attempt to link if userId is provided
        //    if (dto.UserId != null && dto.UserId != Guid.Empty)
        //    {
        //        var userMedical = await _userMedicalRepo.GetByUserIdAsync(dto.UserId.Value);

        //        if (userMedical != null)
        //        {
        //            // Save blood first to generate ID
        //            var createdBlood = await _bloodRepo.CreateBloodAsync(blood);

        //            // Link to medical record
        //            userMedical.BloodId = createdBlood.BloodId;
        //            await _userMedicalRepo.UpdateAsync(userMedical);
        //            return createdBlood;
        //        }
        //    }

        //    // If no userId or medical record not found, create without linking
        //    return await _bloodRepo.CreateBloodAsync(blood);
        //}
        public async Task<BloodResponseDTO> GetBloodByIdAsync(Guid id)
        {
            var blood = await _bloodRepo.GetBloodByIdAsync(id);
            return _mapper.Map<BloodResponseDTO>(blood);
        }

        public async Task<IEnumerable<BloodResponseDTO>> GetAllBloodsAsync()
        {
            var bloods = await _bloodRepo.GetAllAsync();
            return bloods.Select(b => _mapper.Map<BloodResponseDTO>(b));
        }

        public async Task<BloodResponseDTO> UpdateBloodAsync(Guid id, UpdateBloodDTO dto)
        {
            var blood = await _bloodRepo.GetBloodByIdAsync(id);
            if (blood == null) return null;

            blood.BloodName = dto.BloodName;
            blood.ComponentType = dto.ComponentType;
            blood.VolumeInML = dto.VolumeInML;
            blood.CollectedDate = dto.CollectedDate;
            blood.ExpiryDate = dto.ExpiryDate;
            blood.IsAvailable = dto.IsAvailable;

            var updatedBlood = await _bloodRepo.UpdateAsync(blood);
            return _mapper.Map<BloodResponseDTO>(updatedBlood);
        }

        public async Task<bool> DeleteBloodAsync(Guid id)
        {
            return await _bloodRepo.DeleteAsync(id);
        }
    }
}
