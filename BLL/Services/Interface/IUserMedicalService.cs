using Common.DTO;
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

    }
}
