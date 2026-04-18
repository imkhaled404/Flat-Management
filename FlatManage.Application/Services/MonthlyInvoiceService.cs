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
    public class MonthlyInvoiceService : IMonthlyInvoiceService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MonthlyInvoiceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task GenerateMonthlyInvoicesAsync(int month, int year, CancellationToken cancellationToken = default)
        {
            var activeAgreements = await _unitOfWork.Repository<Agreement>()
                .Query()
                .Where(a => a.Status == AgreementStatus.Active && a.StartDate <= DateTimeOffset.UtcNow)
                .ToListAsync(cancellationToken);

            foreach (var agreement in activeAgreements)
            {
                // Check if invoice already exists for this tenant, unit, month, year
                bool exists = await _unitOfWork.Repository<RentInvoice>()
                    .ExistsAsync(i => i.TenantId == agreement.TenantId && i.UnitId == agreement.UnitId && i.BillingMonth == month && i.BillingYear == year, cancellationToken);

                if (!exists)
                {
                    var invoice = new RentInvoice
                    {
                        TenantId = agreement.TenantId,
                        UnitId = agreement.UnitId,
                        AgreementId = agreement.Id,
                        InvoiceNumber = $"INV-{year}{month:D2}-{agreement.TenantId}-{Guid.NewGuid().ToString().Substring(0, 4).ToUpper()}",
                        BillingMonth = month,
                        BillingYear = year,
                        RentAmount = agreement.MonthlyRent,
                        DueDate = new DateTimeOffset(new DateTime(year, month, 10)).AddMonths(0), // Due on 10th of the month
                        Status = InvoiceStatus.Pending,
                        PaidAmount = 0,
                        LateFee = 0
                    };

                    await _unitOfWork.Repository<RentInvoice>().AddAsync(invoice, cancellationToken);
                }
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task CalculateLateFeesAsync(CancellationToken cancellationToken = default)
        {
            var overdueInvoices = await _unitOfWork.Repository<RentInvoice>()
                .Query()
                .Where(i => i.Status == InvoiceStatus.Pending && i.DueDate < DateTimeOffset.UtcNow)
                .ToListAsync(cancellationToken);

            foreach (var invoice in overdueInvoices)
            {
                // Simple logic: add 5% late fee if overdue
                if (invoice.LateFee == 0)
                {
                    invoice.LateFee = invoice.RentAmount * 0.05m;
                    invoice.Status = InvoiceStatus.Overdue;
                    _unitOfWork.Repository<RentInvoice>().Update(invoice);
                }
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
