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
    public class OccupancyService : IOccupancyService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OccupancyService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task UpdateUnitStatusAsync(int unitId, CancellationToken cancellationToken = default)
        {
            var unit = await _unitOfWork.Repository<Unit>().GetByIdAsync(unitId, cancellationToken);
            if (unit == null) return;

            var activeAgreement = await _unitOfWork.Repository<Agreement>()
                .Query()
                .AnyAsync(a => a.UnitId == unitId && a.Status == AgreementStatus.Active && a.StartDate <= DateTimeOffset.UtcNow && a.EndDate >= DateTimeOffset.UtcNow, cancellationToken);

            unit.Status = activeAgreement ? UnitStatus.Occupied : UnitStatus.Available;
            _unitOfWork.Repository<Unit>().Update(unit);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task RefreshAllUnitsStatusAsync(CancellationToken cancellationToken = default)
        {
            var units = await _unitOfWork.Repository<Unit>().GetAllAsync(cancellationToken);
            foreach (var unit in units)
            {
                var activeAgreement = await _unitOfWork.Repository<Agreement>()
                    .Query()
                    .AnyAsync(a => a.UnitId == unit.Id && a.Status == AgreementStatus.Active && a.StartDate <= DateTimeOffset.UtcNow && a.EndDate >= DateTimeOffset.UtcNow, cancellationToken);

                unit.Status = activeAgreement ? UnitStatus.Occupied : UnitStatus.Available;
                _unitOfWork.Repository<Unit>().Update(unit);
            }
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
