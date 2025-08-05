using BLL.Services.Interface;
using BLL.Utilities;
using Common.DTO;
using Common.Enum;
using DAL.Models;
using DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;



namespace BLL.Services.Implement
{

    public class UserMedicalService : IUserMedicalService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserUtility _userUtility;
        private readonly IAddressService _addressService;

        public UserMedicalService(IUnitOfWork unitOfWork, UserUtility userUtility, IAddressService addressService)
        {
            _unitOfWork = unitOfWork;
            _userUtility = userUtility;
            _addressService = addressService;
        }

        public async Task<ResponseDTO> CreateUserMedical(CreateUserMediCalDTO dto)
        {
            var userId = _userUtility.GetUserIdFromToken();
            if (userId == Guid.Empty)
                return new ResponseDTO("Không tìm thấy user", 400, false);
            

            // VALIDATE INPUT
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

            //if (string.IsNullOrWhiteSpace(dto.Province))
            //    return new ResponseDTO("Tỉnh/Thành phố không được để trống", 400, false);

            if (string.IsNullOrWhiteSpace(dto.CurrentAddress))
                return new ResponseDTO("Địa chỉ hiện tại không được để trống", 400, false);

            if ( dto.DonationCount.Value < 0)
                return new ResponseDTO("Số lần hiến máu phải lớn hơn 0 nếu đã từng hiến", 400, false);




           

            (double lat, double lon) = await _addressService.GetCoordinatesFromAddress(dto.CurrentAddress);


            // Tạo user medical
            var userMedical = new UserMedical
            {
                UserMedicalId = Guid.NewGuid(),
                FullName = dto.FullName,
                DateOfBirth = dto.DateOfBirth,
                Gender = dto.Gender,
                CitizenId = dto.CitizenId,
                PhoneNumber = dto.PhoneNumber,
                Email = dto.Email,
                //Province = dto.Province,
                CurrentAddress = dto.CurrentAddress,
                HasDonatedBefore = dto.HasDonatedBefore,
                DonationCount = dto.DonationCount,
                DiseaseDescription = dto.DiseaseDescription,
                Type = MedicalType.PENDING,
                CreateDate = DateTime.Now,
                BloodName = dto.BloodName,
                UserId = userId,
                Latitue = lat,
                Longtitue = lon,
                UserMedicalChronicDiseases = new List<UserMedicalChronicDisease>(),
                LastDonorDate = dto.LastDonorDate ?? null,
            };


            // Gán danh sách bệnh mãn tính nếu có
            if (dto.ChronicDiseaseIds != null && dto.ChronicDiseaseIds.Any())
            {
                foreach (var diseaseId in dto.ChronicDiseaseIds)
                {
                    var chronicDisease = await _unitOfWork.ChronicDiseaseRepo.GetByIdAsync(diseaseId);
                    if (chronicDisease != null)
                    {
                        userMedical.UserMedicalChronicDiseases.Add(new UserMedicalChronicDisease
                        {
                            UserMedicalId = userMedical.UserMedicalId,
                            ChronicDiseaseId = diseaseId
                        });
                    }
                }
            }

           

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
                BloodName = u.BloodName,

                CitizenId = u.CitizenId,
                CurrentAddress = u.CurrentAddress,
                DateOfBirth = u.DateOfBirth,
                DiseaseDescription = u.DiseaseDescription,
                DonationCount = u.DonationCount,
                Email = u.Email,
                FullName = u.FullName,
                Gender = u.Gender.ToString(),
                HasDonatedBefore = u.HasDonatedBefore,
                PhoneNumber = u.PhoneNumber,
                Type = u.Type.ToString(),
                Latitue = u.Latitue,
                Longtitue = u.Longtitue,
                //Province = u.Province,
                LastDonorDate = u.LastDonorDate ?? null,
                CreateDate = u.CreateDate
            });
            return new ResponseDTO("get list successfully", 200, true, listDTO);
        }

        public async Task<ResponseDTO> UpdateUserMedical(UpdateUserMedicalDTO updateUserMedicalDTO)
        {
            var updateUserMedical = await _unitOfWork.UserMedicalRepo.GetByIdAsync(updateUserMedicalDTO.UserMedicalId);

            if (updateUserMedical == null)
            {
                return new ResponseDTO("not found id", 400, false);
            }

            // VALIDATE
            if (string.IsNullOrWhiteSpace(updateUserMedicalDTO.FullName))
                return new ResponseDTO("Họ và tên không được để trống", 400, false);

            if (updateUserMedicalDTO.DateOfBirth >= DateTime.Now)
                return new ResponseDTO("Ngày sinh không hợp lệ", 400, false);

            if (string.IsNullOrWhiteSpace(updateUserMedicalDTO.CitizenId))
                return new ResponseDTO("CMND/CCCD không được để trống", 400, false);

            if (string.IsNullOrWhiteSpace(updateUserMedicalDTO.PhoneNumber))
                return new ResponseDTO("Số điện thoại không được để trống", 400, false);

            if (string.IsNullOrWhiteSpace(updateUserMedicalDTO.Email))
                return new ResponseDTO("Email không được để trống", 400, false);

            if (!IsValidEmail(updateUserMedicalDTO.Email))
                return new ResponseDTO("Email không hợp lệ", 400, false);

            //if (string.IsNullOrWhiteSpace(updateUserMedicalDTO.Province))
            //    return new ResponseDTO("Tỉnh/Thành phố không được để trống", 400, false);

            if (string.IsNullOrWhiteSpace(updateUserMedicalDTO.CurrentAddress))
                return new ResponseDTO("Địa chỉ hiện tại không được để trống", 400, false);

            if (updateUserMedicalDTO.HasDonatedBefore && (!updateUserMedicalDTO.DonationCount.HasValue || updateUserMedicalDTO.DonationCount.Value <= 0))
                return new ResponseDTO("Số lần hiến máu phải lớn hơn 0 nếu đã từng hiến", 400, false);

            
            updateUserMedical.UserMedicalId = updateUserMedicalDTO.UserMedicalId;
            updateUserMedical.FullName = updateUserMedicalDTO.FullName;
            updateUserMedical.DateOfBirth = updateUserMedicalDTO.DateOfBirth;
            updateUserMedical.Gender = updateUserMedicalDTO.Gender;
            updateUserMedical.CitizenId = updateUserMedicalDTO.CitizenId;

            //updateUserMedical.BloodId = updateUserMedicalDTO.BloodId;

            updateUserMedical.PhoneNumber = updateUserMedicalDTO.PhoneNumber;
            updateUserMedical.Email = updateUserMedicalDTO.Email;
            //updateUserMedical.Province = updateUserMedicalDTO.Province;
            updateUserMedical.CurrentAddress = updateUserMedicalDTO.CurrentAddress;

            updateUserMedical.HasDonatedBefore = updateUserMedicalDTO.HasDonatedBefore;
            updateUserMedical.DonationCount = updateUserMedicalDTO.DonationCount;

            updateUserMedical.DiseaseDescription = updateUserMedicalDTO.DiseaseDescription;
            updateUserMedical.Type = updateUserMedicalDTO.Type;

            updateUserMedical.CreateDate = updateUserMedicalDTO.CreateDate;

            updateUserMedical.UserId = updateUserMedicalDTO.UserId;
            updateUserMedical.LastDonorDate = updateUserMedicalDTO.HasDonatedBefore ? updateUserMedicalDTO.LastDonorDate : null;



            //var updateBlood = await _unitOfWork.BloodRepo.GetByIdAsync(updateUserMedicalDTO.BloodId);

            //if (updateBlood == null)
            //{
            //    return new ResponseDTO("not found id", 400, false);
            //}
            //updateBlood.BloodName = updateUserMedicalDTO.BloodName;
            //updateBlood.VolumeInML = updateUserMedicalDTO.VolumeInML;

            //updateBlood.CollectedDate = updateUserMedicalDTO.CollectedDate;
            //updateBlood.ExpiryDate = updateUserMedicalDTO.ExpiryDate;
            //updateBlood.IsAvailable = updateUserMedicalDTO.IsAvailable;

            try
            {
                await _unitOfWork.UserMedicalRepo.UpdateAsync(updateUserMedical);
                //await _unitOfWork.BloodRepo.UpdateAsync(updateBlood);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                return new ResponseDTO("loi khi update" + ex, 500, false);
            }

            return new ResponseDTO("Update thanh cong", 200, true);
        }

        public async Task<ResponseDTO> GetNearestAvailableAsync(double latitude, double longitude, double maxDistanceKm = 50)
        {
            try
            {
                // 1. Lấy danh sách UserMedical có máu sẵn
                var availableUsers = await _unitOfWork.UserMedicalRepo.GetAllAvailableWithBloodAsync();

                

                var nearestList = availableUsers
                    .Select(um => new
                    {
                        UserMedical = um,
                        Distance = CalculateDistanceKm(latitude, longitude, um.Latitue, um.Longtitue)
                    })
                    .Where(x => x.Distance <= maxDistanceKm)
                    .OrderBy(x => x.Distance)
                    .Select(x => x.UserMedical)
                    .ToList();

                if (nearestList == null)
                {
                    return new ResponseDTO("Khong co ai o gan ca", 400, false);
                }

                // ✅ Check nếu không có kết quả
                if (nearestList.Count == 0)
                {
                    return new ResponseDTO("Không có ai ở gần cả", 404, false);
                }

                // 3. Chuyển sang DTO
                var listDTO = nearestList.Select(u => new UserMedicalDTO
                {
                    UserMedicalId = u.UserMedicalId,
                    UserId = u.UserId,
                    BloodName = u.BloodName,
                    CitizenId = u.CitizenId,
                    CurrentAddress = u.CurrentAddress,
                    DateOfBirth = u.DateOfBirth,
                    DiseaseDescription = u.DiseaseDescription,
                    DonationCount = u.DonationCount,
                    Email = u.Email,
                    FullName = u.FullName,
                    Gender = u.Gender.ToString(),
                    HasDonatedBefore = u.HasDonatedBefore,
                    PhoneNumber = u.PhoneNumber,
                    //Province = u.Province,
                    Latitue = u.Latitue,
                    Longtitue = u.Longtitue,
                    Type = u.Type.ToString(),
                    LastDonorDate = u.LastDonorDate
                }).ToList();

                // 4. Trả về ResponseDTO
                return new ResponseDTO("get list successfully", 200, true, listDTO);
            }
            catch (Exception ex)
            {
                return new ResponseDTO($"Lỗi khi truy xuất: {ex.Message}", 500, false);
            }
        }

        public async Task<ResponseDTO> CheckUserMedical()
        {
            var userId = _userUtility.GetUserIdFromToken();
            if (userId == Guid.Empty)
            {
                return new ResponseDTO("Khong thay user", 400, false);
            }

            var userMedicalId = await _unitOfWork.UserMedicalRepo.GetUserMedicalIdAsync(userId);

            //var user = await _unitOfWork.UserRepo.GetByIdAsync(userId);

            //var medicalId = _unitOfWork.UserMedicalRepo.GetByIdAsync(user.)

            if (userMedicalId == Guid.Empty || userMedicalId == null )
            {
                return new ResponseDTO("khong co ho so", 400, false);
            }

            var medical = await _unitOfWork.UserMedicalRepo.GetByIdAsync(userMedicalId.Value);

            medical.Type = MedicalType.PENDING;
            


            try
            {
                await _unitOfWork.UserMedicalRepo.UpdateAsync(medical);
                
                await _unitOfWork.SaveChangeAsync();

            } catch (Exception ex)
            {
                return new ResponseDTO("Error" + ex, 500, false);
            }

            return new ResponseDTO("co ho so", 200, true);

        }

        public async Task<ResponseDTO> ChangeStatus(Guid userMedicalId, MedicalType type)
        {
            var userMedical = await _unitOfWork.UserMedicalRepo.GetByIdAsync(userMedicalId);
            if (userMedical == null)
            {
                return new ResponseDTO("Hồ sơ y tế không tồn tại", 404, false);
            }
            userMedical.Type = type;
            try
            {
                await _unitOfWork.UserMedicalRepo.UpdateAsync(userMedical);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                return new ResponseDTO($"Lỗi khi cập nhật trạng thái: {ex.Message}", 500, false);
            }
            return new ResponseDTO("Cập nhật trạng thái thành công", 200, true);
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


        private double CalculateDistanceKm(double lat1, double lon1, double lat2, double lon2)
        {
            double R = 6371; // Bán kính trái đất (km)
            var dLat = DegreesToRadians(lat2 - lat1);
            var dLon = DegreesToRadians(lon2 - lon1);

            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(DegreesToRadians(lat1)) * Math.Cos(DegreesToRadians(lat2)) *
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return R * c;
        }

        private double DegreesToRadians(double deg)
        {
            return deg * (Math.PI / 180);
        }

    }
}
