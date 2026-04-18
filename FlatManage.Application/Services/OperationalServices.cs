using FlatManage.Application.Interfaces;
using FlatManage.Domain.Entities;
using FlatManage.Domain.Enums;
using FlatManage.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FlatManage.Application.Services
{
    public class BillService : IBillService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BillService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task GenerateBulkBillsAsync(int month, int year, decimal serviceCharge, decimal waterRate, decimal gasRate)
        {
            var occupiedUnits = await _unitOfWork.Repository<Unit>().Query()
                .Include(u => u.Tenants)
                .Where(u => u.Status == UnitStatus.Occupied)
                .ToListAsync();

            foreach (var unit in occupiedUnits)
            {
                var tenant = unit.Tenants.FirstOrDefault(t => t.IsActive);
                if (tenant == null) continue;

                // Create Service Charge Bill
                await CreateBill(unit.Id, tenant.Id, BillType.ServiceCharge, month, year, serviceCharge);
                // Create Water Bill
                await CreateBill(unit.Id, tenant.Id, BillType.Water, month, year, waterRate);
                // Create Gas Bill
                await CreateBill(unit.Id, tenant.Id, BillType.Gas, month, year, gasRate);
            }

            await _unitOfWork.SaveChangesAsync();
        }

        private async Task CreateBill(int unitId, int tenantId, BillType type, int month, int year, decimal amount)
        {
            var exists = await _unitOfWork.Repository<Bill>()
                .ExistsAsync(b => b.UnitId == unitId && b.BillType == type && b.BillingMonth == month && b.BillingYear == year);

            if (!exists)
            {
                var bill = new Bill
                {
                    UnitId = unitId,
                    TenantId = tenantId,
                    BillType = type,
                    BillingMonth = month,
                    BillingYear = year,
                    Amount = amount,
                    Status = BillStatus.Pending,
                    DueDate = new System.DateTimeOffset(new System.DateTime(year, month, 15)),
                    InvoiceNumber = $"BILL-{type}-{year}{month:D2}-{unitId}"
                };
                await _unitOfWork.Repository<Bill>().AddAsync(bill);
            }
        }
    }

    public class ReportService : IReportService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReportService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<object> GetMonthlyCollectionReportAsync(int month, int year)
        {
            return await _unitOfWork.Repository<Payment>().Query()
                .Where(p => p.PaymentDate.Month == month && p.PaymentDate.Year == year)
                .Select(p => new { p.Tenant.User.FullName, p.Amount, p.PaymentDate, p.PaymentMethod })
                .ToListAsync();
        }

        public async Task<object> GetOccupancyReportAsync()
        {
            return await _unitOfWork.Repository<Unit>().Query()
                .GroupBy(u => u.Status)
                .Select(g => new { Status = g.Key.ToString(), Count = g.Count() })
                .ToListAsync();
        }

        public async Task<object> GetOverdueTenantsAsync()
        {
            return await _unitOfWork.Repository<RentInvoice>().Query()
                .Where(i => i.Status == InvoiceStatus.Overdue)
                .Select(i => new { i.Tenant.User.FullName, i.Unit.UnitNumber, i.RentAmount, i.DueDate })
                .ToListAsync();
        }
    }
}
