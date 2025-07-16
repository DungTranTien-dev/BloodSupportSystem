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
            SeparatedBloodComponentRepo = new SeparatedBloodComponentRepository(_context);
            BloodRequestRepo = new BloodRequestRepository(_context);
            TransactionRepo = new TransactionRepository(_context);

            // Admin Repositories
            ContactQueryRepo = new ContactQueryRepository(_context);
            SystemSettingRepo = new SystemSettingRepository(_context);
            AdminActivityLogRepo = new AdminActivityLogRepository(_context);
            BloodGroupSettingRepo = new BloodGroupSettingRepository(_context);
            AdminReportRepo = new AdminReportRepository(_context);
            NotificationRepo = new NotificationRepository(_context);
        }

        public IUserRepository UserRepo { get; private set; }
        public ITokenRepository TokenRepo { get; private set; }
        public IBloodRegistrationRepository BloodRegistrationRepo { get; private set; }
        public IEventRepository EventRepo { get; private set; }
        public IChronicDiseaseRepository ChronicDiseaseRepo { get; private set; }
        public IUserMedicalRepository UserMedicalRepo { get; private set; }
        public IBloodRepository BloodRepo { get; private set; }
        public ISeparatedBloodComponentRepository SeparatedBloodComponentRepo { get; private set; }
        public IBloodRequestRepository BloodRequestRepo { get; private set; }
        public ITransactionRepository TransactionRepo { get; private set; }
        
        // Admin Repositories
        public IContactQueryRepository ContactQueryRepo { get; private set; }
        public ISystemSettingRepository SystemSettingRepo { get; private set; }
        public IAdminActivityLogRepository AdminActivityLogRepo { get; private set; }
        public IBloodGroupSettingRepository BloodGroupSettingRepo { get; private set; }
        public IAdminReportRepository AdminReportRepo { get; private set; }
        public INotificationRepository NotificationRepo { get; private set; }
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
