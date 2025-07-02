using BLL.Services.Interface;
using BLL.Utilities;
using Common.DTO;
using Common.Enum;
using DAL.Models;
using DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Linq;
using System.Threading.Tasks;
using VNPAY.NET.Enums;
using VNPAY.NET.Models;
using TransactionStatus = Common.Enum.TransactionStatus;

namespace BLL.Services.Implement
{
    public class BloodRequestService : IBloodRequestService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserUtility _userUtility;
        private readonly ISeparatedBloodComponentService _bloodService;
        private readonly IVnPayService _vnPayService;
        private readonly IEmailService _emailService;

        public BloodRequestService(IUnitOfWork unitOfWork, UserUtility userUtility, ISeparatedBloodComponentService bloodService, IVnPayService vnPayService, IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _userUtility = userUtility;
            _bloodService = bloodService;
            _vnPayService = vnPayService;
            _emailService = emailService;

        }

        public async Task<ResponseDTO> CreateBloodRequestAsync(CreateBloodRequestDTO dto)
        {
            var userId = _userUtility.GetUserIdFromToken();
            var role = _userUtility.GetRoleFromToken();

            if (userId == Guid.Empty)
                return new ResponseDTO("Không tìm thấy user", 400, false);

            var user = await _unitOfWork.UserRepo.GetByIdAsync(userId);
            if (user == null)
                return new ResponseDTO("Không tìm thấy người dùng", 404, false);

            if (!Enum.TryParse(dto.ComponentType, true, out BloodComponentType componentType))
                return new ResponseDTO("Loại thành phần máu không hợp lệ", 400, false);

            var request = new BloodRequest
            {
                RequestedByUserId = userId,
                BloodRequestId = Guid.NewGuid(),
                PatientName = dto.PatientName,
                HospitalName = dto.HospitalName,
                BloodGroup = dto.BloodGroup,
                ComponentType = componentType,
                VolumeInML = dto.VolumeInML,
                Reason = dto.Reason,
                RequestedDate = DateTime.UtcNow
            };

            if (role == "HOSPITAL")
            {
                // Kiểm tra kho máu
                bool hasStock = await _bloodService.HasSufficientAvailableBloodComponentAsync(
                    dto.BloodGroup, componentType, dto.VolumeInML);

                if (!hasStock)
                    return new ResponseDTO("Không đủ thành phần máu", 400, false);

                // Gán trạng thái chờ thanh toán
                request.Status = BloodRequestStatus.WAITING_PAYMENT;

                // Tính tiền theo loại thành phần
                long finalAmount = CalculateBloodPrice(componentType, dto.VolumeInML);

                var ipAddress = "127.0.0.1"; // Hoặc lấy từ HttpContext.Connection.RemoteIpAddress

                var transaction = new Transaction
                {
                    TransactionId = Guid.NewGuid(),
                    UserId = userId,
                    BloodRequestId = request.BloodRequestId,
                    Amount = finalAmount,
                    TransactionDate = DateTime.UtcNow,
                    Status = TransactionStatus.PENDING,
                    TransactionCode = request.BloodRequestId.ToString("N") // Sử dụng ID đơn làm mã giao dịch

                };

                var paymentRequest = new PaymentRequest
                {
                    PaymentId = DateTime.UtcNow.Ticks,
                    Money = finalAmount,
                    Description = $"{request.BloodRequestId}/{transaction.TransactionId}",
                    IpAddress = ipAddress,
                    BankCode = BankCode.ANY,
                    CreatedDate = DateTime.UtcNow,
                    Currency = Currency.VND,
                    Language = DisplayLanguage.Vietnamese
                };



                // Tạo URL thanh toán
                string paymentUrl = await _vnPayService.CreatePaymentUrlAsync(paymentRequest);

                // Gửi email với thông tin đơn và URL thanh toán
                await _emailService.SendEmailBloodRequestAsync(request,user.Email, paymentUrl);

                // Lưu đơn vào DB trước khi return
                await _unitOfWork.TransactionRepo.AddAsync(transaction);    
                await _unitOfWork.BloodRequestRepo.AddAsync(request);
                await _unitOfWork.SaveChangeAsync();

                return new ResponseDTO("Tạo yêu cầu máu thành công, vui lòng kiểm tra email để thanh toán", 200, true);
            }
            else if (role == "CUSTOMER")
            {
                request.Status = BloodRequestStatus.PENDING;
            }
            else
            {
                return new ResponseDTO("Vai trò không hợp lệ", 403, false);
            }

            // Lưu đơn nếu là customer
            await _unitOfWork.BloodRequestRepo.AddAsync(request);
            await _unitOfWork.SaveChangeAsync();

            return new ResponseDTO("Tạo yêu cầu máu thành công", 200, true);
        }




