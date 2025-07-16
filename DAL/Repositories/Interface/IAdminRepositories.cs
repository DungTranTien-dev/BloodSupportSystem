using DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Repositories.Interface
{
    public interface IContactQueryRepository
    {
        Task<List<ContactQuery>> GetAllAsync();
        Task<List<ContactQuery>> GetByStatusAsync(string status);
        Task<ContactQuery?> GetByIdAsync(Guid id);
        Task<ContactQuery> AddAsync(ContactQuery query);
        Task<ContactQuery> UpdateAsync(ContactQuery query);
        Task<bool> DeleteAsync(Guid id);
        Task<List<ContactQuery>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
    }

    public interface ISystemSettingRepository
    {
        Task<List<SystemSetting>> GetAllAsync();
        Task<SystemSetting?> GetByKeyAsync(string key);
        Task<List<SystemSetting>> GetByCategoryAsync(string category);
        Task<SystemSetting> UpdateAsync(SystemSetting setting);
        Task<SystemSetting> AddAsync(SystemSetting setting);
        Task<bool> DeleteAsync(string key);
    }

    public interface IAdminActivityLogRepository
    {
        Task<AdminActivityLog> AddAsync(AdminActivityLog log);
        Task<List<AdminActivityLog>> GetAllAsync(int page = 1, int pageSize = 50);
        Task<List<AdminActivityLog>> GetByAdminAsync(string adminName, int page = 1, int pageSize = 50);
        Task<List<AdminActivityLog>> GetByModuleAsync(string module, int page = 1, int pageSize = 50);
        Task<List<AdminActivityLog>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<List<AdminActivityLog>> GetRecentAsync(int count = 10);
    }

    public interface IBloodGroupSettingRepository
    {
        Task<List<BloodGroupSetting>> GetAllAsync();
        Task<BloodGroupSetting?> GetByBloodTypeAsync(string bloodType);
        Task<BloodGroupSetting> UpdateAsync(BloodGroupSetting setting);
        Task<BloodGroupSetting> AddAsync(BloodGroupSetting setting);
        Task<bool> DeleteAsync(string bloodType);
        Task<List<BloodGroupSetting>> GetActiveAsync();
    }

    public interface IAdminReportRepository
    {
        Task<AdminReport> AddAsync(AdminReport report);
        Task<List<AdminReport>> GetAllAsync();
        Task<AdminReport?> GetByIdAsync(Guid id);
        Task<List<AdminReport>> GetByTypeAsync(string reportType);
        Task<List<AdminReport>> GetByGeneratorAsync(string generatedBy);
        Task<List<AdminReport>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<bool> ArchiveAsync(Guid id);
        Task<bool> DeleteAsync(Guid id);
    }

    public interface INotificationRepository
    {
        Task<Notification> AddAsync(Notification notification);
        Task<List<Notification>> GetAllAsync();
        Task<List<Notification>> GetByUserIdAsync(Guid userId);
        Task<List<Notification>> GetByRoleAsync(string role);
        Task<List<Notification>> GetGlobalAsync();
        Task<List<Notification>> GetUnreadAsync(Guid? userId, string? role);
        Task<Notification?> GetByIdAsync(Guid id);
        Task<Notification> UpdateAsync(Notification notification);
        Task<bool> MarkAsReadAsync(Guid id);
        Task<bool> MarkAllAsReadAsync(Guid userId);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> DeleteExpiredAsync();
    }
}
