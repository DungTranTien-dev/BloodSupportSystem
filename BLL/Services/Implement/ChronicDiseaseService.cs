using BLL.Services.Interface;
using Common.DTO;
using DAL.Repositories.Interface;
using DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Implement
{
    public class ChronicDiseaseService : IChronicDiseaseService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ChronicDiseaseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ResponseDTO> GetAllChronicDisease()
        {
            var list = _unitOfWork.ChronicDiseaseRepo.GetAll();
            if(list == null)
            {
                return new ResponseDTO("not found", 400, false);
            }

            var listDTO = list.Select(c => new ChronicDiseaseDTO
            {
                ChronicDiseaseId = c.ChronicDiseaseId,
                ChronicDiseaseName = c.ChronicDiseaseName,
                
            });
            return new ResponseDTO("get list successfully", 200, true, listDTO);
        }
    }
}
