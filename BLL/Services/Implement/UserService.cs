using BLL.Services.Interface;
using Common.DTO;
using DAL.Models;
using DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Common.DTO.AuthDTO;

namespace BLL.Services.Implement
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseDTO> CreateUserAsync(CreateUserDTO createUserDTO)
        {
            // Kiểm tra username không được null hoặc rỗng
            if (string.IsNullOrWhiteSpace(createUserDTO.UserName))
            {
                return new ResponseDTO("Username is required.", 400, false);
            }

            // Kiểm tra email không được null hoặc rỗng
            if (string.IsNullOrWhiteSpace(createUserDTO.Email))
            {
                return new ResponseDTO("Email is required.", 400, false);
            }

            // Kiểm tra định dạng email hợp lệ
            if (!IsValidEmail(createUserDTO.Email))
            {
                return new ResponseDTO("Invalid email format.", 400, false);
            }

            // Kiểm tra trùng email
            var existingUser = await _unitOfWork.UserRepo.FindByEmailAsync(createUserDTO.Email);
            if (existingUser != null)
            {


                return new ResponseDTO("Email is already registered.", 400, false);
            }

            // Kiểm tra mật khẩu không được null
            if (string.IsNullOrWhiteSpace(createUserDTO.Password))
            {
                return new ResponseDTO("Password is required.", 400, false);
            }

            
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(createUserDTO.Password);
            // Tạo người dùng mới
            var newUser = new User
            {
                UserId = Guid.NewGuid(),
                UserName = createUserDTO.UserName,
                Email = createUserDTO.Email,
                Password = passwordHash
            };

            await _unitOfWork.UserRepo.AddAsync(newUser);
            await _unitOfWork.SaveChangeAsync();

            return new ResponseDTO("Create user successful.", 200, true);
        }

        public async Task<ResponseDTO> DeleteUserAsync(Guid userId)
        {
            if(userId == null)
            {
                return new ResponseDTO("not found userid.", 400, false);
            }
            var deleteUser = await _unitOfWork.UserRepo.GetByIdAsync(userId);
            if(deleteUser == null)
            {
                return new ResponseDTO("not found user.", 400, false);
            }
            try
            {
                await _unitOfWork.UserRepo.DeleteAsync(userId);
                await _unitOfWork.SaveChangeAsync();
            }
            catch (Exception ex)
            {
                return new ResponseDTO("Error at delete user" + ex, 400, false);
            }

            return new ResponseDTO("Delete user successful.", 200, true);
        }

        public async Task<ResponseDTO> GetAllUserAsync()
        {
            var allUser = _unitOfWork.UserRepo.GetAll();
            if (allUser == null || !allUser.Any())
            {
                return new ResponseDTO("No users found.", 404, false);
            }

            var listUserDTO = allUser.Select(user => new UserDTO
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Email = user.Email
            }).ToList();

            return new ResponseDTO("Users retrieved successfully.", 200, true,listUserDTO);

        }

        public async Task<ResponseDTO> GetUserByIdAsync(Guid userId)
        {
            if (userId == null)
            {
                return new ResponseDTO("not found userid.", 400, false);
            }
            var user =await _unitOfWork.UserRepo.GetByIdAsync(userId);
            var userDTO = new UserDTO
            {
                UserId = user.UserId,
                Email = user.Email,
                UserName = user.UserName,

            };
            return new ResponseDTO("Users retrieved successfully.", 200, true, userDTO);
        }

        public async Task<ResponseDTO> UpdateUserAsync(UpdateUserDTO updateUserDTO)
        {
            if (updateUserDTO.UserId == Guid.Empty)
            {
                return new ResponseDTO("User ID is required.", 400, false);
            }
            var userToUpdate = await _unitOfWork.UserRepo.GetByIdAsync(updateUserDTO.UserId);
            if (userToUpdate == null)
            {
                return new ResponseDTO("User not found.", 404, false);
            }

            // Kiểm tra username không được null hoặc rỗng
            if (string.IsNullOrWhiteSpace(updateUserDTO.UserName))
            {
                return new ResponseDTO("Username is required.", 400, false);
            }

            if (string.IsNullOrWhiteSpace(updateUserDTO.Email))
            {
                return new ResponseDTO("Email is required.", 400, false);
            }

            if (!IsValidEmail(updateUserDTO.Email))
            {
                return new ResponseDTO("Invalid email format.", 400, false);
            }

            if (string.IsNullOrWhiteSpace(updateUserDTO.Password))
            {
                return new ResponseDTO("Password is required.", 400, false);
            }
            // Kiểm tra trùng email
            var existingUserWithEmail = await _unitOfWork.UserRepo.FindByEmailAsync(updateUserDTO.Email);

            if (existingUserWithEmail != null && existingUserWithEmail.UserId != updateUserDTO.UserId)
            {
                return new ResponseDTO("Email is already registered by another user.", 400, false);
            }

            userToUpdate.UserName = updateUserDTO.UserName;
            userToUpdate.Email = updateUserDTO.Email;

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(updateUserDTO.Password);
            userToUpdate.Password = passwordHash;

            try
            {
                await _unitOfWork.UserRepo.UpdateAsync(userToUpdate);
                await _unitOfWork.SaveChangeAsync();
            }
            catch(Exception ex)
            {
                return new ResponseDTO("Error at update user" +  ex , 400, false);
            }

            return new ResponseDTO("Update user successful.", 200, true);

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
