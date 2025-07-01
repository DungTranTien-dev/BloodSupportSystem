using Common.DTO;
using Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interface
{
    public interface IUserMedicalService
    {
        Task<ResponseDTO> GetAllUserMedical();
        Task<ResponseDTO> CreateUserMedical(CreateUserMediCalDTO createUserMediCalDTO);
        Task<ResponseDTO> UpdateUserMedical(UpdateUserMedicalDTO updateUserMedicalDTO);

        Task<ResponseDTO> GetNearestAvailableAsync(double latitude, double longitude, double maxDistanceKm = 50);

        Task<ResponseDTO> CheckUserMedical();
        Task<ResponseDTO> ChangeStatus(Guid userMedicalId, MedicalType type);
    }
}
