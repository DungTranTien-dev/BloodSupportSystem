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
    public class SeparatedBloodComponentRepository : GenericRepository<SeparatedBloodComponent>, ISeparatedBloodComponentRepository
    {
        private readonly ApplicationDbContext _context;
        public SeparatedBloodComponentRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
