using Common.DTO;
using Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interface
{
    public interface  IBloodRegistrationService
    {
        Task<ResponseDTO> CreateByEvenId(Guid eventId);
        Task<ResponseDTO> UpdateStatus(RegisterType type, Guid id);
    }
}
