using Common.DTO;
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
        Task<BloodResponseDTO> GetBloodByIdAsync(Guid id);
        Task<IEnumerable<BloodResponseDTO>> GetAllBloodsAsync();
        Task<BloodResponseDTO> UpdateBloodAsync(Guid id, UpdateBloodDTO dto);
        Task<bool> DeleteBloodAsync(Guid id);

    }
}
