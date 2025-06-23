using System;
using System.ComponentModel.DataAnnotations;

namespace Common.DTO
{
    public class GoogleSignInDTO
    {
        [Required]
        public string GoogleToken { get; set; }
    }

    public class GoogleSignUpCompleteDTO
    {
        [Required]
        public string GoogleToken { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string FullName { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public int Gender { get; set; } // 0 = MALE, 1 = FEMALE, 2 = OTHER

        [Required]
        public string CitizenId { get; set; }

        [Required]
        public string BloodTypeName { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Province { get; set; }

        [Required]
        public string CurrentAddress { get; set; }

        [Required]
        public bool HasDonatedBefore { get; set; }

        public string? DiseaseDescription { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 8)]
        public string Password { get; set; }
    }
}