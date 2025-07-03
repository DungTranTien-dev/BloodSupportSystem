using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO
{
    public class AuthDTO
    {
        public class LoginDTO
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class RegisterDTO
        {
            public string UserName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string ConfirmPassword { get; set; }

            // Thông tin cá nhân
            public string FullName { get; set; }
            public DateTime? DateOfBirth { get; set; }
            public string Gender { get; set; } // "Male", "Female", "Other"
            public string CitizenId { get; set; }

            // Thông tin liên hệ
            public string PhoneNumber { get; set; }
            public string CurrentAddress { get; set; }
            public Guid BloodId { get; set; }  // Có thể để null
        }

        public class UserInfoDTO
        {
            public Guid UserId { get; set; }
            public string UserName { get; set; }
            public string Email { get; set; }
            public string Role { get; set; }
        }

        public class ChronicDiseaseInfoDTO
        {
            public string DiseaseName { get; set; }
        }

        public class UserMedicalInfoDTO
        {
            public Guid UserMedicalId { get; set; }
            public Guid UserId { get; set; }
            public string FullName { get; set; }
            public DateTime DateOfBirth { get; set; }
            public string Gender { get; set; }
            public string CitizenId { get; set; }
            public string BloodType { get; set; } // Blood name instead of BloodId
            public string PhoneNumber { get; set; }
            public string Email { get; set; }
            public string CurrentAddress { get; set; }
            public bool HasDonatedBefore { get; set; }
            public int? DonationCount { get; set; }
            public string DiseaseDescription { get; set; }
            public string Type { get; set; }
            public DateTime CreateDate { get; set; }
            public double Latitude { get; set; } // Corrected from Latitue
            public double Longitude { get; set; } // Corrected from Longtitue
            public List<ChronicDiseaseInfoDTO> ChronicDiseases { get; set; }
        }

        public class LoginResponseDTO
        {
            public UserInfoDTO User { get; set; }
            public UserMedicalInfoDTO Medical { get; set; }
            public string AccessToken { get; set; }
            public string RefreshToken { get; set; }
        }
    }
}