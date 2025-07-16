using DAL.Data;
using DAL.Models;
using DAL.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories.Implement
{
    public class ContactQueryRepository : IContactQueryRepository
    {
        private readonly ApplicationDbContext _context;

        public ContactQueryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ContactQuery>> GetAllAsync()
        {
            return await _context.ContactQueries
                .OrderByDescending(cq => cq.CreatedAt)
                .ToListAsync();
        }

        public async Task<List<ContactQuery>> GetByStatusAsync(string status)
        {
            return await _context.ContactQueries
                .Where(cq => cq.Status == status)
                .OrderByDescending(cq => cq.CreatedAt)
                .ToListAsync();
        }

        public async Task<ContactQuery?> GetByIdAsync(Guid id)
        {
            return await _context.ContactQueries
                .FirstOrDefaultAsync(cq => cq.Id == id);
        }

        public async Task<ContactQuery> AddAsync(ContactQuery query)
        {
            _context.ContactQueries.Add(query);
            await _context.SaveChangesAsync();
            return query;
        }

        public async Task<ContactQuery> UpdateAsync(ContactQuery query)
        {
            query.UpdatedAt = DateTime.UtcNow;
            _context.ContactQueries.Update(query);
            await _context.SaveChangesAsync();
            return query;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var query = await GetByIdAsync(id);
            if (query == null) return false;

            _context.ContactQueries.Remove(query);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<ContactQuery>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.ContactQueries
                .Where(cq => cq.CreatedAt >= startDate && cq.CreatedAt <= endDate)
                .OrderByDescending(cq => cq.CreatedAt)
                .ToListAsync();
        }
    }

    public class SystemSettingRepository : ISystemSettingRepository
    {
        private readonly ApplicationDbContext _context;

        public SystemSettingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<SystemSetting>> GetAllAsync()
        {
            return await _context.SystemSettings
                .Where(ss => ss.IsActive)
                .OrderBy(ss => ss.Category)
                .ThenBy(ss => ss.Key)
                .ToListAsync();
        }

        public async Task<SystemSetting?> GetByKeyAsync(string key)
        {
            return await _context.SystemSettings
                .FirstOrDefaultAsync(ss => ss.Key == key && ss.IsActive);
        }

        public async Task<List<SystemSetting>> GetByCategoryAsync(string category)
        {
            return await _context.SystemSettings
                .Where(ss => ss.Category == category && ss.IsActive)
                .OrderBy(ss => ss.Key)
                .ToListAsync();
        }

        public async Task<SystemSetting> UpdateAsync(SystemSetting setting)
        {
            setting.LastModified = DateTime.UtcNow;
            _context.SystemSettings.Update(setting);
            await _context.SaveChangesAsync();
            return setting;
        }

        public async Task<SystemSetting> AddAsync(SystemSetting setting)
        {
            _context.SystemSettings.Add(setting);
            await _context.SaveChangesAsync();
            return setting;
        }

        public async Task<bool> DeleteAsync(string key)
        {
            var setting = await GetByKeyAsync(key);
            if (setting == null) return false;

            setting.IsActive = false;
            await UpdateAsync(setting);
            return true;
        }
    }

    public class AdminActivityLogRepository : IAdminActivityLogRepository
    {
        private readonly ApplicationDbContext _context;

        public AdminActivityLogRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<AdminActivityLog> AddAsync(AdminActivityLog log)
        {
            _context.AdminActivityLogs.Add(log);
            await _context.SaveChangesAsync();
            return log;
        }

        public async Task<List<AdminActivityLog>> GetAllAsync(int page = 1, int pageSize = 50)
        {
            return await _context.AdminActivityLogs
                .OrderByDescending(al => al.Timestamp)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<List<AdminActivityLog>> GetByAdminAsync(string adminName, int page = 1, int pageSize = 50)
        {
            return await _context.AdminActivityLogs
                .Where(al => al.AdminName == adminName)
                .OrderByDescending(al => al.Timestamp)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<List<AdminActivityLog>> GetByModuleAsync(string module, int page = 1, int pageSize = 50)
        {
            return await _context.AdminActivityLogs
                .Where(al => al.Module == module)
                .OrderByDescending(al => al.Timestamp)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<List<AdminActivityLog>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.AdminActivityLogs
                .Where(al => al.Timestamp >= startDate && al.Timestamp <= endDate)
                .OrderByDescending(al => al.Timestamp)
                .ToListAsync();
        }

        public async Task<List<AdminActivityLog>> GetRecentAsync(int count = 10)
        {
            return await _context.AdminActivityLogs
                .OrderByDescending(al => al.Timestamp)
                .Take(count)
                .ToListAsync();
        }
    }

    public class BloodGroupSettingRepository : IBloodGroupSettingRepository
    {
        private readonly ApplicationDbContext _context;

        public BloodGroupSettingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<BloodGroupSetting>> GetAllAsync()
        {
            return await _context.BloodGroupSettings
                .OrderBy(bgs => bgs.BloodType)
                .ToListAsync();
        }

        public async Task<BloodGroupSetting?> GetByBloodTypeAsync(string bloodType)
        {
            return await _context.BloodGroupSettings
                .FirstOrDefaultAsync(bgs => bgs.BloodType == bloodType);
        }

        public async Task<BloodGroupSetting> UpdateAsync(BloodGroupSetting setting)
        {
            setting.UpdatedAt = DateTime.UtcNow;
            _context.BloodGroupSettings.Update(setting);
            await _context.SaveChangesAsync();
            return setting;
        }

        public async Task<BloodGroupSetting> AddAsync(BloodGroupSetting setting)
        {
            _context.BloodGroupSettings.Add(setting);
            await _context.SaveChangesAsync();
            return setting;
        }

        public async Task<bool> DeleteAsync(string bloodType)
        {
            var setting = await GetByBloodTypeAsync(bloodType);
            if (setting == null) return false;

            _context.BloodGroupSettings.Remove(setting);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<BloodGroupSetting>> GetActiveAsync()
        {
            return await _context.BloodGroupSettings
                .Where(bgs => bgs.IsActive)
                .OrderBy(bgs => bgs.BloodType)
                .ToListAsync();
        }
    }

    public class AdminReportRepository : IAdminReportRepository
    {
        private readonly ApplicationDbContext _context;

        public AdminReportRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<AdminReport> AddAsync(AdminReport report)
        {
            _context.AdminReports.Add(report);
            await _context.SaveChangesAsync();
            return report;
        }

        public async Task<List<AdminReport>> GetAllAsync()
        {
            return await _context.AdminReports
                .Where(ar => !ar.IsArchived)
                .OrderByDescending(ar => ar.GeneratedAt)
                .ToListAsync();
        }

        public async Task<AdminReport?> GetByIdAsync(Guid id)
        {
            return await _context.AdminReports
                .FirstOrDefaultAsync(ar => ar.Id == id);
        }

        public async Task<List<AdminReport>> GetByTypeAsync(string reportType)
        {
            return await _context.AdminReports
                .Where(ar => ar.ReportType == reportType && !ar.IsArchived)
                .OrderByDescending(ar => ar.GeneratedAt)
                .ToListAsync();
        }

        public async Task<List<AdminReport>> GetByGeneratorAsync(string generatedBy)
        {
            return await _context.AdminReports
                .Where(ar => ar.GeneratedBy == generatedBy && !ar.IsArchived)
                .OrderByDescending(ar => ar.GeneratedAt)
                .ToListAsync();
        }

        public async Task<List<AdminReport>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.AdminReports
                .Where(ar => ar.GeneratedAt >= startDate && ar.GeneratedAt <= endDate && !ar.IsArchived)
                .OrderByDescending(ar => ar.GeneratedAt)
                .ToListAsync();
        }

        public async Task<bool> ArchiveAsync(Guid id)
        {
            var report = await GetByIdAsync(id);
            if (report == null) return false;

            report.IsArchived = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var report = await GetByIdAsync(id);
            if (report == null) return false;

            _context.AdminReports.Remove(report);
            await _context.SaveChangesAsync();
            return true;
        }
    }

    public class NotificationRepository : INotificationRepository
    {
        private readonly ApplicationDbContext _context;

        public NotificationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Notification> AddAsync(Notification notification)
        {
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();
            return notification;
        }

        public async Task<List<Notification>> GetAllAsync()
        {
            return await _context.Notifications
                .Include(n => n.TargetUser)
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();
        }

        public async Task<List<Notification>> GetByUserIdAsync(Guid userId)
        {
            return await _context.Notifications
                .Include(n => n.TargetUser)
                .Where(n => n.TargetUserId == userId || n.IsGlobal)
                .Where(n => n.ExpiresAt == null || n.ExpiresAt > DateTime.UtcNow)
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();
        }

        public async Task<List<Notification>> GetByRoleAsync(string role)
        {
            return await _context.Notifications
                .Include(n => n.TargetUser)
                .Where(n => n.TargetRole == role || n.IsGlobal)
                .Where(n => n.ExpiresAt == null || n.ExpiresAt > DateTime.UtcNow)
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();
        }

        public async Task<List<Notification>> GetGlobalAsync()
        {
            return await _context.Notifications
                .Where(n => n.IsGlobal)
                .Where(n => n.ExpiresAt == null || n.ExpiresAt > DateTime.UtcNow)
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();
        }

        public async Task<List<Notification>> GetUnreadAsync(Guid? userId, string? role)
        {
            var query = _context.Notifications.AsQueryable();

            if (userId.HasValue)
            {
                query = query.Where(n => 
                    (n.TargetUserId == userId.Value || n.IsGlobal || n.TargetRole == role) &&
                    !n.IsRead);
            }
            else if (!string.IsNullOrEmpty(role))
            {
                query = query.Where(n => 
                    (n.TargetRole == role || n.IsGlobal) &&
                    !n.IsRead);
            }

            return await query
                .Where(n => n.ExpiresAt == null || n.ExpiresAt > DateTime.UtcNow)
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();
        }

        public async Task<Notification?> GetByIdAsync(Guid id)
        {
            return await _context.Notifications
                .Include(n => n.TargetUser)
                .FirstOrDefaultAsync(n => n.Id == id);
        }

        public async Task<Notification> UpdateAsync(Notification notification)
        {
            _context.Notifications.Update(notification);
            await _context.SaveChangesAsync();
            return notification;
        }

        public async Task<bool> MarkAsReadAsync(Guid id)
        {
            var notification = await GetByIdAsync(id);
            if (notification == null) return false;

            notification.IsRead = true;
            notification.ReadAt = DateTime.UtcNow;
            await UpdateAsync(notification);
            return true;
        }

        public async Task<bool> MarkAllAsReadAsync(Guid userId)
        {
            var notifications = await _context.Notifications
                .Where(n => (n.TargetUserId == userId || n.IsGlobal) && !n.IsRead)
                .ToListAsync();

            foreach (var notification in notifications)
            {
                notification.IsRead = true;
                notification.ReadAt = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var notification = await GetByIdAsync(id);
            if (notification == null) return false;

            _context.Notifications.Remove(notification);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteExpiredAsync()
        {
            var expiredNotifications = await _context.Notifications
                .Where(n => n.ExpiresAt != null && n.ExpiresAt <= DateTime.UtcNow)
                .ToListAsync();

            _context.Notifications.RemoveRange(expiredNotifications);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
