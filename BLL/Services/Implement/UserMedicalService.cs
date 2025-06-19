using BLL.Services.Interface;
using BLL.Utilities;
using Common.DTO;
using DAL.Models;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserUtility _userUtility;

        public async Task<ResponseDTO> CreateUserMedical(CreateUserMediCalDTO dto)
        {
            var userId = _userUtility.GetUserIDFromToken();
            if (userId == null)
            {
                return new ResponseDTO("Không tìm thấy user", 400, false);
            }

            // VALIDATE
            if (string.IsNullOrWhiteSpace(dto.FullName))
                return new ResponseDTO("Họ và tên không được để trống", 400, false);

            if (dto.DateOfBirth >= DateTime.Now)
                return new ResponseDTO("Ngày sinh không hợp lệ", 400, false);

            if (string.IsNullOrWhiteSpace(dto.CitizenId))
                return new ResponseDTO("CMND/CCCD không được để trống", 400, false);

            if (string.IsNullOrWhiteSpace(dto.PhoneNumber))
                return new ResponseDTO("Số điện thoại không được để trống", 400, false);

            if (string.IsNullOrWhiteSpace(dto.Email))
                return new ResponseDTO("Email không được để trống", 400, false);

            if (!IsValidEmail(dto.Email))
                return new ResponseDTO("Email không hợp lệ", 400, false);

            if (string.IsNullOrWhiteSpace(dto.Province))
                return new ResponseDTO("Tỉnh/Thành phố không được để trống", 400, false);

            if (string.IsNullOrWhiteSpace(dto.CurrentAddress))
                return new ResponseDTO("Địa chỉ hiện tại không được để trống", 400, false);

            if (string.IsNullOrWhiteSpace(dto.BloodName))
                return new ResponseDTO("Nhóm máu không được để trống", 400, false);

            if (dto.HasDonatedBefore && (!dto.DonationCount.HasValue || dto.DonationCount.Value <= 0))
                return new ResponseDTO("Số lần hiến máu phải lớn hơn 0 nếu đã từng hiến", 400, false);

            // Tạo mới Blood luôn
            var newBlood = new Blood
            {
                BloodId = Guid.NewGuid(),
                BloodName = dto.BloodName,
                IsAvailable = false,
                CollectedDate = null,
                ExpiryDate = null,
                VolumeInML = null,
                ComponentType = Common.Enum.BloodComponentType.IN_PROGESS // hoặc bạn để mặc định
            };

            await _unitOfWork.BloodRepo.AddAsync(newBlood);

            var userMedical = new UserMedical
            {
                UserMedicalId = Guid.NewGuid(),
                FullName = dto.FullName,
                DateOfBirth = dto.DateOfBirth,
                Gender = dto.Gender,
                CitizenId = dto.CitizenId,
                PhoneNumber = dto.PhoneNumber,
                Email = dto.Email,
                Province = dto.Province,
                CurrentAddress = dto.CurrentAddress,
                HasDonatedBefore = dto.HasDonatedBefore,
                DonationCount = dto.DonationCount,
                DiseaseDescription = dto.DiseaseDescription,
                Type = Common.Enum.MedicalType.PENDING,
                CreateDate = DateTime.Now,
                BloodId = newBlood.BloodId,
                UserId = userId,
            };

            await _unitOfWork.UserMedicalRepo.AddAsync(userMedical);
            await _unitOfWork.SaveAsync();

            return new ResponseDTO("Tạo hồ sơ y tế thành công", 200, true);
        }



        public async Task<ResponseDTO> GetAllUserMedical()
        {
            var list = _unitOfWork.UserMedicalRepo.GetAll();
            if (list == null)
            {
                return new ResponseDTO("not found", 400, false);
            }

            var listDTO = list.Select(u => new UserMedicalDTO
            {
                UserMedicalId = u.UserMedicalId,
                UserId = u.UserId,
                BloodName = u.Blood.BloodName,
                CitizenId = u.CitizenId,
                CurrentAddress = u.CurrentAddress,
                DateOfBirth = u.DateOfBirth,
                DiseaseDescription = u.DiseaseDescription,
                DonationCount = u.DonationCount,
                Email = u.Email,
                FullName = u.FullName,
                Gender = u.Gender,
                HasDonatedBefore = u.HasDonatedBefore,
                PhoneNumber = u.PhoneNumber,
                Province = u.Province,                              
            });
            return new ResponseDTO("get list successfully", 200, true, listDTO);
        }
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

    }
}
