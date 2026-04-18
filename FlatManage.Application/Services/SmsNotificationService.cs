using FlatManage.Application.Interfaces;
using FlatManage.Domain.Entities;
using FlatManage.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace FlatManage.Application.Services
{
    public class SmsNotificationService
    {
        private readonly ISmsService _smsService;
        private readonly IUnitOfWork _unitOfWork;

        public SmsNotificationService(ISmsService smsService, IUnitOfWork unitOfWork)
        {
            _smsService = smsService;
            _unitOfWork = unitOfWork;
        }

        public async Task SendDueReminderAsync(int tenantId, string message, CancellationToken cancellationToken = default)
        {
            var tenant = await _unitOfWork.Repository<Tenant>().GetByIdAsync(tenantId, cancellationToken);
            if (tenant != null && tenant.User != null)
            {
                bool sent = await _smsService.SendSmsAsync(tenant.User.PhoneNumber!, message);
                
                var log = new SMSLog
                {
                    RecipientPhone = tenant.User.PhoneNumber!,
                    Message = message,
                    Status = sent ? "Sent" : "Failed",
                    TenantId = tenantId
                };
                await _unitOfWork.Repository<SMSLog>().AddAsync(log, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
