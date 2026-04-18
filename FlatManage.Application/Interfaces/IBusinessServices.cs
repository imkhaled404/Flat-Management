using System.Threading;
using System.Threading.Tasks;

namespace FlatManage.Application.Interfaces
{
    public interface IMonthlyInvoiceService
    {
        Task GenerateMonthlyInvoicesAsync(int month, int year, CancellationToken cancellationToken = default);
        Task CalculateLateFeesAsync(CancellationToken cancellationToken = default);
    }

    public interface IOccupancyService
    {
        Task UpdateUnitStatusAsync(int unitId, CancellationToken cancellationToken = default);
        Task RefreshAllUnitsStatusAsync(CancellationToken cancellationToken = default);
    }

    public interface IDashboardService
    {
        Task<object> GetAdminDashboardStatsAsync(CancellationToken cancellationToken = default);
        Task<object> GetTenantDashboardStatsAsync(string userId, CancellationToken cancellationToken = default);
    }

    public interface IReportService
    {
        Task<object> GetMonthlyCollectionReportAsync(int month, int year);
        Task<object> GetOccupancyReportAsync();
        Task<object> GetOverdueTenantsAsync();
    }

    public interface IBillService
    {
        Task GenerateBulkBillsAsync(int month, int year, decimal serviceCharge, decimal waterRate, decimal gasRate);
    }
}
