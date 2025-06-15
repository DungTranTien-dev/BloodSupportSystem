using Common.Enum;
using System;

namespace BLL.DTOs
{
    public class UserCreateDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string BloodType { get; set; }
    }
}