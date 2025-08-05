using BLL.Services.Interface;
using Common.DTO;
using Common.Enum;
using DAL.Models;
using DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services.Implement
{
    public class SeparatedBloodComponentService : ISeparatedBloodComponentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SeparatedBloodComponentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseDTO> CreateSeparatedBloodComponentAsync(CreateSeparatedBloodComponentDTO dto)
        {
            var blood = await _unitOfWork.BloodRepo.GetByIdAsync(dto.BloodId);
            if (blood == null)
                return new ResponseDTO("Blood source not found.", 404, false);

            var newComponent = new SeparatedBloodComponent
            {

                SeparatedBloodComponentId = Guid.NewGuid(),
                BloodId = dto.BloodId,
                ComponentType = dto.ComponentType,
                VolumeInML = dto.VolumeInML,
                CreatedDate = DateTime.UtcNow,
                ExpiryDate = dto.ExpiryDate,
                IsAvailable = true,
                Code = await GenerateNewSeparatedCodeAsync()
            };

            await _unitOfWork.SeparatedBloodComponentRepo.AddAsync(newComponent);
            await _unitOfWork.SaveChangeAsync();

            return new ResponseDTO("Separated blood component created successfully.", 200, true);
        }

        public async Task<ResponseDTO> AutoSeparateBloodComponentAsync(Guid bloodId)
        {
            var blood = await _unitOfWork.BloodRepo.GetByIdAsync(bloodId);
            if (blood == null)
                return new ResponseDTO("Blood source not found.", 404, false);

            // Kiểm tra đã tách chưa
            var existing = await _unitOfWork.SeparatedBloodComponentRepo
                .GetAllAsync(c => c.BloodId == bloodId);
            if (existing.Any())
                return new ResponseDTO("This blood has already been separated.", 400, false);

            // Áp dụng công thức phân tách
            double totalVolume = (double)blood.VolumeInML; // VD: 500ml
            double rbcVolume = Math.Round(totalVolume * 0.55, 2);
            double plasmaVolume = Math.Round(totalVolume * 0.40, 2);
            double plateletVolume = Math.Round(totalVolume * 0.05, 2);
            var now = DateTime.UtcNow;

            var components = new List<SeparatedBloodComponent>
    {
        new SeparatedBloodComponent
        {
            SeparatedBloodComponentId = Guid.NewGuid(),
            BloodId = bloodId,
            ComponentType = BloodComponentType.RED_BLOOD_CELL,
            VolumeInML = rbcVolume,
            CreatedDate = now,
            ExpiryDate = now.AddDays(42),
            IsAvailable = true,
            Code = await GenerateNewSeparatedCodeAsync()
        },
        new SeparatedBloodComponent
        {
            SeparatedBloodComponentId = Guid.NewGuid(),
            BloodId = bloodId,
            ComponentType = BloodComponentType.PLASMA,
            VolumeInML = plasmaVolume,
            CreatedDate = now,
            ExpiryDate = now.AddMonths(12),
            IsAvailable = true,
            Code = await GenerateNewSeparatedCodeAsync()
        },
        new SeparatedBloodComponent
        {
            SeparatedBloodComponentId = Guid.NewGuid(),
            BloodId = bloodId,
            ComponentType = BloodComponentType.PLATELET,
            VolumeInML = plateletVolume,
            CreatedDate = now,
            ExpiryDate = now.AddDays(5),
            IsAvailable = true,
            Code = await GenerateNewSeparatedCodeAsync()
        }
    };

            foreach (var c in components)
                await _unitOfWork.SeparatedBloodComponentRepo.AddAsync(c);

            // Cập nhật trạng thái máu đã được tách
            blood.Status = (BloodSeparationStatus)2;
            await _unitOfWork.BloodRepo.UpdateAsync(blood);

            await _unitOfWork.SaveChangeAsync();

            return new ResponseDTO($"Blood separated into 3 components (RBC: {rbcVolume}ml, Plasma: {plasmaVolume}ml, Platelet: {plateletVolume}ml)", 200, true);
        }

        public async Task<ResponseDTO> DeleteSeparatedBloodComponentAsync(Guid id)
        {
            var component = await _unitOfWork.SeparatedBloodComponentRepo.GetByIdAsync(id);
            if (component == null)
                return new ResponseDTO("Component not found.", 404, false);

            await _unitOfWork.SeparatedBloodComponentRepo.DeleteAsync(id);
            await _unitOfWork.SaveChangeAsync();

            return new ResponseDTO("Component deleted successfully.", 200, true);
        }

        public async Task<ResponseDTO> GetAllSeparatedBloodComponentAsync()
        {
            var components = await _unitOfWork.SeparatedBloodComponentRepo.GetAllWithBloodAsync();

            var result = components.Select(c => new
            {
                SeparatedBloodComponentId = c.SeparatedBloodComponentId,
                BloodId = c.BloodId,
                ComponentType = c.ComponentType,
                VolumeInML = c.VolumeInML,
                CreatedDate = c.CreatedDate,
                ExpiryDate = c.ExpiryDate,
                IsAvailable = c.IsAvailable,
                Code = c.Code,

                Blood = c.Blood != null ? new
                {
                    c.Blood.BloodName,
                    // Thêm các field khác nếu cần
                } : null
            }).ToList();

            return new ResponseDTO("List retrieved successfully.", 200, true, result);
        }


        public async Task<ResponseDTO> GetSeparatedBloodComponentByIdAsync(Guid id)
        {
            var component = await _unitOfWork.SeparatedBloodComponentRepo.GetByIdAsync(id);
            if (component == null)
                return new ResponseDTO("Component not found.", 404, false);

            var result = new
            {
                component.SeparatedBloodComponentId,
                component.BloodId,
                component.ComponentType,
                component.VolumeInML,
                component.CreatedDate,
                component.ExpiryDate,
                component.IsAvailable
            };

            return new ResponseDTO("Component retrieved successfully.", 200, true, result);
        }

        public async Task<ResponseDTO> UpdateSeparatedBloodComponentAsync(UpdateSeparatedBloodComponentDTO dto)
        {
            var component = await _unitOfWork.SeparatedBloodComponentRepo.GetByIdAsync(dto.SeparatedBloodComponentId);
            if (component == null)
                return new ResponseDTO("Component not found.", 404, false);

            component.BloodId = dto.BloodId;
            component.ComponentType = dto.ComponentType;
            component.VolumeInML = dto.VolumeInML;
            component.CreatedDate = dto.CreatedDate;
            component.ExpiryDate = dto.ExpiryDate;
            component.IsAvailable = dto.IsAvailable;

            await _unitOfWork.SeparatedBloodComponentRepo.UpdateAsync(component);
            await _unitOfWork.SaveChangeAsync();

            return new ResponseDTO("Component updated successfully.", 200, true);
        }

        private async Task<string> GenerateNewSeparatedCodeAsync()
        {
            var latestSeparated = await _unitOfWork.SeparatedBloodComponentRepo.GetAll()
                .OrderByDescending(s => s.Code)
                .FirstOrDefaultAsync();

            int latestNumber = 0;
            if (latestSeparated != null && !string.IsNullOrEmpty(latestSeparated.Code))
            {
                int.TryParse(latestSeparated.Code.Substring(3), out latestNumber); // SPL00001 -> 00001
            }

            return $"SPL{(latestNumber + 1).ToString("D5")}";
        }


        public async Task<bool> HasSufficientAvailableBloodComponentAsync(string bloodGroup, BloodComponentType componentType, double requiredVolume)
        {
            var now = DateTime.UtcNow;

            var components = await _unitOfWork.SeparatedBloodComponentRepo
                .GetAllAsync(c =>
                    c.ComponentType == componentType &&
                    c.IsAvailable &&
                    (c.ExpiryDate == null || c.ExpiryDate > now) &&
                    c.Blood.BloodName == bloodGroup.ToString()
                );

            var totalVolume = components.Sum(c => c.VolumeInML);

            return totalVolume >= requiredVolume;
        }

        public async Task<bool> SubtractBloodComponentVolumeAsync(string bloodGroup, BloodComponentType componentType, double requiredVolume)
        {
            var now = DateTime.UtcNow;

            var components = await _unitOfWork.SeparatedBloodComponentRepo.GetAllAsync(c =>
                c.ComponentType == componentType &&
                c.IsAvailable &&
                (c.ExpiryDate == null || c.ExpiryDate > now) &&
                c.Blood.BloodName == bloodGroup.ToString()
            );

            var sortedComponents = components.OrderBy(c => c.ExpiryDate).ToList();
            double remaining = requiredVolume;

            foreach (var component in sortedComponents)
            {
                if (remaining <= 0) break;

                if (component.VolumeInML <= remaining)
                {
                    remaining -= component.VolumeInML;
                    component.VolumeInML = 0;
                    component.IsAvailable = false;
                }
                else
                {
                    component.VolumeInML -= remaining;
                    remaining = 0;
                }

                await _unitOfWork.SeparatedBloodComponentRepo.UpdateAsync(component);
            }

            if (remaining > 0)
                return false;

            await _unitOfWork.SaveChangeAsync();
            return true;
        }
    }
}
