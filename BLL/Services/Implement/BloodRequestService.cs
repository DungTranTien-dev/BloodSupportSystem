using BLL.Services.Interface;
using Common.DTO;
using Common.Enum;
using DAL.Models;
using DAL.Repositories.Interface;
using DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Implement
{
    public class BloodRequestService : IBloodRequestService
    {
        private readonly IGenericRepository<BloodRequest> _repo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<User> _userRepo;    // để kiểm tra user

        public BloodRequestService(IGenericRepository<BloodRequest> repo, IUnitOfWork unitOfWork, IGenericRepository<User> userRepo)
        {
            _repo = repo;
            _unitOfWork = unitOfWork;
            _userRepo = userRepo;
        }

        public async Task<List<BloodRequestDTO>> GetAllAsync()
        {
            // Lấy tất cả BloodRequest từ repository
            var entities = await _repo.GetAll().ToListAsync();

            // Map sang DTO
            return entities.Select(x => new BloodRequestDTO
            {
                BloodRequestId = x.BloodRequestId,
                PatientName = x.PatientName,
                HospitalName = x.HospitalName,
                BloodGroup = x.BloodGroup,
                ComponentType = x.ComponentType.ToString(),
                VolumeInML = x.VolumeInML,
                Reason = x.Reason,
                RequestedDate = x.RequestedDate,
                Status = x.Status.ToString()
            }).ToList();
        }

        public async Task<List<BloodRequestDTO>> GetByUserAsync(Guid userId)
        {
            var list = await _repo.GetAllByListAsync(x => x.RequestedByUserId == userId);

            return list.Select(x => new BloodRequestDTO
            {
                BloodRequestId = x.BloodRequestId,
                PatientName = x.PatientName,
                HospitalName = x.HospitalName,
                BloodGroup = x.BloodGroup,
                ComponentType = x.ComponentType.ToString(),
                VolumeInML = x.VolumeInML,
                Reason = x.Reason,
                RequestedDate = x.RequestedDate,
                Status = x.Status.ToString()
            }).ToList();
        }

        public async Task<bool> CreateAsync(CreateBloodRequestDTO dto)
        {
            // 1. Validate UserId (dto.RequestedByUserId vẫn được truyền tạm)
            bool userExists = await _userRepo.AnyAsync(u => u.UserId == dto.RequestedByUserId);
            if (!userExists)
                throw new ArgumentException($"UserId '{dto.RequestedByUserId}' không tồn tại.");

            // 2. Parse enum an toàn
            if (!Enum.TryParse<BloodComponentType>(dto.ComponentType, true, out var componentEnum))
                throw new ArgumentException($"ComponentType '{dto.ComponentType}' không hợp lệ.");

            var entity = new BloodRequest
            {
                BloodRequestId = Guid.NewGuid(),
                PatientName = dto.PatientName,
                HospitalName = dto.HospitalName,
                BloodGroup = dto.BloodGroup,
                ComponentType = componentEnum,
                VolumeInML = dto.VolumeInML,
                Reason = dto.Reason,
                RequestedDate = dto.RequestedDate,
                RequestedByUserId = dto.RequestedByUserId,
                Status = BloodRequestStatus.PENDING
            };

            await _repo.AddAsync(entity);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(Guid id, CreateBloodRequestDTO dto)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null || entity.Status != BloodRequestStatus.PENDING) return false;

            entity.PatientName = dto.PatientName;
            entity.HospitalName = dto.HospitalName;
            entity.BloodGroup = dto.BloodGroup;
            entity.ComponentType = Enum.Parse<BloodComponentType>(dto.ComponentType);
            entity.VolumeInML = dto.VolumeInML;
            entity.Reason = dto.Reason;
            entity.RequestedDate = dto.RequestedDate;

            await _repo.UpdateAsync(entity);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null || entity.Status != BloodRequestStatus.PENDING) return false;

            await _repo.DeleteAsync(id);
            await _unitOfWork.SaveAsync();
            return true;
        }
    }
}
