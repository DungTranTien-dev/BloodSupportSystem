using BLL.DTOs;
using BLL.Services.Interface;
using Common.Enum;
using DAL.Models;
using DAL.Repositories.Interface;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BCrypt.Net;

namespace BLL.Services.Implement
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IBloodTypeRepository _bloodTypeRepository;
        private readonly ILocationRepository _locationRepository;

        public UserService(
            IUserRepository userRepository,
            IBloodTypeRepository bloodTypeRepository,
            ILocationRepository locationRepository)
        {
            _userRepository = userRepository;
            _bloodTypeRepository = bloodTypeRepository;
            _locationRepository = locationRepository;
        }

        public async Task<User> CreateUserAsync(UserCreateDto dto)
        {
            // Validate email format
            if (!IsValidEmail(dto.Email))
                throw new ValidationException("Invalid email format");

            // Check for duplicate email
            if (await _userRepository.EmailExistsAsync(dto.Email))
                throw new ValidationException("Email already registered");

            // Validate password strength (using PasswordHash as plain password)
            if (string.IsNullOrWhiteSpace(dto.PasswordHash) || dto.PasswordHash.Length < 8)
                throw new ValidationException("Password must be at least 8 characters");

            // Get blood type
            var bloodType = await _bloodTypeRepository.GetByNameAsync(dto.BloodType);
            if (bloodType == null)
                throw new ValidationException("Invalid blood type");

            // Create or get existing location
            var location = await _locationRepository.GetOrCreateAsync(dto.Address);

            // Hash password
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.PasswordHash, BCrypt.Net.BCrypt.GenerateSalt(12));

            // Create user entity
            var user = new User
            {
                Id = Guid.NewGuid(),
                FullName = dto.FullName,
                Email = dto.Email,
                PasswordHash = passwordHash, // Store the hashed password
                PhoneNumber = dto.PhoneNumber,
                Role = UserRole.Member, // Automatically assign Member role
                BloodTypeId = bloodType.Id,
                LocationId = location.Id,
                CreatedAt = DateTime.UtcNow,
                LastDonationDate = DateTime.MinValue // or set as needed
            };

            // Save to database
            await _userRepository.CreateAsync(user);

            return user;
        }

        private bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                RegexOptions.IgnoreCase);
        }
    }
}