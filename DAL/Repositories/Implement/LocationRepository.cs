using DAL.Data;
using DAL.Models;
using DAL.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DAL.Repositories.Implement
{
    public class LocationRepository : GenericRepository<Location>, ILocationRepository
    {
        public LocationRepository(ApplicationDbContext context) : base(context) { }

        public async Task<Location> GetOrCreateAsync(string address)
        {
            var location = await _context.Locations
                .FirstOrDefaultAsync(l => l.Address == address);

            if (location == null)
            {
                location = new Location { Address = address };
                await _context.Locations.AddAsync(location);
                await _context.SaveChangesAsync();
            }

            return location;
        }
    }
}