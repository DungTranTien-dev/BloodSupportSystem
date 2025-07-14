using Common.DTO;
using Common.Enum;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interface
{
    public interface IBloodService
    {
        //Task<Blood> CreateBloodAsync(CreateBloodDTO dto);
        Task<ResponseDTO> CreateBloodAsync(CreateBloodDTO dto);
        Task<ResponseDTO> GetBloodByIdAsync(Guid id);
        Task<ResponseDTO> GetAllBloodsAsync();
        Task<ResponseDTO> UpdateBloodAsync(Guid id, UpdateBloodDTO dto);
        Task<ResponseDTO> ChangeStatus(Guid id, BloodSeparationStatus status);
        //Task<bool> DeleteBloodAsync(Guid id);

    }
}
