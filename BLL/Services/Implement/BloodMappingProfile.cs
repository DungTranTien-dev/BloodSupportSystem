using AutoMapper;
using Common.DTO;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Implement
{
    public class BloodMappingProfile : Profile
    {
        public BloodMappingProfile()
        {
            CreateMap<Blood, BloodResponseDTO>();
        }
    }
    
}
