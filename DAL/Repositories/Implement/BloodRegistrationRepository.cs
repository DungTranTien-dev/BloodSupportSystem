using Common.DTO;
using DAL.Data;
using DAL.Models;
using DAL.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Implement
{
    public class BloodRegistrationRepository : GenericRepository<BloodRegistration>, IBloodRegistrationRepository
    {
        private readonly ApplicationDbContext _context;

        public BloodRegistrationRepository (ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<RegistrationByUserIdDTO>> GetByUserIdAsync(Guid userId)
        {
            var result = await _context.BloodRegistrations
                .Where(r => r.UserId == userId)
                .Include(r => r.DonationEvent)
                .Select(r => new RegistrationByUserIdDTO
                {
                    BloodRegistrationId = r.BloodRegistrationId,
                    CreateDate = r.CreateDate,
                    RegisterType = r.Type.ToString(),

                    DonationEventId = r.DonationEvent.DonationEventId,
                    EventTitle = r.DonationEvent.Title,
                    EventLocation = r.DonationEvent.Location,
                    StartTime = r.DonationEvent.StartTime,
                    EndTime = r.DonationEvent.EndTime
                })
                .ToListAsync();

            return result;
        }

        public async Task<List<BloodRegistration>> GetAllRegistration()
        {
            return await _context.BloodRegistrations
                .Include(r => r.DonationEvent) // luôn include DonationEvent
                .ToListAsync();
        }


    }
}