        public async Task<ResponseDTO> UpdateBloodRequestAsync(UpdateBloodRequestDTO dto)
        {
            var existing = await _unitOfWork.BloodRequestRepo.GetByIdAsync(dto.BloodRequestId);
            if (existing == null)
                return new ResponseDTO("Blood request not found", 404, false);

            
            existing.PatientName = dto.PatientName;
            existing.HospitalName = dto.HospitalName;
            existing.BloodGroup = dto.BloodGroup;
            existing.ComponentType = dto.ComponentType;
            existing.VolumeInML = dto.VolumeInML;
            existing.Reason = dto.Reason;
            existing.RequestedDate = dto.RequestedDate;
            existing.Status = dto.Status;

            await _unitOfWork.BloodRequestRepo.UpdateAsync(existing);
            await _unitOfWork.SaveChangeAsync();

            return new ResponseDTO("Blood request updated successfully", 200, true);
        }

        public async Task<ResponseDTO> DeleteBloodRequestAsync(Guid bloodRequestId)
        {
            var request = await _unitOfWork.BloodRequestRepo.GetByIdAsync(bloodRequestId);
            if (request == null)
                return new ResponseDTO("Blood request not found", 404, false);

            await _unitOfWork.BloodRequestRepo.DeleteAsync(bloodRequestId);
            await _unitOfWork.SaveChangeAsync();

            return new ResponseDTO("Blood request deleted successfully", 200, true);
        }

        public async Task<ResponseDTO> GetAllBloodRequestsAsync()
        {
            var list = _unitOfWork.BloodRequestRepo.GetAll();

            if (list == null || !list.Any())
                return new ResponseDTO("No blood requests found", 404, false);

            var result = list.Select(x => new
            {
                x.BloodRequestId,
                x.PatientName,
                x.HospitalName,
                x.BloodGroup,
                x.ComponentType,
                x.VolumeInML,
                x.Reason,
                x.RequestedDate,
                x.Status
            });

            return new ResponseDTO("List retrieved successfully", 200, true, result);
        }

        public async Task<ResponseDTO> GetBloodRequestByIdAsync(Guid bloodRequestId)
        {
            var request = await _unitOfWork.BloodRequestRepo.GetByIdAsync(bloodRequestId);
            if (request == null)
                return new ResponseDTO("Blood request not found", 404, false);

            var result = new
            {
                request.BloodRequestId,
                request.PatientName,
                request.HospitalName,
                request.BloodGroup,
                request.ComponentType,
                request.VolumeInML,
                request.Reason,
                request.RequestedDate,
                request.Status
            };

            return new ResponseDTO("Blood request retrieved successfully", 200, true, result);
        }

        private long CalculateBloodPrice(BloodComponentType componentType, double volumeInML)
        {
            decimal unitPrice = componentType switch
            {
                BloodComponentType.WHOLE_BLOOD => 500m,
                BloodComponentType.RED_BLOOD_CELL => 700m,
                BloodComponentType.PLASMA => 300m,
                BloodComponentType.PLATELET => 900m,
                _ => throw new ArgumentOutOfRangeException(nameof(componentType), "Loại thành phần máu không hợp lệ")
            };

            decimal totalPrice = unitPrice * (decimal)volumeInML;

            return (long)Math.Round(totalPrice, MidpointRounding.AwayFromZero);
        }


    }
}
