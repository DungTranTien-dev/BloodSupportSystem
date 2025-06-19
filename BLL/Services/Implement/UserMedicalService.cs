using BLL.Services.Interface;
using BLL.Utilities;
using Common.DTO;
using DAL.Models;
using DAL.Repositories.Interface;
using DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Implement
{

    public class UserMedicalService : IUserMedicalService
    {
        private readonly IUserMedicalRepository _repo;

        public UserMedicalService(IUserMedicalRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<UserMedicalDTO>> GetAllAsync()
        {
            var list = await _repo.GetAllAsync();

            return list.Select(x => new UserMedicalDTO
            {
                UserMedicalId = x.UserMedicalId,
                FullName = x.FullName,
                DateOfBirth = x.DateOfBirth,
                Gender = x.Gender,
                CitizenId = x.CitizenId,
                BloodId = x.BloodId,
                PhoneNumber = x.PhoneNumber,
                Email = x.Email,
                Province = x.Province,
                CurrentAddress = x.CurrentAddress,
                HasDonatedBefore = x.HasDonatedBefore,
                DonationCount = x.DonationCount,
                DiseaseDescription = x.DiseaseDescription,
                Type = x.Type,
                CreateDate = x.CreateDate,
                UserId = x.UserId
            });
        }

        public async Task<UserMedicalDTO?> GetByIdAsync(Guid id)
        {
            var x = await _repo.GetByIdAsync(id);
            if (x == null) return null;

            return new UserMedicalDTO
            {
                UserMedicalId = x.UserMedicalId,
                FullName = x.FullName,
                DateOfBirth = x.DateOfBirth,
                Gender = x.Gender,
                CitizenId = x.CitizenId,
                BloodId = x.BloodId,
                PhoneNumber = x.PhoneNumber,
                Email = x.Email,
                Province = x.Province,
                CurrentAddress = x.CurrentAddress,
                HasDonatedBefore = x.HasDonatedBefore,
                DonationCount = x.DonationCount,
                DiseaseDescription = x.DiseaseDescription,
                Type = x.Type,
                CreateDate = x.CreateDate,
                UserId = x.UserId
            };
        }

        public async Task<bool> AddAsync(UserMedicalDTO dto)
        {
            var entity = new UserMedical
            {
                UserMedicalId = Guid.NewGuid(),
                FullName = dto.FullName,
                DateOfBirth = dto.DateOfBirth,
                Gender = dto.Gender,
                CitizenId = dto.CitizenId,
                BloodId = dto.BloodId,
                PhoneNumber = dto.PhoneNumber,
                Email = dto.Email,
                Province = dto.Province,
                CurrentAddress = dto.CurrentAddress,
                HasDonatedBefore = dto.HasDonatedBefore,
                DonationCount = dto.HasDonatedBefore ? dto.DonationCount : 0,
                DiseaseDescription = dto.DiseaseDescription,
                Type = dto.Type,
                CreateDate = DateTime.Now,
                UserId = dto.UserId
            };

            await _repo.AddAsync(entity);
            await _repo.SaveAsync();
            return true;
        }


        public async Task<bool> UpdateAsync(UserMedicalDTO dto)
        {
            var existing = await _repo.GetByIdAsync(dto.UserMedicalId);
            if (existing == null) return false;

            // Update fields
            existing.FullName = dto.FullName;
            existing.DateOfBirth = dto.DateOfBirth;
            existing.Gender = dto.Gender;
            existing.CitizenId = dto.CitizenId;
            existing.BloodId = dto.BloodId;
            existing.PhoneNumber = dto.PhoneNumber;
            existing.Email = dto.Email;
            existing.Province = dto.Province;
            existing.CurrentAddress = dto.CurrentAddress;
            existing.HasDonatedBefore = dto.HasDonatedBefore;
            existing.DonationCount = dto.DonationCount;
            existing.DiseaseDescription = dto.DiseaseDescription;
            existing.Type = dto.Type;
            existing.UserId = dto.UserId;

            _repo.Update(existing);
            await _repo.SaveAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return false;

            _repo.Delete(existing);
            await _repo.SaveAsync();
            return true;
        }
    }
}
