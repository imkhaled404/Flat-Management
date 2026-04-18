using FlatManage.Application.Interfaces;
using FlatManage.Domain.Entities;
using FlatManage.Domain.Enums;
using FlatManage.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FlatManage.Application.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DashboardService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<object> GetAdminDashboardStatsAsync(CancellationToken cancellationToken = default)
        {
            var buildingsCount = await _unitOfWork.Repository<Building>().Query().CountAsync(cancellationToken);
            var unitsCount = await _unitOfWork.Repository<Unit>().Query().CountAsync(cancellationToken);
            var tenantsCount = await _unitOfWork.Repository<Tenant>().Query().CountAsync(cancellationToken);
            
            var occupiedUnits = await _unitOfWork.Repository<Unit>().Query()
                .CountAsync(u => u.Status == UnitStatus.Occupied, cancellationToken);
            
            var occupancyRate = unitsCount > 0 ? (double)occupiedUnits / unitsCount * 100 : 0;

            var currentMonth = DateTime.Now.Month;
            var currentYear = DateTime.Now.Year;

            var totalCollected = await _unitOfWork.Repository<Payment>().Query()
                .Where(p => p.PaymentDate.Month == currentMonth && p.PaymentDate.Year == currentYear)
                .SumAsync(p => p.Amount, cancellationToken);

            var overdueCount = await _unitOfWork.Repository<RentInvoice>().Query()
                .CountAsync(i => i.Status == InvoiceStatus.Overdue, cancellationToken);

            var openTickets = await _unitOfWork.Repository<Ticket>().Query()
                .CountAsync(t => t.Status == TicketStatus.Open || t.Status == TicketStatus.InProgress, cancellationToken);

            return new
            {
                TotalBuildings = buildingsCount,
                TotalUnits = unitsCount,
                TotalTenants = tenantsCount,
                OccupancyRate = Math.Round(occupancyRate, 2),
                MonthlyCollection = totalCollected,
                OverdueInvoices = overdueCount,
                ActiveTickets = openTickets
            };
        }

        public async Task<object> GetTenantDashboardStatsAsync(string userId, CancellationToken cancellationToken = default)
        {
            var tenant = await _unitOfWork.Repository<Tenant>().Query()
                .FirstOrDefaultAsync(t => t.UserId == userId, cancellationToken);

            if (tenant == null) return new { };

            var pendingInvoices = await _unitOfWork.Repository<RentInvoice>().Query()
                .Where(i => i.TenantId == tenant.Id && (i.Status == InvoiceStatus.Pending || i.Status == InvoiceStatus.Overdue))
                .SumAsync(i => i.RentAmount - i.PaidAmount, cancellationToken);

            var pendingBills = await _unitOfWork.Repository<Bill>().Query()
                .Where(b => b.TenantId == tenant.Id && b.Status == BillStatus.Pending)
                .SumAsync(b => b.Amount, cancellationToken);

            var recentTickets = await _unitOfWork.Repository<Ticket>().Query()
                .Where(t => t.TenantId == tenant.Id)
                .OrderByDescending(t => t.CreatedAt)
                .Take(5)
                .ToListAsync(cancellationToken);

            return new
            {
                TotalDue = pendingInvoices + pendingBills,
                PendingInvoices = pendingInvoices,
                PendingBills = pendingBills,
                RecentTickets = recentTickets
            };
        }
    }
}
