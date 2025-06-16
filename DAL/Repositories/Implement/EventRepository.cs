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
    public class EventRepository : GenericRepository<DonationEvent>, IEventRepository
    {
        public readonly ApplicationDbContext _context;

        public EventRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        
    }
}
