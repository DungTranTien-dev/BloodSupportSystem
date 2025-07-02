using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Services.Interface;
using DAL.UnitOfWork;
using Microsoft.Extensions.Configuration;
using VNPAY.NET.Models;
using VNPAY.NET;
using Common.DTO;
using Common.Enum;
using TransactionStatus = Common.Enum.TransactionStatus;

namespace BLL.Services.Implement
{
    public class VnPayService : IVnPayService
    {
        private readonly IVnpay _vnpay;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;

        public VnPayService(IVnpay vnpay, IConfiguration configuration, IUnitOfWork unitOfWork)
        {
            _vnpay = vnpay; // ✅ Không cần gọi _vnpay.Initialize() nữa vì đã làm trong Program.cs
            _configuration = configuration;
            _unitOfWork = unitOfWork;
        }

        public async Task<string> CreatePaymentUrlAsync(PaymentRequest request)
        {
            return _vnpay.GetPaymentUrl(request);
        }

        public async Task<ResponseDTO> CallBackVNPay(Guid transactionId, Guid bloodRequestId, string status)
        {
            var transaction = await _unitOfWork.TransactionRepo.GetByIdAsync(transactionId);
            if (transaction == null)
            {
                return new ResponseDTO("Không tìm thấy giao dịch.", 404, false);
            }

            var bloodRequest = await _unitOfWork.BloodRequestRepo.GetByIdAsync(bloodRequestId);
            if (bloodRequest == null)
            {
                return new ResponseDTO("Không tìm thấy yêu cầu máu.", 404, false);
            }

            // Convert string status ("SUCCESS" | "FAILED") từ FE sang TransactionStatus enum
            TransactionStatus parsedStatus;
            switch (status?.ToUpper())
            {
                case "SUCCESS":
                    parsedStatus = TransactionStatus.COMPLETED;
                    break;
                case "FAILED":
                    parsedStatus = TransactionStatus.FAILED;
                    break;
                default:
                    return new ResponseDTO("Trạng thái không hợp lệ.", 400, false);
            }

            // Cập nhật trạng thái giao dịch
            transaction.Status = parsedStatus;
            await _unitOfWork.TransactionRepo.UpdateAsync(transaction);

            // Nếu thanh toán thành công, cập nhật trạng thái yêu cầu máu
            if (parsedStatus == TransactionStatus.COMPLETED)
            {
                bloodRequest.Status = BloodRequestStatus.FULFILLED;
                await _unitOfWork.BloodRequestRepo.UpdateAsync(bloodRequest);
            }

            await _unitOfWork.SaveChangeAsync();

            return new ResponseDTO(
                parsedStatus == TransactionStatus.COMPLETED
                    ? "Thanh toán thành công. Đã cập nhật yêu cầu máu."
                    : "Thanh toán thất bại. Trạng thái đã được ghi nhận.",
                200,
                true
            );
        }

    }
}
