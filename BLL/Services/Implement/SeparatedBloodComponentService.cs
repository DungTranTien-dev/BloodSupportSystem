using BLL.Services.Interface;
using Common.DTO;
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
                CreatedDate = dto.CreatedDate,
                ExpiryDate = dto.ExpiryDate,
                IsAvailable = dto.IsAvailable,
                Code = await GenerateNewSeparatedCodeAsync()
            };

            await _unitOfWork.SeparatedBloodComponentRepo.AddAsync(newComponent);
            await _unitOfWork.SaveChangeAsync();

            return new ResponseDTO("Separated blood component created successfully.", 200, true);
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
            var components = _unitOfWork.SeparatedBloodComponentRepo.GetAll();

            var result = components.Select(c => new
            {
                c.SeparatedBloodComponentId,
                c.BloodId,
                c.ComponentType,
                c.VolumeInML,
                c.CreatedDate,
                c.ExpiryDate,
                c.IsAvailable
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

    }
}
