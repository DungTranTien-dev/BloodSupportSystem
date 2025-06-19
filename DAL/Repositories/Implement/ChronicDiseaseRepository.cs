using DAL.Data;
using DAL.Models;
using DAL.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Implement
{
    public class ChronicDiseaseRepository : GenericRepository<ChronicDisease>, IChronicDiseaseRepository
    {
        public readonly ApplicationDbContext _context;

        public ChronicDiseaseRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
    

    
}
