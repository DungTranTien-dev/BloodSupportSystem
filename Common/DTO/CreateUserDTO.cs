using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO
{
    public class CreateUserDTO
    {
        // Thông tin tài khoản
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
}

