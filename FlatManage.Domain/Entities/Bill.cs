using FlatManage.Domain.Common;
using FlatManage.Domain.Enums;
using System;
using System.Collections.Generic;

namespace FlatManage.Domain.Entities
{
    public class Bill : BaseEntity
    {
        public int UnitId { get; set; }
        public int TenantId { get; set; }
        public BillType BillType { get; set; }
        public int BillingMonth { get; set; }
        public int BillingYear { get; set; }
        public decimal PreviousReading { get; set; }
        public decimal CurrentReading { get; set; }
        public decimal Units { get; set; }
        public decimal RatePerUnit { get; set; }
        public decimal Amount { get; set; }
        public DateTimeOffset DueDate { get; set; }
        public DateTimeOffset? PaidDate { get; set; }
        public BillStatus Status { get; set; }
        public string InvoiceNumber { get; set; } = string.Empty;

        public Unit Unit { get; set; } = null!;
        public Tenant Tenant { get; set; } = null!;
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}
