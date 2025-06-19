using DAL.Data;
using DAL.Repositories.Implement;
using DAL.Repositories.Interface;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork 
    {
        private readonly ApplicationDbContext _context;


        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            UserRepo = new UserRepository(_context);
            TokenRepo = new TokenRepository(_context);
            BloodRegistrationRepo = new BloodRegistrationRepository(_context);
            EventRepo = new EventRepository(_context);
            ChronicDiseaseRepo = new ChronicDiseaseRepository(_context);
            UserMedicalRepo = new UserMedicalRepository(_context);
            BloodRepo = new BloodRepository(_context);
        }

        public IUserRepository UserRepo { get; private set; }
        public ITokenRepository TokenRepo { get; private set; }
        public IBloodRegistrationRepository BloodRegistrationRepo { get; private set; }
        public IEventRepository EventRepo { get; private set; }
        public IChronicDiseaseRepository ChronicDiseaseRepo { get; private set; }
        public IUserMedicalRepository UserMedicalRepo { get; private set; }
        public IBloodRepository BloodRepo { get; private set; }
        public void Dispose()
        {
            _context.Dispose();
        }
        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<bool> SaveChangeAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
