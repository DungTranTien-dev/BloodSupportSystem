using BLL.Services.Interface;
using Common.DTO;
using DAL.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Implement
{
    public class DonationHistoryService : IDonationHistoryService
    {
        private readonly ApplicationDbContext _context;

        public DonationHistoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<DonationHistoryDto>> GetByUserIdAsync(Guid userId)
        {
            var histories = await _context.DonationHistorys
                .Where(d => d.UserId == userId)
                .OrderByDescending(d => d.CreateAt)
                .ToListAsync();

            return histories.Select(d => new DonationHistoryDto
            {
                DonationHistoryId = d.DonationHistoryId,
                BloodName = d.BloodName,
                CreateAt = d.CreateAt,
                Status = d.Status.ToString()
            }).ToList();
        }
    }

}
