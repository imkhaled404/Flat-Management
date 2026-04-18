using FlatManage.Domain.Common;
using System;

namespace FlatManage.Domain.Entities
{
    public class SMSLog : BaseEntity
    {
        public string RecipientPhone { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public DateTimeOffset SentAt { get; set; } = DateTimeOffset.UtcNow;
        public string Status { get; set; } = string.Empty;
        public string Provider { get; set; } = string.Empty;
        public string? MessageId { get; set; }
        public int? TenantId { get; set; }

        public Tenant? Tenant { get; set; }
    }
}
