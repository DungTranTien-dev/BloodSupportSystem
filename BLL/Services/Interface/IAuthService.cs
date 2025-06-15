using Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Common.DTO.AuthDTO;

namespace BLL.Services.Interface
{
    public interface IAuthService
    {
        Task<ResponseDTO> Login(LoginDTO loginDTO);

        Task<ResponseDTO> GoogleSignInAsync(GoogleSignInDTO dto);
        Task<ResponseDTO> CompleteGoogleSignUpAsync(GoogleSignUpCompleteDTO dto);
    }
    //ADD GOOGLE LOG IN 
}
